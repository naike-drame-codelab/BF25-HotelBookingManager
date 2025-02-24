using System.Text;
using System.Net.Mail;
using BookingManager.DAL.Entities;
using System.Security.Cryptography;
using System.Transactions;
using BookingManager.Application.Abstractions.Repositories;
using BookingManager.Application.Abstractions.Business;
using System.Data;
using BookingManager.Application.Exceptions;
using System.Reflection;
using System.Security.Authentication;

namespace BookingManager.Application.Services
{
    public class CustomerService(ICustomerRepository repository, ILoginRepository loginRepository,SmtpClient smtpClient) : ICustomerService
    {
        public List<Customer> GetBySearch(string? search)
        {
            return [.. repository.FindByKeyword(search)];
        }

        public Customer CreateCustomer(Customer c)
        {
            // vérifier que l'email est unique avant de tenter l'insertion
            Customer? cu = repository.GetByEmail(c.Email);
            if (cu != null)
            {
                throw new DuplicateFieldException(c.Email, "Cet email existe déjà");
            }
            // créer un username
            c.Username = CreateUsername(c);
            // créer un password
            string pwd = CreatePassword();
            // hasher le password après l'avoir transformé en tableau de bytes (salting avec l'email unique, no pepper)
            c.Password = HashPassword(pwd, c.Email);
            // sauver dans la db
            using TransactionScope scope = new TransactionScope();
            Customer result = repository.Add(c);
            // envoyer un email (mdp non crypté)
            SendRegistrationMail(c, pwd);
            // terminer le scope
            scope.Complete();
            // return le résultat 
            return result;
        }

        // in pour empêcher de réécrire le Customer c, de l'écraser avec un new Customer()
        private string CreateUsername(in Customer c)
        {
            string prefix = c.LastName[..2] + c.FirstName[..2].ToUpper();
            int count = repository.CountByUsername(prefix);
            return prefix + count.ToString().PadLeft(4, '0');
        }

        // static car pas d'incidence sur le reste
        // comme les arrow functions de JS, comme on a une seule ligne de return, on peut en faire une fonction fléchée
        private static string CreatePassword() => Guid.NewGuid().ToString().Replace("-", "")[..10];

        private static byte[] HashPassword(string pwd, string salt) => SHA512.HashData(Encoding.UTF8.GetBytes(pwd + salt));

        private void SendRegistrationMail(Customer c, string pwd)
        {
            MailMessage mail = new MailMessage
            {
                Subject = "Merci pour votre inscription !",
                Body = $"Votre mot de pase : {pwd}",
                From = new MailAddress("noreply@test.com")
            };
            mail.To.Add(new MailAddress(c.Email));
            smtpClient.Send(mail);
        }

        public void DeleteCustomer(int id)
        {
            Customer? c = repository.GetById(id);
            if (c == null)
            {
                throw new KeyNotFoundException("Cet utilisateur n'existe pas.");
            }
            else
            {
                c.Deleted = true;
                c.PhoneNumber = null;
                repository.Update(c);
            }
        }

        public IEnumerable<Customer> FindByKeyword(string? search)
        {
            return repository.FindByKeyword(search)
                .Where(c => !c.Deleted); // skippe les Deleted qui sont à true
        }

        public Customer? GetCustomer(int id)
        {
            // return repository.GetById(id)?.Deleted ?? true ? repository.GetById(id) : null;
            Customer? customer = repository.GetById(id);
            if (customer is not null && customer.Deleted)
            {
                return null;
            }
            return customer;
        }

        public void UpdateCustomer(int id, string lastName, string firstName, string? password, string? phoneNumber)
        {
            Customer? cu = repository.GetById(id);
            if (cu == null)
            {
                throw new KeyNotFoundException();
            }
            cu.LastName = lastName;
            cu.FirstName = firstName;
            cu.PhoneNumber = phoneNumber;

            if (password != null)
            {
                byte[] hash = HashPassword(password, cu.Email);
                // if (hash.SequenceEqual(cu.Password)) --> comparer 2 tableaux = comparer 2 refs donc on peut pas faire == comparaison
                if (Encoding.UTF8.GetString(hash) == Encoding.UTF8.GetString(cu.Password))
                {
                    throw new DuplicateFieldException(nameof(cu.Password), "Le mot de passe doit être différent.");
                }
                cu.Password = hash;
            }
            repository.Update(cu);
        }
    
        public Login Login(string usernameOrEmail, string password)
        {
            Login? login = loginRepository.GetByUsername(usernameOrEmail);
            Customer? customer = repository.FindOneByUsernameOrEmail(usernameOrEmail);

            if(login == null && customer == null)
            {
                throw new AuthenticationException();
            }
            if(customer != null)
            {
                if(customer.Password.SequenceEqual(HashPassword(password, customer.Email)))
                {
                    return customer;
                }
            }if(login != null)
            {
                if(login.Password.SequenceEqual(HashPassword(password, "")))
                {
                    return login;
                }
            }

            throw new AuthenticationException();
        }
    }
}

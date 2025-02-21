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

namespace BookingManager.Application.Services
{
    public class CustomerService(ICustomerRepository repository, SmtpClient smtpClient) : ICustomerService
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

        public Customer GetCustomer(int id)
        {
            Customer? c = repository.GetById(id);
            if (c == null)
            {
                throw new KeyNotFoundException("Cet utilisateur n'existe pas.");
            }
            return c;
        }

        public Customer UpdateCustomer(Customer c)
        {
            // vérifier que l'email est unique avant de tenter l'insertion
            Customer? cu = repository.GetById(c.LoginId);
            if (cu != null)
            {
                throw new DuplicateFieldException(c.Email, "Cet utilisateur n'existe pas.");
            }
            else
            {
                using TransactionScope scope = new TransactionScope();
                Customer result = repository.Update(c);
                scope.Complete();
                return result;
            }
        }
    }
}

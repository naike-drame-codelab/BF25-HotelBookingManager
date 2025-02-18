using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;
using BookingManager.MVC.Mappers;
using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.MVC.Controllers
{
    // injection de dépendances pour repository pour donner accès à la db 
    public class CustomerController(ICustomerRepository repository, SmtpClient smtpClient) : Controller
    {
        //FromQuery pour un GET, FromForm pour un POST
        public IActionResult Index([FromQuery] CustomerSearchFormViewModel model)
        {
            #region Avant refactoring
            //List<Customer> customers = repository.GetAll();
            //// mapping des objets customer de la db vers les objets de la view
            //List<CustomerIndexViewModel> model = customers
            //    .Select(ToViewModelMappers.ToCustomerIndex)
            //    .ToList(); 
            #endregion

            if (ModelState.IsValid)
            {
                model.Results = repository
                    .FindByKeyword(model.Search)
                    .Select(ToViewModelMappers.ToCustomerIndex)
                    .ToList();
                return View(model);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerCreateFormViewModel form)
        {
            // vérifier si le formulaire est valide
            if (!ModelState.IsValid)
            // oui
            {
                // revenir sur le formulaire
                return View(form);
            }
            // non 
            // traiter les données
            // mapper les données dans une entité
            Customer c = new Customer
            {
                LastName = form.LastName,
                FirstName = form.FirstName,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
            };
            // créer un username
            string prefix = form.LastName[..2] + form.FirstName[..2].ToUpper();
            int count = repository.CountByUsername(prefix);
            c.Username = prefix + count.ToString().PadLeft(4, '0');
            // créer un password
            string pwd = Guid.NewGuid().ToString().Replace("-", "")[..10];
            // hasher le password après l'avoir transformé en tableau de bytes (salting avec l'email unique, no pepper)
            byte[] hashedPwd = SHA512.HashData(Encoding.UTF8.GetBytes(pwd + form.Email));
            c.Password = hashedPwd;
            // sauver dans la db
            repository.Add(c);
            // envoyer un email (mdp non crypté)
            MailMessage mail = new MailMessage
            {
                Subject = "Merci pour votre inscription !",
                Body = $"Votre mot de pase : {pwd}",
                From = new MailAddress("noreply@test.com")
            };
            mail.To.Add(new MailAddress(form.Email));
            smtpClient.Send(mail);
            // revenir sur une autre page
            return RedirectToAction("Index");
        }
    }
}

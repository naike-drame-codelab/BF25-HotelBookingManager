using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using BookingManager.Application.Abstractions.Business;
using BookingManager.Application.Abstractions.Repositories;
using BookingManager.Application.Exceptions;
using BookingManager.DAL.Entities;
using BookingManager.MVC.Mappers;
using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace BookingManager.MVC.Controllers
{
    // injection de dépendances pour repository pour donner accès à la db 
    public class CustomerController(ICustomerService customerService) : Controller
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
                model.Results = customerService.FindByKeyword(model.Search)
                   .Select(ToViewModelMappers.ToCustomerIndex).ToList();
            }
            return View(model);
        }

        [Route("Customer/Details/{id}")]
        public IActionResult Details([FromRoute] int id)
        {

            //return View va bien sur la page et on voit bien l'url avec id mais pas les data
            Customer? c = customerService.GetCustomer(id);
            if (c == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                CustomerIndexViewModel model = ToViewModelMappers.ToCustomerIndex(c);
                return View(model);
            }
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
                return View();
            }
            // non 
            // traiter les données
            // mapper les données dans une entité
            // créer le nouveau customer
            try
            {
                customerService.CreateCustomer(ToEntityMappers.ToCustomerCreate(form));
            }
            catch (DuplicateFieldException ex)
            {

                ModelState.AddModelError(ex.FieldName, ex.Message);
                return View();
            }
            catch (SmtpException)
            {
                TempData["error"] = "L'email n'a pas pu être envoyé";
                return View();
            }

            TempData["success"] = "Enregistrement OK";
            return RedirectToAction("Index");
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(CustomerCreateFormViewModel form)
        {

            // vérifier si le formulaire est valide
            if (!ModelState.IsValid)
            // oui
            {
                // revenir sur le formulaire
                return View();
            }
            // non 
            // traiter les données
            // mapper les données dans une entité
            // créer le nouveau customer
            try
            {
                customerService.CreateCustomer(ToEntityMappers.ToCustomerCreate(form));
            }
            catch (DuplicateFieldException ex)
            {

                ModelState.AddModelError(ex.FieldName, ex.Message);
                return View();
            }
            catch (SmtpException)
            {
                TempData["error"] = "L'email n'a pas pu être envoyé";
                return View();
            }

            TempData["success"] = "Enregistrement OK";
            return RedirectToAction("Index");
        }


        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                customerService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

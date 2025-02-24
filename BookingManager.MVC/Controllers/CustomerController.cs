using System.Net.Mail;
using BookingManager.Application.Abstractions.Business;
using BookingManager.Application.Exceptions;
using BookingManager.DAL.Entities;
using BookingManager.MVC.Mappers;
using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.MVC.Controllers
{
    // injection de dépendances pour repository pour donner accès à la db 
    public class CustomerController(ICustomerService customerService) : Controller
    {
        //FromQuery pour un GET, FromForm pour un POST
        public IActionResult Index([FromQuery] CustomerSearchFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Results = customerService.FindByKeyword(model.Search)
                   .Select(ToViewModelMappers.ToCustomerIndex).ToList();
            }
            return View(model);
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
            try
            {
                // créer le nouveau customer
                // mapper les données dans une entité
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

        public IActionResult Update([FromRoute] int id)
        {
            Customer? customer = customerService.GetCustomer(id);
            if (customer is null)
            {
                return NotFound();
            }
            CustomerEditFormViewModel model = customer.ToCustomerEditForm();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update([FromRoute] int id, [FromForm] CustomerEditFormViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                customerService.UpdateCustomer(id, model.LastName, model.FirstName, model.Password, model.PhoneNumber);
                TempData["success"] = "OK";
                return RedirectToAction("Index");

            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DuplicateFieldException ex)
            {
                ModelState.AddModelError(ex.FieldName, ex.Message);
                return View();
            }

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

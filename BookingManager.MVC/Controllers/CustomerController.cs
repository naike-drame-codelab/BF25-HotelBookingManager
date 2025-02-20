﻿using System.Net.Mail;
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

namespace BookingManager.MVC.Controllers
{
    // injection de dépendances pour repository pour donner accès à la db 
    public class CustomerController(ICustomerRepository repository, ICustomerService customerService) : Controller
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
                    return View();
                }

                
                // non 
                // traiter les données
                // mapper les données dans une entité
                //Customer c = new Customer
                //{
                //    LastName = form.LastName,
                //    FirstName = form.FirstName,
                //    Email = form.Email,
                //    PhoneNumber = form.PhoneNumber,
                //};

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
                catch(SmtpException)
                {
                    TempData["error"] = "L'email n'a pas pus être envoyé";
                    return View();
                }
  
                TempData["success"] = "Enregistrement OK";
                return RedirectToAction("Index");           
        }
    }
}

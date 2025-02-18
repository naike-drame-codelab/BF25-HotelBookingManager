using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;
using BookingManager.MVC.Mappers;
using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BookingManager.MVC.Controllers
{
    public class CustomerController(ICustomerRepository repository) : Controller
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

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm] CustomerRegisterFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                //SHA512 SHA512 = new SHA512();

                //byte[] password = [255];
                //string username = model.LastName.Substring(0, 2) + model.FirstName.Substring(0, 2);

                Customer? customerByEmail = repository.GetByEmail(model.Email);
                if (customerByEmail != null)
                {
                    throw new Exception("L'email existe déjà");
                }

                repository.Add(model.ToCustomerIndex());

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}

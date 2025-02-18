using BookingManager.Application.Abstractions;
using BookingManager.MVC.Mappers;
using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}

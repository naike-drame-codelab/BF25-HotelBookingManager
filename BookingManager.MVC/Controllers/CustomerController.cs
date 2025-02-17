using BookingManager.Application.Abstractions;
using BookingManager.MVC.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.MVC.Controllers
{
    public class CustomerController(ICustomerRepository repository) : Controller
    {
        public IActionResult Index()
        {
            #region Avant refactoring
            //List<Customer> customers = repository.GetAll();
            //// mapping des objets customer de la db vers les objets de la view
            //List<CustomerIndexViewModel> model = customers
            //    .Select(ToViewModelMappers.ToCustomerIndex)
            //    .ToList(); 
            #endregion

            return View(repository
                .GetAll()
                .Select(ToViewModelMappers.ToCustomerIndex)
                .ToList()
                );
        }
    }
}

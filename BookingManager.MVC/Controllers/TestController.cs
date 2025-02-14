using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace BookingManager.MVC.Controllers
{
    public class TestController : Controller
    {
        public string Hello([FromQuery] string name)
        {
            return $"Hello {name}!!!";
        }

        //[Route("pause")]
        [Route("[controller]/toto/{heure}")]
        public ViewResult Pause([FromRoute] int id)
        {
            #region Solution proposée
            //DateTime time = DateTime.Now;
            //DateTime breakTime = DateTime.Today.AddHours(10);
            //TimeSpan diff = breakTime - time;
            //string msg = time < breakTime ? $"Il reste {diff.Minutes}min{diff.Seconds}s avant la pause." : "C'est la pause!";

            //return msg; 
            #endregion

            #region Correction
            //int heurePause = 12;
            //if (heure >= heurePause) return View("Pause");
            int minutesRestantes = ((id - DateTime.Now.Hour - 1) * 60) + (60 - DateTime.Now.Minute);
            return View(minutesRestantes);
            // return View("Pause", minutesRestantes);
            #endregion
        }

        // méthode pour afficher le formulaire
        // [HttpGet]
        public ViewResult Contact()
        {
            return View();
        }

        //méthode pour traiter le formulaire
        // ViewResult & RedirectToAction héritent tous 2 de ActionResult
        [HttpPost]
        public IActionResult Contact([FromForm] ContactFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                // faire un traitement pour envoyer un email
                // redirection
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("199e72957c20c0", "f90a78cca99de2"),
                    EnableSsl = true
                };
                client.Send("from@example.com", "to@example.com", "Hello world", "testbody");
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // sinon revenir à la vue
                return View();
            }
        }
    }
}

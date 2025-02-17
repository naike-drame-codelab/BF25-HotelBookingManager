using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace BookingManager.MVC.Controllers
{
    // injection via le primary constructor
    public class TestController(SmtpClient smtpClient) : Controller
    {
        #region Injection par constructeur
        //private readonly SmtpClient _smtpClient;

        //public TestController(SmtpClient client)
        //{
        //    _smtpClient = client;
        //}

        #endregion

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
        public IActionResult Contact(
            [FromForm] ContactFormViewModel form)
        #region Injection par méthode
        // [FromServices] SmtpClient client

        #endregion
        {
            if (ModelState.IsValid)
            {
                // faire un traitement pour envoyer un email
                MailMessage message = new();
                message.Subject = form.Subject;
                message.Body = form.Message;
                message.From = new MailAddress("naike@DESKTOP-E563U3H");
                message.To.Add(new MailAddress("naike.drame@gmail.com"));

                smtpClient.Send(message);

                // redirection
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

using Microsoft.AspNetCore.Mvc;

namespace BookingManager.MVC.Controllers
{
    public class TestController : Controller
    {
        public string Hello([FromQuery] string name)
        {
            return $"Hello {name}!!!";
        }

        public string Pause()
        {
            #region Solution proposée
            //DateTime time = DateTime.Now;
            //DateTime breakTime = DateTime.Today.AddHours(10);
            //TimeSpan diff = breakTime - time;
            //string msg = time < breakTime ? $"Il reste {diff.Minutes}min{diff.Seconds}s avant la pause." : "C'est la pause!";

            //return msg; 
            #endregion

            #region Correction
            int heurePause = 10;
            if (DateTime.Now.Hour >= heurePause) return "Ce n'est pas encore l'heure de la pause";
            int minutesRestantes = ((heurePause - DateTime.Now.Hour - 1) * 60) + (60 - DateTime.Now.Minute);
            return $"Il reste {minutesRestantes} minutes avant la pause.";
            #endregion
        }
    }
}

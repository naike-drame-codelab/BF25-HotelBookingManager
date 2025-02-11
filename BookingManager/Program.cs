using BookingManager.DAL;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

/*----- Multi Threading & tâches asynchrones -----*/

// Rmq :  un async a été mis automatiquement sur main() après l'ajout de notre await t;

// permet de forcer l'arrêt d'un thread
CancellationTokenSource tokenSrc = new ();

// démarrer une action en arrière-plan
// un Task est similaire à une Promesse en js mais on décide si elle doit attendre
Task t = Task.Run(() =>
{
    // s'exécute sur un thread différent du thread principal
    for (int i = 0; i < 10000000; i++)
    {
        Console.WriteLine(i);
        if (tokenSrc.Token.IsCancellationRequested) break;
    }
}, tokenSrc.Token);

// va annuler le thread en pressant espace et passer su le thread principal
ConsoleKey key = Console.ReadKey().Key;
if(key == ConsoleKey.Spacebar)
{
    tokenSrc.Cancel();
}

// await : exécution asynchrone, t va être annulée et va lancer le thread principal
// en LINQ, async = méthodes renvoient des Tasks
// si pas await t; et si on presse une key autre que espace : exécute les 2 threads en même temps
await t;

// s'exécute sur le thread principal
for (int i = 0;i < 20000000; i++)
{
    Console.WriteLine(i);
}
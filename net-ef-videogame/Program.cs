//Vogliamo modificare l’esercizio di ieri rimuovendo le parti gestite con SqlClient e implementandole con Entity Framework.
//Devono essere presenti tutte le funzionalità dell’esercizio originale.
//Aggiungiamo anche un’altra voce al menu :
//inserisci una nuova software house
//Fatto questo, ogni volta che creiamo un nuovo videogioco dobbiamo abbinargli la software house che l’ha prodotto (che dobbiamo aver precedentemente inserito in tabella), chiedendo all’utente l’id della software house e impostandolo nell’entity del videogame.
//Realizzare quindi tutte le entity e le migration necessarie per creare il database e implementare tutte le richieste dell’esercizio.
//BONUS : aggiungere un’altra voce di menu
//stampa tutti i videogiochi prodotti da una software house (all’utente verrà chiesto l’id della software house della quale mostrare i videogame)

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using net_ef_videogame.Classes;
using net_ef_videogame.Database;
using net_ef_videogame.Helpers;
using System;
using System.Linq.Expressions;

namespace net_ef_videogame
{
  class Program
    {
        public static void Main(string[] args)
        {
            int number = 1;
            while (number > 0 && number < 5)
            {
                Console.WriteLine("Buongiorno e benvenuto al nostro gestionale. Scegli un opzione\r\n\n1) Inserisci Software house\r\n2) Inserire un nuovo videogioco\r\n3) Ricercare un videogioco per id\r\n4) Ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input\r\n5) Cancellare un videogioco\r\n6) In base a un ID di una softawareHouse, trova tutti i videogiochi che ha sviluppato\r\n7) Chiudere il programma\r\n");
                number = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (number)
                {
                    case 1:
                        try
                        {

                        Console.WriteLine("Inserisci gentilmente i dati della casa softwarehouse..\n\r");

                        Console.WriteLine("\n\rNome ");
                        string namer = Console.ReadLine();
                        Console.WriteLine("\n\rDescrizione ");
                        string description = Console.ReadLine();

                        Console.WriteLine("\n\rCittà ");
                        string city = Console.ReadLine();

                        Console.WriteLine("\n\rPaese ");
                        string country = Console.ReadLine();
                            using (MovieContext db = new MovieContext())
                            {


                        SoftwareHouse softwareHouse = new SoftwareHouse() { Name = namer, Description = description, City = city, Country = country };

                        db.Add(softwareHouse);
                        db.SaveChanges();
                            }

                        Console.WriteLine("\n\rSoftwarehouse inserita correttamente\n\r");
                        
                        }catch (Exception ex) { Console.WriteLine(ex.Message); }


                        break;

                        case 2:
                        Console.WriteLine("Inserisci un nuovo videogioco, specificando gentilmente: \n ");

                        Console.WriteLine("\nTitolo: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("\n Descrizione: ");
                        string overview = Console.ReadLine();

                        Console.WriteLine("\n Data di rilascio");
                        DateTime dateTime = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("\n Inserisci la softwarehouse, indicandone l'id ");

                        using (MovieContext db = new MovieContext())
                        {
                            List<SoftwareHouse> softwareHouse = db.Software_houses.ToList<SoftwareHouse>();
                            foreach (SoftwareHouse house in softwareHouse)
                            {
                                Console.WriteLine($"ID: {house.Id} , {house.Name} \n\n");
                            }

                            long softwareHouseId = int.Parse(Console.ReadLine());

                            Videogame newVideogame = new Videogame()
                            {
                                Name = name,
                                Overview = overview,
                                ReleaseDate = dateTime,
                                SoftwareHouseId = softwareHouseId
                            };
                            try
                            {
                                db.Add(newVideogame);
                                db.SaveChanges();
                                Console.WriteLine("\nVideogioco aggiunto con successo\n");
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                        }


                        break;

                    case 3:
                        Console.WriteLine("Ricerca un videogioco per ID, inserisci l'ID desiderato");
                        long idGame = int.Parse(Console.ReadLine());
                        try
                        {

                            using (MovieContext db = new MovieContext())
                            {
                                Videogame videogame = db.Videogames.FirstOrDefault(game => game.Id == idGame);

                                if (videogame != null)
                                {
                                    Console.WriteLine($"\nVideogioco trovato trovato: {videogame.Id} : {videogame.Name} --> {videogame.Overview}\n\n");
                                }
                                else
                                {
                                    // Il record con l'ID specificato non è stato trovato
                                    Console.WriteLine($"\nNessun record trovato con ID: {idGame}\n");
                                }
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }

                        break;

                    case 4:
                        Console.WriteLine("Ottieni una lista di videogame, che contengono la parola.......\n");
                        string word = Console.ReadLine();
                        try
                        {

                            using (MovieContext db = new MovieContext())
                            {


                                List<Videogame> videogames = db.Videogames
                                       .Where(game => game.Name.Contains(word))
                                       .Include(game => game.SoftwareHouse)
                                       .ToList();


                                if (videogames.Count > 0)
                                {
                                    Console.WriteLine("Videogiochi trovati ....\n");
                                    foreach (Videogame game in videogames)
                                    {
                                        Console.WriteLine($"-{game.Id} {game.Name}, {game.Overview}, Software House: {game.SoftwareHouse.Name}\n");

                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Nessun videogiochi trovato con la parola {word}");
                                }
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }


                        break;

                    case 5:
                        Console.WriteLine("Dammi un ID che cancelliamo un videogioco\n");
                        long deleteId = int.Parse( Console.ReadLine() );

                        try
                        {

                            using (MovieContext db = new MovieContext())
                            {
                                Videogame videogame = db.Videogames.Include(a => a.SoftwareHouse).FirstOrDefault(game => game.Id == deleteId);

                                if (videogame != null)
                                {
                                    db.Videogames.Remove(videogame);
                                    db.SaveChanges();
                                    Console.WriteLine($"\nVideogioco {videogame.Name}, con ID {videogame.Id}\n\r della softarehouse {videogame.SoftwareHouse.Name} eliminato correttamente\n");
                                }
                                else
                                {
                                    Console.WriteLine("Videogioco non trovato.");
                                }
                            }
                        }
                          catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                            

                        break;
                        case 6:
                        try
                        {
                            {
                                using (MovieContext db = new MovieContext())
                                {
                                    Console.WriteLine("In base a questa lista di softwarehouse che abbiamo in database, dove trovi tutti i dati relativi");

                                    List<SoftwareHouse> softwareHouses = db.Software_houses.ToList();

                                    if (softwareHouses.Count > 0)
                                    {
                                        foreach (SoftwareHouse softwareHouse in softwareHouses)
                                        {
                                            Console.WriteLine($"\n - ID: {softwareHouse.Id}, \n\rNome: {softwareHouse.Name} \n\rDescrizione: {softwareHouse.Description} \n\rCittà: {softwareHouse.City} \n\rPaese: {softwareHouse.Country}\n\r ");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Non ci sono softwarehouse");
                                    }
                                    long idChoise;

                                    do
                                    {
                                        Console.WriteLine("Dammi un ID di una softwarehouse, cosi stampiamo tutti i suoi videogames\n\r");

                                        if (long.TryParse(Console.ReadLine(), out  idChoise))
                                        {
                                            // Verifica se l'ID scelto è valido
                                            if (softwareHouses.Any(softwareHouse => softwareHouse.Id == idChoise))
                                            {
                                                break; 
                                            }
                                            else
                                            {
                                                Console.WriteLine("ID non valido. Riprova.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Input non valido. Inserisci un numero.");
                                        }
                                    } while (true);

                                    SoftwareHouse softwarehouseChoise = db.Software_houses.FirstOrDefault(softwareHouse => softwareHouse.Id == idChoise);

                                    List<Videogame> videogames = db.Videogames.Where(game => game.SoftwareHouseId == idChoise).ToList();

                                    if (videogames.Count > 0)
                                    {
                                        Console.WriteLine($"La softwarehouse {softwarehouseChoise.Name} ha creato questi videogames:\n\r");

                                        foreach (Videogame game in videogames)
                                        {
                                            Console.WriteLine($"\n\r {game.Name}, {game.Overview}\n\r");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{softwarehouseChoise.Name} non ha sviluppato nessun videogioco");
                                    }

                                }
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }

                        break;

                        default:
                        Console.WriteLine("Non hai scelto nessuna delel opzioni, stai per uscire...");
                        break;
                }
            }
        }
    }
}
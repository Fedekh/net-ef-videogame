//Vogliamo modificare l’esercizio di ieri rimuovendo le parti gestite con SqlClient e implementandole con Entity Framework.
//Devono essere presenti tutte le funzionalità dell’esercizio originale.
//Aggiungiamo anche un’altra voce al menu :
//inserisci una nuova software house
//Fatto questo, ogni volta che creiamo un nuovo videogioco dobbiamo abbinargli la software house che l’ha prodotto (che dobbiamo aver precedentemente inserito in tabella), chiedendo all’utente l’id della software house e impostandolo nell’entity del videogame.
//Realizzare quindi tutte le entity e le migration necessarie per creare il database e implementare tutte le richieste dell’esercizio.
//BONUS : aggiungere un’altra voce di menu
//stampa tutti i videogiochi prodotti da una software house (all’utente verrà chiesto l’id della software house della quale mostrare i videogame)

using net_ef_videogame.Classes;
using net_ef_videogame.Database;
using net_ef_videogame.Helpers;
using System;

namespace net_ef_videogame
{
  class Program
    {
        public static void Main(string[] args)
        {
            int number = 1;
            while (number > 0 && number < 5)
            {
                Console.WriteLine("Buongiorno e benvenuto al nostro gestionale. Scegli un opzione\r\n\n1) Inserisci Software house\r\n2) Inserire un nuovo videogioco\r\n3) Ricercare un videogioco per id\r\n4) Ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input\r\n5) Cancellare un videogioco\r\n6) Chiudere il programma\r\n");
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
                        Console.WriteLine("Ricerca un videogioco per ID");

                        break;

                    case 4:
                        Console.WriteLine("Non hai scelto nessuna delel opzioni, stai per uscire...");


                        break;

                        default:
                        Console.WriteLine("Non hai scelto nessuna delel opzioni, stai per uscire...");
                        break;
                }
            }
        }
    }
}
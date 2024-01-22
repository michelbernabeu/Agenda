namespace Agenda
{

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
    {
        static List<string> agenda = new List<string>();
        static string agendaFilePath = "agenda.txt";

        static void Main()
        {
            Console.WriteLine("Benvingut a l'agenda!");

            while (true)
            {
                ShowMenu();
                char option = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (option)
                {
                    case '1':
                        AddUser();
                        break;
                    case '2':
                        SearchUser();
                        break;
                    case '3':
                        ModifyUser();
                        break;
                    case '4':
                        DeleteUser();
                        break;
                    case '5':
                        ShowAgenda();
                        break;
                    case '6':
                        SortAgenda();
                        break;
                    case 'Q':
                    case 'q':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opció no vàlida. Si us plau, torna a intentar-ho.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nMENU:");
            Console.WriteLine("1. Donar d'alta usuari");
            Console.WriteLine("2. Recuperar usuari");
            Console.WriteLine("3. Modificar usuari");
            Console.WriteLine("4. Eliminar usuari");
            Console.WriteLine("5. Mostrar agenda");
            Console.WriteLine("6. Ordenar agenda");
            Console.WriteLine("Q. Sortir");
            Console.Write("Tria una opció: ");
        }

        static void AddUser()
        {
            Console.WriteLine("\nDonar d'alta usuari:");
            string[] userData = GetUserData();

            // Validar i processar dades aquí

            // Afegir les dades de l'usuari a la llista
            agenda.Add(string.Join(",", userData));

            // Guardar la llista d'usuaris en el fitxer
            SaveToFile();
        }

        static void SearchUser()
        {
            Console.WriteLine("\nRecuperar usuari:");
            Console.Write("Introdueix el nom de l'usuari: ");
            string searchName = Console.ReadLine().ToLower();

            // Buscar l'usuari a la llista
            List<string> matchingUsers = agenda
                .Where(u => u.Split(',')[0].ToLower().StartsWith(searchName))
                .ToList();

            if (matchingUsers.Any())
            {
                Console.WriteLine("\nUsuaris trobats:");
                foreach (string user in matchingUsers)
                {
                    Console.WriteLine(user);
                }
            }
            else
            {
                Console.WriteLine("L'usuari no existeix.");

                Console.Write("Vols buscar un altre usuari? (Si/No): ");
                string response = Console.ReadLine().ToLower();

                if (response == "no")
                    return;
            }
        }

        static void ModifyUser()
        {
            Console.WriteLine("\nModificar usuari:");
            Console.Write("Introdueix el nom de l'usuari a modificar: ");
            string searchName = Console.ReadLine().ToLower();

            // Buscar l'usuari a la llista
            int index = agenda.FindIndex(u => u.Split(',')[0].ToLower().StartsWith(searchName));

            if (index != -1)
            {
                while (true)
                {
                    Console.WriteLine("\nUsuari trobat:");
                    Console.WriteLine(agenda[index]);

                    // Demanar quina dada es vol modificar

                    // Modificar la dada i validar-la

                    // Actualitzar la llista d'usuaris i guardar al fitxer

                    // Preguntar si es vol continuar modificant
                    Console.Write("\nVols continuar modificant aquest usuari? (Si/No): ");
                    string response = Console.ReadLine().ToLower();

                    if (response == "no")
                        break;
                }
            }
            else
            {
                Console.WriteLine("L'usuari no existeix.");

                Console.Write("Vols buscar un altre usuari? (Si/No): ");
                string response = Console.ReadLine().ToLower();

                if (response == "no")
                    return;
            }
        }

        static void DeleteUser()
        {
            Console.WriteLine("\nEliminar usuari:");
            Console.Write("Introdueix el nom de l'usuari a eliminar: ");
            string searchName = Console.ReadLine().ToLower();

            // Buscar l'usuari a la llista
            int index = agenda.FindIndex(u => u.Split(',')[0].ToLower().StartsWith(searchName));

            if (index != -1)
            {
                // Eliminar l'usuari de la llista
                agenda.RemoveAt(index);

                // Guardar la llista actualitzada al fitxer
                SaveToFile();

                Console.WriteLine("Usuari eliminat amb èxit.");
            }
            else
            {
                Console.WriteLine("L'usuari no existeix.");

                Console.Write("Vols buscar un altre usuari? (Si/No): ");
                string response = Console.ReadLine().ToLower();

                if (response == "no")
                    return;
            }
        }

        static void ShowAgenda()
        {
            Console.WriteLine("\nMostrar agenda:");
            // Mostrar la llista d'usuaris
            foreach (string user in agenda)
            {
                Console.WriteLine(user);
            }
        }

        static void SortAgenda()
        {
            Console.WriteLine("\nOrdenar agenda:");
            // Ordenar la llista d'usuaris
            agenda.Sort();

            // Actualitzar el fitxer amb la llista ordenada
            SaveToFile();
        }

        static string[] GetUserData()
        {
            // Demanar les dades de l'usuari i retornar-les com un array de cadenes
            Console.Write("Nom: ");
            string firstName = Console.ReadLine();
            Console.Write("Cognom: ");
            string lastName = Console.ReadLine();
            Console.Write("DNI: ");
            string dni = Console.ReadLine();
            Console.Write("Telèfon: ");
            string phone = Console.ReadLine();
            Console.Write("Data de naixement (yyyy-MM-dd): ");
            string birthDate = Console.ReadLine();
            Console.Write("Correu electrònic: ");
            string email = Console.ReadLine();

            return new string[] { firstName, lastName, dni, phone, birthDate, email };
        }

        static void SaveToFile()
        {
            // Guardar la llista d'usuaris en el fitxer
            File.WriteAllLines(agendaFilePath, agenda);
        }
    }

}

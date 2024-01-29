using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MenuAgenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            MostrarMenu();
        }
        static void MostrarMenu()
        {
            int opcio, numero1, numero2;
            do
            {
                Console.Clear();
                Console.Write(CrearMenu());
                Console.Write("Escull una opcio: ");
                opcio = Convert.ToChar(Console.ReadLine());
                switch (opcio)
                {
                    case '1':
                        Console.Clear();
                        DonarAlta();
                        break;
                    case '2':
                        Console.Clear();
                        RecuperarUsuari();
                        break;
                    case '3':
                        Console.Clear();
                        ModificarUsuari();
                        break;
                    case '4':
                        Console.Clear();
                        EliminarUsuari();
                        break;
                    case '5':
                        Console.Clear();
                        MostrarAgenda();
                        break;
                    case '6':
                        Console.Clear();
                        OrdenarAgenda();
                        break;
                    case 'q':
                        break;
                    case 'Q':
                        break;

                }
            } while (opcio != 'Q' && opcio != 'q');

        }
        static string CrearMenu()
        {
            string TextMenu =
               "╔══════════════════════════════════════════════════╗\n" +
               "║                      * Agenda *                  ║ \n" +
               "╠══════════════════════════════════════════════════╣ \n" +
               "║              1) Donar alta Usuari                ║ \n" +
               "║              2) Recuperar Usuari                 ║ \n" +
               "║              3) Modificar Usuari                 ║ \n" +
               "║              4) Esborrar Usuari                  ║ \n" +
               "║              5) Mostrar Agenda                   ║ \n" +
               "║              6) Ordenar Agenda                   ║ \n" +
               "║                                                  ║ \n" +
               "║              q) Sortir                           ║ \n" +
               "╚══════════════════════════════════════════════════╝ \n";

            return TextMenu;
        }
        // EscriureFitxer: Crear el fitxer i escriure el que ha guardat de les variables
        static void EscriureFitxer(string nom, string cognom, string dni, string telefon, DateTime dataNaix, string correu)
        {
            StreamWriter sW = new StreamWriter("agenda.txt", true);
            sW.WriteLine($"{nom};{cognom};{dni};{telefon};{dataNaix.ToString("d")};{correu}\r");
            sW.Close();

        }

        // DonarAlta: Demana strings i emmagatzema els valors dintre del fitxer
        static void DonarAlta()
        {
            Console.Write("Introdueix el nom: ");
            string nom = Console.ReadLine();
            Console.Write("Introdueix el cognom: ");
            string cognom = Console.ReadLine();
            Console.Write("Introdueix el DNI: ");
            string dni = Console.ReadLine();
            Console.Write("Introdueix el telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("Introdueix la data de naixament: (dd-mm-yyyy)");
            DateTime dataNaix = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Introdueix el teu correu electrónic:");
            string correuElectronic = Console.ReadLine();
            Console.Clear();
            EscriureFitxer(ValidarNom(nom), ValidarCognom(cognom), ValidarDni(dni), ValidarTelefon(telefon), ValidarDataNaixament(dataNaix), ValidarCorreu(correuElectronic));
            Return();
        }

        // RecuperarUsuari: Cerca l'usuari que vulguis i et diu si existeix o no a la agenda
        static void RecuperarUsuari()
        {
            Return();
        }
        static string RecuperarUsuari(string nomUsuari)
        {
            char trobarUsuari = 'S';
            bool trobat;
            while (trobarUsuari != 'N' && trobarUsuari != 'n')
            {
                var linea = File.ReadLines("agenda.txt")
                    .Select(linea => linea.Split(';')[0]).ToList(); 

                trobat = linea.Contains(nomUsuari);

                if (trobat)
                {
                    trobarUsuari = 'N';
                }
                else
                {
                    Console.Write("Usuari no trobat. Vols trobar un altre usuari? (S/N)");
                    trobarUsuari = Convert.ToChar(Console.ReadLine());
                }
            }
            return nomUsuari;
        }
        static void ModificarUsuari()
        {
            char Finalitzar = 'S';
            Console.Write("Quin usuari vols trobar? ");
            string nomUsuari = Console.ReadLine();
            while (Finalitzar != 'N' && Finalitzar != 'n')
            {
                string usuari = RecuperarUsuari(nomUsuari);
                Console.Write("Quina dada vols modificar? ");
                string dada = Console.ReadLine();

                Console.Write("Introdueix el nou valor: ");
                string nouValor = Console.ReadLine();

                var dadesUsuari = usuari.Split(';');

                switch (dada.ToLower())
                {
                    case "nom":
                        dadesUsuari[0] = nouValor;
                        break;
                    case "cognom":
                        dadesUsuari[1] = nouValor;
                        break;
                    case "dni":
                        dadesUsuari[2] = nouValor;
                        break;
                    case "telefon":
                        dadesUsuari[3] = nouValor;
                        break;
                    case "datanaixament":
                        dadesUsuari[4] = nouValor;
                        break;
                    case "correu":
                        dadesUsuari[5] = nouValor;
                        break;
                    default:
                        Console.WriteLine("Dada no existent.");
                        return;
                }
                usuari = string.Join(";", dadesUsuari);
                var lineas = File.ReadAllLines("agenda.txt").ToList();
                lineas[lineas.IndexOf(usuari)] = usuari;
                File.WriteAllLines("agenda.txt", lineas);
                Console.WriteLine($"Vols tornar a modifcar alguna dada de {nomUsuari}? (S/N)");
                Finalitzar = Convert.ToChar(Console.ReadLine());
            }
        }
        static void EliminarUsuari()
        {
            char tornarEliminarUsuari = 'S';
            string nomUsuari, usuario;
            while (tornarEliminarUsuari != 'n' && tornarEliminarUsuari != 'N')
            {
                Console.Write("Quin usuari vols eliminar? ");
                nomUsuari = Console.ReadLine();

                usuario = RecuperarUsuari(nomUsuari);

                var lineas = File.ReadAllLines("agenda.txt").ToList();
                lineas.RemoveAll(linea => linea.Split(';')[0].Equals(nomUsuari));
                File.WriteAllLines("agenda.txt", lineas.Where(linea => !string.IsNullOrWhiteSpace(linea)));

                Console.WriteLine($"Usuari {nomUsuari} eliminat amb èxit.");
                Console.Write("Vols tornar a eliminar un usuari? (S/N)");
                tornarEliminarUsuari = Convert.ToChar(Console.ReadLine());
            }

        }
        static void MostrarAgenda()
        {
            OrdenarAgenda();
            var lineas = File.ReadLines("agenda.txt")
                .Select(linea => linea.Split(';'))
                .Where(datos => datos.Length >= 4)
                .Select(datos => new
                {
                    Nom = datos[0],
                    Telefon = datos[3]
                })
                .OrderBy(usuario => usuario.Nom)
                .ToList();

            for (int i = 0; i < lineas.Count; i++)
            {
                Console.WriteLine($"Nom: {lineas[i].Nom}, Teléfon: {lineas[i].Telefon}");
            }
            Return();
        }

        // OrdenarAgenda: Ordena el contingut de la agenda segons el nom de la persona
        static void OrdenarAgenda()
        {
            var lineas = File.ReadLines("agenda.txt")
                .Select(linea => new
                {
                    Datos = linea.Split(';'),
                    Nombre = linea.Split(';')[0]
                })
                .OrderBy(usuario => usuario.Nombre)
                .Select(usuario => string.Join(";", usuario.Datos))
                .ToList();

            File.WriteAllLines("agenda.txt", lineas);
            Console.WriteLine("Agenda ordenada!");
            Return();
        }

        // Return: Temporitzador de 5s que et torna al Menú
        static void Return()
        {
            int i = 5;
            while (i != 0)
            {
                Console.Write("\r");
                Console.Write($"Tornant al menu {i}s");
                Thread.Sleep(1000);
                i--;
            }
        }

        // Validacions: ValidarNom, ValidarCognom, ValidarDNI, ValidarTelefon, ValidarDnaixa, ValidarCorreuElectronic
        static string ValidarNom(string nom)
        {
            string resultat = "";
            nom = nom.ToLower().Trim();
            for (int i = 0; i < nom.Length; i++)
            {
                if (char.IsLetter(nom[i]))
                {
                    resultat += nom[i];
                }
            }
            if (resultat.Length != 0)
                resultat = resultat.Substring(0, 1).ToUpper() + resultat.Substring(1);
            return resultat;
        }
        static string ValidarCognom(string cognom)
        {
            string resultat = "";
            cognom = cognom.ToLower().Trim();
            for (int i = 0; i < cognom.Length; i++)
            {
                if (char.IsLetter(cognom[i]))
                {
                    resultat += cognom[i];
                }
            }
            if (resultat.Length != 0)
                resultat = resultat.Substring(0, 1).ToUpper() + resultat.Substring(1);
            return resultat;
        }
        static string ValidarDni(string dni)
        {
            Console.Clear();
            bool dniValid = false;
            while (!dniValid)
            {
                var dniRegex = new Regex(@"^\d{8}[A-Z]$");
                if (!dniRegex.IsMatch(dni))
                {
                    Console.WriteLine("Aquest DNI no es valid.");
                    Console.Write("Introdueix un altre DNI:");
                    dni = Console.ReadLine();
                }
                else
                {
                    dniValid = true;
                }
            }
            return dni;
        }
        static string ValidarTelefon(string telefon)
        {
            if (telefon.Length != 9)
            {
                Console.WriteLine("Telèfon incorrecte. Intenteu-ho de nou.");
                Console.Write("Entra el nou teléfon: ");
                return ValidarTelefon(Console.ReadLine());
            }
            return telefon;
        }

        static DateTime ValidarDataNaixament(DateTime dataNaix)
        {
            Console.Clear();
            bool dataValida = false;
            while (!dataValida)
            {
                if (dataNaix > DateTime.Now)
                {
                    Console.WriteLine("Data de naixament incorrecte. Intenteu-ho de nou.");
                    Console.Write("Entra una nova data de naixament: ");
                    dataNaix = Convert.ToDateTime(Console.ReadLine());
                }
                else
                {
                    dataValida = true;
                }
            }
            return dataNaix;
        }
        static string ValidarCorreu(string correu)
        {
            Console.Clear();
            bool correuValid = false;
            while (!correuValid)
            {
                var correuRegex = new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z]{3,}\.(com|es)$");
                if (!correuRegex.IsMatch(correu))
                {
                    Console.WriteLine("Correu incorrecte. Intenteu-ho de nou.");
                    Console.Write("Entra el nou correu: ");
                    correu = Console.ReadLine();
                }
                else
                    correuValid = true;
            }
            return correu;
        }
    }
}

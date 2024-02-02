using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MenuAgenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
        // Mostra la Agenda
        static void MostrarAgenda()
        {
            Console.Clear();
            string contingut = File.ReadAllText("agenda.txt");

            string[] lineas = contingut.Split('\n');

            Console.WriteLine("Noms i Telèfons de l'Agenda:");
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║   Nom            Telefono      ║");
            Console.WriteLine("╠════════════════════════════════╬");

            for (int i = 0; i < lineas.Length; i++)
            {
                int indexOfSeparator = lineas[i].IndexOf(';');
                string nombre = indexOfSeparator != -1 ? lineas[i].Substring(0, indexOfSeparator) : "";
                string telefono = indexOfSeparator != -1 && lineas[i].Length > indexOfSeparator + 1 ? lineas[i].Substring(indexOfSeparator + 1) : "";

                Console.WriteLine($"║ {nombre,-30} {telefono,-20} ║");
            }

            Console.WriteLine("╚════════════════════════════════╩════════════╝");

            Return();
        }


        // OrdenarAgenda: Ordena el contingut de la agenda segons el nom de la persona
        static void OrdenarAgenda()
        {
            string contingut = File.ReadAllText("agenda.txt");
            int principistring = 0;
            int finalstring = 0;

            while (principistring < contingut.Length)
            {
                finalstring = contingut.IndexOf('\n', principistring);

                if (finalstring == -1)
                    finalstring = contingut.Length;

                string linia = contingut.Substring(principistring, finalstring - principistring);

                int punticoma = linia.IndexOf(';');
                string nom = punticoma != -1 ? linia.Substring(0, punticoma) : linia;

                int posicio = principistring;
                while (posicio > 0 && punticoma != -1 && string.Compare(nom, contingut.Substring(posicio, contingut.IndexOf('\n', posicio) - posicio)) < 0)
                    posicio = contingut.IndexOf('\n', posicio) + 1;

                contingut = contingut.Remove(principistring, finalstring - principistring);
                contingut = contingut.Insert(posicio, linia);
                principistring = posicio + 1;
            }
            File.WriteAllText("agenda.txt", contingut);

            Console.WriteLine("Agenda ordenada!");
            Return();
        }


        // OrdenarAgendaAux: Ordena la agenda sense mostrar res per a que en el metode de mostrar agenda estigui ordenat
        static void OrdenarAgendaAux()
        {
            string contingut = File.ReadAllText("agenda.txt");

            // Verificar si el archivo está vacío o no contiene saltos de línea
            if (string.IsNullOrEmpty(contingut) || !contingut.Contains("\n"))
            {
                Console.WriteLine("La agenda está vacía o no contiene saltos de línea para ordenar.");
                return;
            }

            int principiLinia = 0;
            int finalLinia = 0;

            while (principiLinia < contingut.Length)
            {
                finalLinia = contingut.IndexOf('\n', principiLinia);

                if (finalLinia == -1)
                    finalLinia = contingut.Length;

                string linia = contingut.Substring(principiLinia, finalLinia - principiLinia);

                // Verificar si la línea contiene al menos un punto y coma
                int punticoma = linia.IndexOf(';');
                if (punticoma != -1)
                {
                    string nom = linia.Substring(0, punticoma);

                    int posicio = principiLinia;
                    while (posicio > 0 && posicio + linia.Length < contingut.Length &&
                           string.Compare(nom, contingut.Substring(posicio, linia.Length)) < 0)
                    {
                        posicio = contingut.IndexOf('\n', posicio) + 1;
                    }

                    contingut = contingut.Remove(principiLinia, finalLinia - principiLinia);
                    contingut = contingut.Insert(posicio, linia);
                    principiLinia = posicio + 1;
                }
            }

            File.WriteAllText("agenda.txt", contingut);
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

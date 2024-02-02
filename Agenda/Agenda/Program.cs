using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MenuAgenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuLlistat();
            Menu();
        }
        static void Menu()
        {
            int opcio, numero1, numero2;
            do
            {
                Console.Clear();
                Console.Write(MenuLlistat());
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
        static string MenuLlistat()
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
        static void ModificarFitxer(string nom, string cognom, string dni, string telefon, DateTime dataNaix, string correu)
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
            ModificarFitxer(ValidarNom(nom), ValidarCognom(cognom), ValidarDni(dni), ValidarTelefon(telefon), ValidarDataNaixament(dataNaix), ValidarCorreu(correuElectronic));
            Return();
        }
        // RecuperarUsuari: Demana el nom d'un usuari, i si el troba, mostra tota la seva informació
        static void RecuperarUsuari()
        {
            Console.Write("Introdueix el nom de l'usuari a buscar: ");
            string nomUsuari = Console.ReadLine();

            char trobarUsuari = 'S';
            bool usuariTrobat = false;

            while (trobarUsuari != 'N' && trobarUsuari != 'n' && !usuariTrobat)
            {
                string contingutFitxer = File.ReadAllText("agenda.txt");

                // Buscar el nombre de usuario en el contenido del archivo
                int posicio = contingutFitxer.IndexOf($"{nomUsuari};");

                if (posicio != -1)
                {
                    usuariTrobat = true;
                }
                else
                {
                    Console.Write("Usuari no trobat. Vols trobar un altre usuari? (S/N)");
                    trobarUsuari = Convert.ToChar(Console.ReadLine());
                }
            }

            if (usuariTrobat)
            {
                Console.WriteLine("Usuari trobat amb éxit");
                string contingutFitxer = File.ReadAllText("agenda.txt");
                int posicio = contingutFitxer.IndexOf($"{nomUsuari};");
                int finalLinea = contingutFitxer.IndexOf('\r', posicio);

                if (finalLinea != -1)
                {
                    string lineaUsuario = contingutFitxer.Substring(posicio, finalLinea - posicio);

                    // Dividir la línea en sus componentes
                    string[] dadesUsuari = lineaUsuario.Split(';');

                    // Mostrar los detalles del usuario
                    Console.WriteLine($"Nom: {dadesUsuari[0]}");
                    Console.WriteLine($"Cognom: {dadesUsuari[1]}");
                    Console.WriteLine($"DNI: {dadesUsuari[2]}");
                    Console.WriteLine($"Telefon: {dadesUsuari[3]}");
                    Console.WriteLine($"Data Naixament: {dadesUsuari[4]}");
                    Console.WriteLine($"Correu: {dadesUsuari[5]}");
                }
            }
            else
            {
                Console.WriteLine($"L'usuari {nomUsuari} no existeix a la agenda.");
            }

            Return();
        }
        // ModificarUsuari: demana quin usuari vols modificar i la dada, si no es correcte (es a dir, no passa per la validació) ho torna a demanar.
        static void ModificarUsuari()
        {
            char finalitzar = 'S';
            Console.Write("Quin usuari vols trobar? ");
            string nomUsuari = Console.ReadLine();

            while (finalitzar != 'N' && finalitzar != 'n')
            {
                string contingutFitxer = File.ReadAllText("agenda.txt");

                int posiciousuari = contingutFitxer.IndexOf(nomUsuari);
                if (posiciousuari == -1)
                {
                    Console.WriteLine($"Usuari {nomUsuari} no trobat.");
                    return;
                }

                int principivalor = contingutFitxer.LastIndexOf(Environment.NewLine, posiciousuari) + Environment.NewLine.Length;
                int finalvalor = contingutFitxer.IndexOf(Environment.NewLine, posiciousuari);

                string usuari = contingutFitxer.Substring(principivalor, finalvalor - principivalor);

                Console.Write("Quina dada vols modificar? ");
                string dada = Console.ReadLine();

                Console.Write("Introdueix el nou valor: ");
                string nouvalor = Console.ReadLine();
                if (dada.ToLower() == "nom")
                {
                    ValidarNom(nouvalor);
                }
                else if (dada.ToLower() == "cognom")
                {
                    ValidarCognom(nouvalor);
                }
                else if (dada.ToLower() == "dni")
                {
                    ValidarDni(nouvalor);
                }
                else if (dada.ToLower() == "telefon")
                {
                    ValidarTelefon(nouvalor);
                }
                else if (dada.ToLower() == "correu")
                {
                    ValidarCorreu(nouvalor);
                }
                int posicioDada = -1;

                switch (dada.ToLower())
                {
                    case "nom":
                        posicioDada = 0;
                        break;
                    case "cognom":
                        posicioDada = usuari.IndexOf(';', usuari.IndexOf(';') + 1) + 1;
                        break;
                    case "dni":
                        posicioDada = usuari.IndexOf(';', usuari.IndexOf(';', usuari.IndexOf(';') + 1) + 1) + 1;
                        break;
                    case "telefon":
                        posicioDada = usuari.IndexOf(';', usuari.IndexOf(';', usuari.IndexOf(';', usuari.IndexOf(';') + 1) + 1) + 1) + 1;
                        break;
                    case "datanaixament":
                        posicioDada = usuari.IndexOf(';', usuari.IndexOf(';', usuari.IndexOf(';', usuari.IndexOf(';', usuari.IndexOf(';') + 1) + 1) + 1) + 1) + 1;
                        break;
                    case "correu":
                        posicioDada = usuari.LastIndexOf(';') + 1;
                        break;
                    default:
                        Console.WriteLine("Dada no existent.");
                        return;
                }

                string novaLinia = usuari.Substring(0, posicioDada) + nouvalor + usuari.Substring(posicioDada + nouvalor.Length);
                contingutFitxer = contingutFitxer.Remove(principivalor, finalvalor - principivalor).Insert(principivalor, novaLinia);
                File.WriteAllText("agenda.txt", contingutFitxer);

                Console.WriteLine($"Vols tornar a modificar alguna dada de {nomUsuari}? (S/N)");
                finalitzar = Convert.ToChar(Console.ReadLine());
            }
        }
        // EliminarUsuari: elimina un usuari de la agenda
        static void EliminarUsuari()
        {
            char tornarEliminarUsuari = 'S';

            while (tornarEliminarUsuari != 'n' && tornarEliminarUsuari != 'N')
            {
                Console.Write("Quin usuari vols eliminar? ");
                string nomUsuari = Console.ReadLine();

                string contingutFitxer = File.ReadAllText("agenda.txt");

                if (contingutFitxer.Contains(nomUsuari))
                {
                    int posiciousuari = contingutFitxer.IndexOf(nomUsuari);
                    int posicioparaula = contingutFitxer.LastIndexOf(Environment.NewLine, posiciousuari) + Environment.NewLine.Length;
                    int finalparaula = contingutFitxer.IndexOf(Environment.NewLine, posiciousuari);

                    string usuari = contingutFitxer.Substring(posicioparaula, finalparaula - posicioparaula);

                    contingutFitxer = contingutFitxer.Remove(posicioparaula, finalparaula - posicioparaula).Insert(posicioparaula, "");
                    File.WriteAllText("agenda.txt", contingutFitxer);

                    Console.WriteLine($"Usuari {nomUsuari} eliminat amb èxit.");
                }
                else
                {
                    Console.WriteLine($"Usuari {nomUsuari} no trobat. No s'ha eliminat cap usuari.");
                }

                Console.Write("Vols tornar a eliminar un usuari? (S/N)");
                tornarEliminarUsuari = Convert.ToChar(Console.ReadLine());
                if (tornarEliminarUsuari == 'N' || tornarEliminarUsuari == 'n') 
                {
                    Console.WriteLine("Sortint amb exit!");
                    Return();
                }
            }
        }

        // Mostra la Agenda
        static void MostrarAgenda()
        {
            OrdenarAgendaAux();
            Console.Clear();
            string contingut = File.ReadAllText("agenda.txt");

            string[] lineas = contingut.Split('\n');

            Console.WriteLine("Noms i Telèfons de l'Agenda:");
            Console.WriteLine("╔══════════════════════╦══════════════════════════════╗");
            Console.WriteLine("║   Nom                ║         Telèfon              ║");
            Console.WriteLine("╠══════════════════════╩══════════════════════════════╣");

            for (int i = 0; i < lineas.Length; i++)
            {
                int punticoma = lineas[i].IndexOf(';');
                string nombre = punticoma != -1 ? lineas[i].Substring(0, punticoma) : "";
                string telefono = punticoma != -1 ? lineas[i].Split(';')[3] : "";

                Console.WriteLine($"║ {nombre,-30} {telefono,-20} ║");
            }

            Console.WriteLine("╚═════════════════════════════════════════════════════╝");
            Return();
        }
        // OrdenarAgenda: Ordena el contingut de la agenda segons el nom de la persona
        static void OrdenarAgenda()
        {
            string contingut = File.ReadAllText("agenda.txt");
            List<string> lineas = new List<string>(contingut.Split('\n'));

            lineas.Sort((linea1, linea2) =>
            {
                string nomUsuari1 = TreureNomUsuari(linea1);
                string nomUsuari2 = TreureNomUsuari(linea2);

                return string.Compare(nomUsuari1, nomUsuari2, StringComparison.OrdinalIgnoreCase);
            });

            File.WriteAllLines("agenda.txt", lineas);

            Console.WriteLine("Agenda ordenada per nom d'usuari!");
            Return();
        }
        static string TreureNomUsuari(string linea)
        {
            int punticoma = linea.IndexOf(';');
            return punticoma != -1 ? linea.Substring(0, punticoma) : linea;
        }

        // OrdenarAgendaAux: Ordena la agenda sense mostrar res per a que en el metode de mostrar agenda estigui ordenat
        static void OrdenarAgendaAux()
        {
            string contingut = File.ReadAllText("agenda.txt");
            List<string> lineas = new List<string>(contingut.Split('\n'));

            lineas.Sort((linea1, linea2) =>
            {
                string nomUsuari1 = TreureNomUsuari(linea1);
                string nomUsuari2 = TreureNomUsuari(linea2);

                return string.Compare(nomUsuari1, nomUsuari2, StringComparison.OrdinalIgnoreCase);
            });

            File.WriteAllLines("agenda.txt", lineas);
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

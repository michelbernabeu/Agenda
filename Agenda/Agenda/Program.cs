namespace Agenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char opcio;

            Console.WriteLine("Benvingut a la teva agenda!");
            Console.WriteLine("MENU");
            Console.WriteLine("1. Donar d'alta usuari");
            Console.WriteLine("2. Recuperar usuari");
            Console.WriteLine("3. Modificar usuari");
            Console.WriteLine("4. Eliminar usuari");
            Console.WriteLine("5. Mostrar agenda");
            Console.WriteLine("6. Ordenar agenda");
            Console.WriteLine("Q. Sortir menú");

            opcio = Console.ReadKey().KeyChar;
        }
    }
}

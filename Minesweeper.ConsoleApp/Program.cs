namespace Minesweeper.ConsoleApp
{
    internal class Program 
    {
        static ConsolePlatform consolePlatform;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Minesweeper!");
            Console.WriteLine("***********************");
            Console.WriteLine("To start the game press any key");
            Console.ReadKey();

            consolePlatform = new ConsolePlatform();
            consolePlatform.StartGame();
        }

    }
}
using System.ComponentModel;

namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            Game game = new Game();
            while (run == true)
            {
                switch (game.Status)
                {
                    case "main":
                        game.MainMenu();
                        break;
                    case "preGame":
                        game.PreGame();
                        break;
                    case "game":
                        game.Run();
                        break;
                    case "close":
                        run = false;
                        break;
                    default:
                        game.MainMenu();
                        break;
                }
            }
        }
    }
}
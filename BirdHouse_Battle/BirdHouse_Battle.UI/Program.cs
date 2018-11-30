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
                        game.Arena = new Model.Arena();
                        game.PreGame();
                        break;
                    case "game":
                        game.Run();
                        break;
                    case "close":
                        run = false;
                        break;
                    case "pause":
                        game.PauseMenu();
                  
                        break;
                    default:
                        game.MainMenu();
                        break;
                }
            }
        }
    }
}
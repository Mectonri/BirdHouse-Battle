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
                    //main menu
                    case "main":
                        game.MainMenu();
                        break;
                        
                    case "preGame"://game prep menu
                        game.Arena = new Model.Arena();
                        game.PreGame();
                        break;

                        
                    case "game"://game screen
                        game.Run();
                        break;
                        
                        
                    case "close"://close the window
                        game.ExitMenu();
                        //run = false;
                        break;
                        
                    case "pause"://pause the game screen
                        game.PauseMenu();
                        break;

                    case "ended"://when the game has ended
                        game.ResultWindow();
                        break;

                    default:
                        game.MainMenu();
                        break;
                }
                
            }
        }
    }
}
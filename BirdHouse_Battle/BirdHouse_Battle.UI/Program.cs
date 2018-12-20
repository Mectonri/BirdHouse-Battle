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
                    case "preGame":
                        game.NewArena();
                        game.PreGame();
                        break;
                    case "quickGame":
                        game.NewArena();
                        game.RandomGame(game.Arena);
                        break;
                    case "game"://game screen
                        game.Run();
                        break;
                        
                    case "close"://close the window
                        game.ExitMenu();
                        break;
                        
                    //case "pause"://pause the game screen
                    //    if ( game.Paused == true ) { game.PauseMenu(); }
                        
                    //    break;

                    case "ended"://when the game has ended
                        game.ResultWindow();
                        break;

                    case "credit"://credit
                        game.CreditPage();
                        break;

                    default:
                        game.MainMenu();
                        break;
                }
                
            }
        }
    }
}
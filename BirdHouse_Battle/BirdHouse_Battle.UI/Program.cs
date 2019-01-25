namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            Game game = new Game();
            //game.LevelWritte();
            string path = "";
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
                        game.FillRandom(game.Arena);
                        
                        break;
                    case "game"://game screen
                        game.Run("play", path);
                        break;

                    case "elderGame":
                        path = game.ElderGame();
                        break;

                    case "replay":
                        game.Run("replay", path, true);
                        break;

                    case "history"://history mode
                        game.LevelSelectionMenu();
                        break;

                    case "historyPreGame":
                        game.HistoryPreGame(game.Arena);//should take arena
                        break;

                    case "historyGame":
                       game.RunHistory(path);
                        break; 

                    case "return"://close the window
                        game.ReturnMenu();
                        break;
                        
                    case "ended"://when the game has ended
                        game.ResultWindow();
                        break;

                    case "historyEnded":
                        game.HistoryResultWindow();
                        break;

                    case "credit"://credit
                        game.CreditPage();
                        break;

                    case "quit":
                        game.QuitPage();
                        break;

                    case "placement":
                        game.Placement();
                        break;

                    default:
                        game.MainMenu();
                        break;
                }
            }
        }
    }
}
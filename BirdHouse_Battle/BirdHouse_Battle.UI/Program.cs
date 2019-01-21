namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            Game game = new Game();
            //game.ElderGame();
            string LastStatut = "";
            string path = "";
            while (run == true)
            {
                switch (game.Status)
                {
                    //main menu
                    case "main":
                        LastStatut = "main";
                        game.MainMenu();
                        break;
                    case "preGame":
                        LastStatut = "preGame";
                        game.NewArena();
                        game.PreGame();
                        break;
                    case "quickGame":
                        LastStatut = "quickGame";
                        game.NewArena();
                        game.FillRandom(game.Arena);
                        
                        break;
                    case "game"://game screen
                        game.Run("play", path);
                        break;

                    case "elderGame":
                        LastStatut = "elderGame";
                        /*path = */game.ElderGame();
                        break;

                    case "replay":
                        game.Run("replay", path);
                        break;

                    case "historyGame":
                       game.RunHistory(path);
                        break; 
                    case "return"://close the window
                        LastStatut = "return";
                        game.ReturnMenu();
                        break;
                        
                    case "history"://history mode
                        LastStatut = "history";
                        game.LevelSelectionMenu();
                        break;
                    case "historyPreGame":
                        //game.HistoryPreGame();

                        break;

                    case "ended"://when the game has ended
                        LastStatut = "ended";
                        game.ResultWindow();

                        break;
                    case "historyEnded":
                        game.HistoryResultWindow();
                        break;

                    case "credit"://credit
                        LastStatut = "credit";
                        game.CreditPage();

                        break;

                    case "quit":
                        LastStatut = "quit";
                        game.QuitPage();

                        break;

                    //case "placement":
                    //    game.Placement();
                    //    break;

                    default:
                        LastStatut = "MainMenu";
                        game.MainMenu();
                        break;
                }
            }
        }
    }
}
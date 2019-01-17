namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            Game game = new Game();
            //game.LevelWritte();
            //game.ElderGame();
            string LastStatut = "";
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
                        game.Run(true);
                        break;

                    case "elderGame":
                        LastStatut = "elderGame";
                        game.ElderGame();
                        break;

                    case "replay":
                        game.Run(false/*, path*/);
                        break;

                    case "return"://close the window
                        LastStatut = "return";
                        game.ReturnMenu();
                        break;
                        
                    case "history"://history mode
                        LastStatut = "history";
                        game.NewArena();
                        //game.LevelWritte();
                        game.HistoryLvSelectionMenu();
                        
                        break;

                    case "ended"://when the game has ended
                        LastStatut = "ended";
                        game.ResultWindow();
                        break;

                    case "credit"://credit
                        LastStatut = "credit";
                        game.CreditPage();
                        break;

                    case "quit":
                        LastStatut = "quit";
                        game.QuitPage();
                        break;

                    default:
                        LastStatut = "MainMenu";
                        game.MainMenu();
                        break;
                }
                
            }
        }
    }
}
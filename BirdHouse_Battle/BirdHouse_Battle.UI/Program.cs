namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            Game game = new Game();
            game.LevelWritte();
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
                        game.Run();
                        break;
                        
                    case "return"://close the window
                        game.ReturnMenu();
                        break;
                        
                    case "history"://history mode

                        game.NewArena();
                        //game.LevelWritte();
                        game.HistoryLvSelectionMenu();
                        
                        break;

                    case "ended"://when the game has ended
                        game.ResultWindow();
                        break;

                    case "credit"://credit
                        game.CreditPage();
                        break;

                    case "quit":
                        game.QuitPage();
                        break;

                    default:
                        game.MainMenu();
                        break;
                }
                
            }
        }
    }
}
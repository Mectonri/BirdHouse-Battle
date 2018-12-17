using BirdHouse_Battle.Model;
using Newtonsoft.Json.Linq;


namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            Game game = new Game();

            ///////
            //Arena a = new Arena();
            //Team t = new Team(a,"Bleu",125);
            //Archer A = new Archer(t,a, 1);

            //JObject o = new JObject
            //    (
            //        new Archer(t, a, 1)
            //        {
                      
           
            //        }
            //    );
                   

            //string json = JsonConvert.SerializeObject(t);

            //System.IO.File.WriteAllText("../../../../res/", json);
            /////


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
                        break;
                        
                    //case "pause"://pause the game screen
                    //    if ( game.Paused == true ) { game.PauseMenu(); }
                        
                    //    break;

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
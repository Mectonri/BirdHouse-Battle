using BirdHouse_Battle.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            Game game = new Game();

            /////
            Arena a = new Arena();
            Team t = new Team(a, "RED", 125);

            t.AddArcher(2);
            
            string St = JsonConvert.SerializeObject(t);


            //Archer bsObj = new Archer(t, a, 1);
            //Archer a1 = new Archer(t, a, 2);
            //Paladin paladin = new Paladin(t, a, 1);

            //string jsonData = JsonConvert.SerializeObject(bsObj);
            //string Sa1 = JsonConvert.SerializeObject(a1);

            //string pal = JsonConvert.SerializeObject(paladin);

            //File.WriteAllText(@"D:\Users\saxel\ITI\PI\BirdHouse-Battle\BirdHouse_Battle\res\Archerdata.JSON", jsonData);
            //File.WriteAllText(@"D:\Users\saxel\ITI\PI\BirdHouse-Battle\BirdHouse_Battle\res\Archerdata.JSON", Sa1);
            File.WriteAllText(@"D:\Users\saxel\ITI\PI\BirdHouse-Battle\BirdHouse_Battle\res\Archerdata.JSON", St);

            //Archer archer = JsonConvert.DeserializeObject<Archer>(File.ReadAllText(@"D:\Users\saxel\ITI\PI\BirdHouse-Battle\BirdHouse_Battle\res\Archerdata.JSON"));
            //Archer Da1 = JsonConvert.DeserializeObject<Archer>(File.ReadAllText(@"D:\Users\saxel\ITI\PI\BirdHouse-Battle\BirdHouse_Battle\res\Archerdata.JSON"));
            //Paladin Dpal = JsonConvert.DeserializeObject<Paladin>(File.ReadAllText(@"D:\Users\saxel\ITI\PI\BirdHouse-Battle\BirdHouse_Battle\res\Archerdata.JSON"));


            Team Dt = JsonConvert.DeserializeObject<Team>(File.ReadAllText(@"D:\Users\saxel\ITI\PI\BirdHouse-Battle\BirdHouse_Battle\res\Archerdata.JSON"));


            //System.Console.WriteLine("The serialized archer is named: " + archer.Name);
            //System.Console.WriteLine("The serialized archer is named: " + Da1.Name);
            //System.Console.WriteLine("The serialized paladin is named: " + Dpal.Name);

            //System.Console.WriteLine("The serialized archer calldown is: " + archer.Speed);
            //System.Console.WriteLine("The serialized Team : " + Dt.Name);
            //System.Console.WriteLine("The serialized Team : " + Dt.UnitCount);
            //System.Console.WriteLine("The serialized Team : " + Dt.Pcount + Dt.Acount + "Team Number" + Dt.TeamNumber);





            //// Convert BlogSites object to JOSN string format  
            //string jsonData = JsonConvert.SerializeObject(bsObj);

            //Response.Write(jsonData);


            //System.IO.File.WriteAllText("../../../../res/", o.ToString());
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
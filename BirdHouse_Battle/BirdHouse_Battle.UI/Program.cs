namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            //game.Run();
            switch (game.Status)
            {
                case "main":
                    game.MainMenu();
                    break;
                case "game":
                    game.Run();
                    break;

                default:
                    break;
            }
        }
    }
}
using BirdHouse_Battle.Model;
using System;

namespace BirdHouse_Battle.UI
{
    class Program
    {
        static void processInput()
        {
            throw new ArgumentException(); // To configure later.
        }

        static double getCurrentTime()
        {
           return DateTime.Now.ToOADate();
        }

        static void update(Arena arena)
        {
            arena.Update();
        }

        static void Render(GameScreen rend, Arena arena)
        {
            rend.window.Clear();
            rend.UnitDisplay(arena);
            rend.window.Display();
        }

        static double MS_PER_UPDATE
        {
            get { return 0.0000006; }
        }

        static void Main(string[] args)
        {
            //Preparation of the game.

            
            Arena arena = new Arena();


            Team blue = arena.CreateTeam("blue");
            Team red = arena.CreateTeam("red");
;
            
            //Each unit is represented by a shape
            //Archers are triagles, goblins by circles and paladin by rectangular shapes

            red.AddArcher(15);
            red.AddGobelin(55);
            red.AddPaladin(55);
            blue.AddArcher(15);
            blue.AddGobelin(55);
            blue.AddPaladin(55);
            green.AddArcher(15);
            green.AddGobelin(55);
            green.AddPaladin(55);
            yellow.AddArcher(15);
            yellow.AddGobelin(55);
            yellow.AddPaladin(55);


            arena.SpawnUnit();

            GameScreen render = new GameScreen();

            ////End of Preparation.



            //Nous affinerons cela plus tard, mais les bases sont ici.
            //processInput() gère toutes les entrées utilisateur intervenues depuis le dernier appel.
            //Ensuite, update() avancer la simulation du jeu d'un pas. Il utilise l'IA et la physique (généralement dans cet ordre).
            //Enfin, render() dessine le jeu afin que le joueur puisse voir ce qui s'est passé.

            double previous = getCurrentTime();
            double lag = 0.0;
            while (arena.TeamCount > 1)
            {
                double current = getCurrentTime();
                double elapsed = current - previous;
                previous = current;
                lag += elapsed;

                //processInput();

                while (lag >= MS_PER_UPDATE)
                {
                    update(arena);
                    lag -= MS_PER_UPDATE;
                }

                Render(render, arena);
            }
        }
    }
}

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

            Team red = arena.CreateTeam("red");
            Team blue = arena.CreateTeam("blue");
            Team green = arena.CreateTeam("green");
            Team yellow = arena.CreateTeam("yellow");

            red.AddArcher(25);
            red.AddGobelin(50);
            red.AddPaladin(50);
            blue.AddArcher(25);
            blue.AddGobelin(50);
            blue.AddPaladin(50);
            green.AddArcher(25);
            green.AddGobelin(50);
            green.AddPaladin(50);
            yellow.AddArcher(25);
            yellow.AddGobelin(50);
            yellow.AddPaladin(50);

            arena.SpawnUnit();
            //End of Preparation.

            GameScreen render = new GameScreen();

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

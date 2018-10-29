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
            return DateTime.Now.Hour;
        }

        static void update(Arena arena)
        {
            arena.Update();
        }

        static void render()
        {
            throw new ArgumentException();
        }

        static double MS_PER_UPDATE
        {
            get { return 0.60; }
        }

        static void Main(string[] args)
        {

            Arena arena = new Arena();

            //Nous affinerons cela plus tard, mais les bases sont ici.
            //processInput() gère toutes les entrées utilisateur intervenues depuis le dernier appel.
            //Ensuite, update() avancer la simulation du jeu d'un pas. Il utilise l'IA et la physique (généralement dans cet ordre).
            //Enfin, render() dessine le jeu afin que le joueur puisse voir ce qui s'est passé.

            double previous = getCurrentTime();
            double lag = 0.0;
            while (true)
            {
                double current = getCurrentTime();
                double elapsed = current - previous;
                previous = current;
                lag += elapsed;

                processInput();

                while (lag >= MS_PER_UPDATE)
                {
                    update(arena);
                    lag -= MS_PER_UPDATE;
                }

                render();
            }
        }
    }
}

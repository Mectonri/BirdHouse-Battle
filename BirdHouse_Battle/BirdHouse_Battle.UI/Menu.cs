﻿using SFML.Graphics;
using SFML.Window;

namespace BirdHouse_Battle.UI
{
    public class Menu
    {
        private Game game;
        RenderWindow _mainMenu;

        public Menu(Game game)
        {
            this.game = game;
            _mainMenu = new RenderWindow(new VideoMode(512, 512), "Main Menu");
        }

        public RenderWindow MainMenu
        {
            get { return _mainMenu; }
        }

        public void Main()
        {
          
        }
    }
}
using BirdHouse_Battle.Model;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace BirdHouse_Battle.UI
{
    /// <summary>
    ///  This class will be used to only draw units
    /// </summary>
    public class Drawer
    {
        RenderWindow _window;
        Shape _bShape;
        Texture _terain;
        Vector2f _size;
        Texture _startButton;
        Texture _creditButton;
        Texture _settingButton;
        Texture _historyButton;
        Texture _continueButton;
        Texture _restartButton;
        Texture _quitButton;

        public Drawer(RenderWindow win)
        {
            _terain = new Texture("../../../../res/terrain1.jpeg");
            _startButton = new Texture("../../../../res/button_start.png");

            _creditButton = new Texture("../../../../res/button_credits.png");
            _settingButton = new Texture("../../../../res/button_setting.png");
            _historyButton = new Texture("../../../../res/button_history.png");

            _continueButton = new Texture("../../../../res/button_continue.png");
            _restartButton = new Texture("../../../../res/button_restart.png");
            _quitButton = new Texture("../../../../res/button_quit.png");

            _window = win;
           
            _size = new Vector2f(512, 512);
            _bShape = new RectangleShape(_size);
            _bShape.Texture = _terain;
            win.Draw(Bshape);
        }

        public Shape Bshape { get { return _bShape; } }

        public Texture Terain { get { return _terain; } }

        public Vector2f Size { get { return _size; } }

        /// <summary>
        /// Displays the game background
        /// </summary>
        public void BackGroundGame()
        {
            _terain = new Texture("../../../../res/terrain1.jpeg");
            _size = new Vector2f(512, 512);
            _bShape = new RectangleShape(_size);
            _bShape.Texture = _terain;
            _window.Draw(Bshape);

            //Displays the legend
            RectangleShape legend = new RectangleShape(new Vector2f(512, 200));
            legend.Texture = new Texture("../../../../res/LEGEND.png");
            legend.Position = new Vector2f(0, 512);
            _window.Draw(legend);
        }

        /// <summary>
        /// Display archer with the corresponding color
        /// </summary>
        /// <param name="unit"></param>
        static Shape DisplayArcher(Unit unit)
        {
            CircleShape shape = new CircleShape(7);
            shape.SetPointCount(3);
            shape.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(shape, unit);

            return shape;
        }

        /// <summary>
        /// Displays Goblin and color it in function of the team
        /// </summary>
        /// <param name="unit"></param>
        static CircleShape DisplayGoblin(Unit unit)
        {
            CircleShape shape = new CircleShape(6);
            shape.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(shape, unit);

            return shape;
        }

        static RectangleShape DisplayPaladin(Unit unit)
        {
            Vector2f vect = new Vector2f(10, 10);
            RectangleShape shape = new RectangleShape(vect);
            shape.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(shape, unit);

            return shape;
        }

        /// <summary>
        /// Display drake with the corresponding color
        /// </summary>
        /// <param name="unit"></param>
        static Shape DisplayDrake(Unit unit)
        {
            CircleShape shape = new CircleShape(8);
            shape.SetPointCount(5);
            shape.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(shape, unit);

            return shape;
        }

        static Shape DisplayBalista(Unit unit)
        {
            CircleShape shape = new CircleShape(8);
            shape.SetPointCount(6);
            shape.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(shape, unit);

            return shape;
        }

        static Shape DisplayCatapult(Unit unit)
        {
            CircleShape shape = new CircleShape(8);
            shape.SetPointCount(7);
            shape.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(shape, unit);

            return shape;
        }

        static Shape DisplayArrow(Projectile projectile)
        {
            CircleShape arrDis = new CircleShape(2);
            arrDis.Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250);

            return arrDis;
        }

        static Shape DisplayBoulder(Projectile projectile)
        {
            CircleShape boulDis = new CircleShape(6);
            boulDis.Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250);

            return boulDis;
        }

        static Shape DisplayBalisticAmmo(Projectile projectile)
        {
            CircleShape balisDis = new CircleShape(4);
            balisDis.Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250);

            return balisDis;
        }

        public static Shape Coloring(Shape shape, Unit unit)
        {
            switch (unit.Team.TeamNumber)
            {
                case 0:
                    shape.FillColor = new Color(0, 71, 96);
                    break;
                case 1:
                    shape.FillColor = new Color(138, 19, 0);
                    break;
                case 2:
                    shape.FillColor = new Color(32, 121, 0);
                    break;
                case 3:
                    shape.FillColor = new Color(213, 205, 0);
                    break;
            }
            return shape;
        }

        /// <summary>
        /// Creates the Texture for the differents buttons 
        /// </summary>
        static void Texturer()
        {
           
        }
        
        public Shape DisplayField(Tile tile)
        {
            CircleShape arrField = new CircleShape(5);
            arrField.SetPointCount(6);
            arrField.Position = new Vector2f((float)tile.X + 250, (float)tile.Y + 250);

            if (tile.Height != 0)
            {
                switch (tile.Height)
                {
                    case 1: arrField.FillColor = new Color(10, 30, 27); break;
                    case 2: arrField.FillColor = new Color(30, 40, 28); break;
                    case 3: arrField.FillColor = new Color(50, 50, 29); break;
                    case 4: arrField.FillColor = new Color(70, 60, 30); break;
                    case 5: arrField.FillColor = new Color(90, 65, 31); break;
                    case 6: arrField.FillColor = new Color(110, 70, 32); break;
                    case 7: arrField.FillColor = new Color(115, 75, 33); break;
                    case 8: arrField.FillColor = new Color(120, 80, 34); break;
                    case 9: arrField.FillColor = new Color(125, 84, 35); break;
                    case 10: arrField.FillColor = new Color(130, 87, 36); break;
                    case 11: arrField.FillColor = new Color(135, 90, 37); break;
                    case 12: arrField.FillColor = new Color(140, 94, 38); break;
                    case 13: arrField.FillColor = new Color(145, 97, 39); break;
                    case 14: arrField.FillColor = new Color(150, 100, 40); break;
                }
            }
            else if (tile.Obstacle != "None")
            {
                switch (tile.Obstacle)
                {
                    case "Rock":
                        arrField.FillColor = new Color(195, 180, 157);
                        break;

                    case "Tree":
                        arrField.FillColor = new Color(0, 67, 5);
                        break;

                    case "River":
                        arrField.FillColor = new Color(30, 38, 129);
                        break;
                }
            }

            return arrField;
        }


        //static  void DisplayUnit(Unit u)
        //{
        //    if (u as Archer) { }
        //    if (u is Archer) { DisplayArcher(u); }
        //    else if (u is Balista) { DisplayBalista(u); }
        //    else if (u is Catapult ) {DisplayCatapult(u); }
        //    else if (u is Drake ) { DisplayDrake(u); }
        //    else if (u is Goblin) { DisplayGoblin(u); }
        //    else { DisplayPaladin(u); }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arena"></param>
        public void UnitDisplay(Arena arena)
        {
            Shape shape = null;

            foreach (KeyValuePair<string, Tile> tile in arena.Field.Elements)
            {
                shape = DisplayField(tile.Value);
                _window.Draw(shape);
            }
            foreach (KeyValuePair<string, Team> team in arena.Teams)
            {
                foreach (KeyValuePair<int, Unit> unit in team.Value.Units)
                {
                    string s = unit.Value.ToString();
                    switch (s)
                    {
                        case "BirdHouse_Battle.Model.Archer":
                            shape = DisplayArcher(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Goblin":
                            shape = DisplayGoblin(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Drake":
                            shape = DisplayDrake(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Balista":
                            shape = DisplayBalista(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Catapult":
                            shape = DisplayCatapult(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Paladin":
                            shape = DisplayPaladin(unit.Value);
                            break;
                    }

                    _window.Draw(shape);
                }
            }
            foreach (KeyValuePair<int, Projectile> projectile in arena.Projectiles)
            {
                string s = projectile.Value.ToString();
                if (s == "BirdHouse_Battle.Model.Arrow") shape = DisplayArrow(projectile.Value);
                else if (s == "BirdHouse_Battle.Model.Boulder") shape = DisplayBoulder(projectile.Value);
                else shape = DisplayBalisticAmmo(projectile.Value);
                _window.Draw(shape);
            }
        }

        public RectangleShape[] MenuDisplay()
        {
            Vector2f Bsize = new Vector2f(100,25);
            RectangleShape[] buttons = new RectangleShape[5];

            RectangleShape buttonPreGame = new RectangleShape(Bsize);
            buttonPreGame.Texture = _startButton;
            buttonPreGame.Position = new Vector2f(200, 50);
            
            
            RectangleShape buttonHistory = new RectangleShape(Bsize);
            buttonHistory.Texture = _historyButton;
            buttonHistory.Position = new Vector2f(200, 100);


            RectangleShape buttonParameter = new RectangleShape(Bsize);
            buttonParameter.Texture = _settingButton;
            buttonParameter.Position = new Vector2f(200, 150);

            RectangleShape buttonCredit = new RectangleShape(Bsize);
            buttonCredit.Texture = _creditButton;
            buttonCredit.Position = new Vector2f(200, 200);

            RectangleShape buttonQuit = new RectangleShape(Bsize);
            buttonQuit.Texture = _quitButton;
            buttonQuit.Position = new Vector2f(200, 250);

            buttons[0] = buttonPreGame;
            buttons[1] = buttonHistory;
            buttons[2] = buttonParameter;
            buttons[3] = buttonCredit;
            buttons[4] = buttonQuit;
            
            foreach (var t in buttons)
            {
                _window.Draw(t);
            }
            return buttons;
        }

        /// <summary>
        /// This function will launch and draw the pause menu when the game is on pause.
        /// </summary>
        public RectangleShape[] PauseDisplay()
        {
            RectangleShape[] button = new RectangleShape[4];

            Vector2f Bsize = new Vector2f(100, 25);
            RectangleShape buttonContinue = new RectangleShape(Bsize);
            buttonContinue.Texture = _continueButton;
            buttonContinue.Position = new Vector2f(200, 50);

            RectangleShape buttonRestart = new RectangleShape(Bsize);
            buttonRestart.Texture = _restartButton;
            buttonRestart.Position = new Vector2f(200, 100);

            RectangleShape buttonSetting = new RectangleShape(Bsize);
            buttonSetting.Texture = _settingButton;
            buttonSetting.Position = new Vector2f(200, 150);

            RectangleShape buttonQuit = new RectangleShape(Bsize);
            buttonQuit.Texture = _quitButton;
            buttonQuit.Position = new Vector2f(200, 200);

            button[0] = buttonContinue;
            button[1] = buttonRestart;
            button[2] = buttonSetting;
            button[3] = buttonQuit;

            foreach (var t in button)
            {
                _window.Draw(t);
            }
            return button;
        }

        public RectangleShape[] PreGameDisplay(string[] status, int[,] teamComposition)
        {
            Vector2f Bsize = new Vector2f(75, 25);//button size
            Vector2f Tsize = new Vector2f(118, 190); //team button  size

            RenderStates rs = new RenderStates();
            Font font = new Font("../../../../res/Overlock-Regular.ttf");//font for the text

            RectangleShape[] buttons = new RectangleShape[17];
            Text[] messages = new Text[10];


            RectangleShape buttonAddRemove = new RectangleShape(Bsize);
            buttonAddRemove.Position = new Vector2f(275, 30);
            buttonAddRemove.FillColor = new Color(211,211,211);
            Text textAddRemove = new Text("",font,25);

            if (status[5] == "add")
            {
                textAddRemove = new Text("ADD", font, 20);
            }
            else
            {
                textAddRemove = new Text("REM", font, 20);
            }
            textAddRemove.Position = new Vector2f(295, 27);
            textAddRemove.FillColor = new Color(0, 0, 0);





            RectangleShape button1 = new RectangleShape();
            Text text1 = new Text("1",font,25);

            RectangleShape button10 = new RectangleShape();
            Text text10 = new Text("10", font, 25);

            RectangleShape button100 = new RectangleShape();
            Text text100 = new Text("100", font, 25);


            RectangleShape buttonPlay = new RectangleShape(Bsize);
            buttonPlay.Position = new Vector2f(380, 125);
            buttonPlay.Texture = new Texture("../../../../res/button_play.png");

            RectangleShape buttonArcher = new RectangleShape(Bsize);
            buttonArcher.Position = new Vector2f(10,30);
            buttonArcher.Texture = new Texture("../../../../res/button_archer.png");

            RectangleShape buttonBalista = new RectangleShape(Bsize);
            buttonBalista.Position = new Vector2f(105, 30);
            buttonBalista.Texture = new Texture("../../../../res/button_balista.png");

            RectangleShape buttonCatapult = new RectangleShape(Bsize);
            buttonCatapult.Position = new Vector2f(105, 95);
            buttonCatapult.Texture = new Texture("../../../../res/button_catapult.png");
           
            RectangleShape buttonDrake = new RectangleShape(Bsize);
            buttonDrake.Position = new Vector2f(10, 95);
            buttonDrake.Texture = new Texture("../../../../res/button_drake.png");

            RectangleShape buttonGobelin = new RectangleShape(Bsize);
            buttonGobelin.Position = new Vector2f(10, 160);
            buttonGobelin.Texture = new Texture("../../../../res/button_goblin.png");

            RectangleShape buttonPaladin = new RectangleShape(Bsize);
            buttonPaladin.Position = new Vector2f(10, 225);
            buttonPaladin.Texture = new Texture("../../../../res/button_paladin.png");
           
            RectangleShape buttonAddTeam1 = new RectangleShape(Tsize);
            buttonAddTeam1.Position = new Vector2f(5, 317);
            buttonAddTeam1.OutlineThickness = 5;
            buttonAddTeam1.OutlineColor = new Color(0,0,250);
            if (status[0]== "selected")
            {
                buttonAddTeam1.FillColor = new Color(255, 160, 122);
            }
            Text messageTeam1 = new Text("ARCHER : "+teamComposition[0,0].ToString()+"\n DRAKE : "+teamComposition[0,1].ToString()+"\n GOBLIN : "+teamComposition[0,2].ToString()+"\n PALADIN : "+teamComposition[0,3].ToString()+"\n BALISTA : "+teamComposition[0,4].ToString()+"\n CATAPULT : "+teamComposition[0,5].ToString(), font, 15);
            messageTeam1.FillColor = new Color(0, 0, 0);
            messageTeam1.Position = new Vector2f(15, 322);

            RectangleShape buttonAddTeam2 = new RectangleShape(Tsize);
            buttonAddTeam2.Position = new Vector2f(132, 317);
            buttonAddTeam2.OutlineThickness = 5;
            buttonAddTeam2.OutlineColor = new Color(250, 0, 0);
            
            if (status[1] == "selected")
            {
                buttonAddTeam2.FillColor = new Color(255, 160, 122);
            }
            Text messageTeam2 = new Text("ARCHER : " + teamComposition[1, 0].ToString() + "\n DRAKE : " + teamComposition[1, 1].ToString() + "\n GOBLIN : " + teamComposition[1, 2].ToString() + "\n PALADIN : " + teamComposition[1, 3].ToString() + "\n BALISTA : " + teamComposition[1, 4].ToString() + "\n CATAPULT : " + teamComposition[1, 5].ToString(), font, 15);
            messageTeam2.FillColor = new Color(0, 0, 0);
            messageTeam2.Position = new Vector2f(142, 322);


            RectangleShape buttonAddTeam3 = new RectangleShape(Tsize);
            RectangleShape buttonSupprTeam3 = new RectangleShape(new Vector2f(20, 20));
            buttonAddTeam3.Position = new Vector2f(261, 317);
            buttonSupprTeam3.Position = new Vector2f(360,317);
            buttonAddTeam3.OutlineThickness = 5;
            buttonAddTeam3.OutlineColor = new Color(0, 250, 0);
            Text messageTeam3 = new Text("", font, 25);
            Text messageSuppr3 = new Text(" ",font,25);
            if (status[2] == "inactive")
            {
                buttonAddTeam3.FillColor = new Color(128, 128, 128);
                buttonSupprTeam3.FillColor = new Color( Color.Transparent);
            }
            else
            {
                buttonSupprTeam3.FillColor = new Color(255, 0, 0);
                messageSuppr3 = new Text("X", font, 25);
                messageSuppr3.FillColor = new Color(0, 0, 0);
                messageSuppr3.Position = new Vector2f(362, 310);
                if (status[2] == "selected")
                {
                    buttonAddTeam3.FillColor = new Color(255, 160, 122);
                    
                }
                messageTeam3 = new Text("ARCHER : " + teamComposition[2, 0].ToString() + "\n DRAKE : " + teamComposition[2, 1].ToString() + "\n GOBLIN : " + teamComposition[2, 2].ToString() + "\n PALADIN : " + teamComposition[2, 3].ToString() + "\n BALISTA : " + teamComposition[2, 4].ToString() + "\n CAPAPULT : " + teamComposition[2, 5].ToString(), font, 15);
                messageTeam3.FillColor = new Color(0, 0, 0);
                messageTeam3.Position = new Vector2f(271, 322);
            }
            
           

            RectangleShape buttonAddTeam4 = new RectangleShape(Tsize);
            RectangleShape buttonSupprTeam4 = new RectangleShape(new Vector2f(20, 20));
            buttonAddTeam4.Position = new Vector2f(389, 317);
            buttonSupprTeam4.Position = new Vector2f(488, 317);
            buttonAddTeam4.OutlineThickness = 5;
            buttonAddTeam4.OutlineColor = new Color(250, 250, 0);
            Text messageTeam4 = new Text("", font, 25);
            Text messageSuppr4 = new Text("",font,25);
            if (status[3] == "inactive")
            {
                buttonAddTeam4.FillColor = new Color(128, 128, 128);
                buttonSupprTeam4.FillColor = new Color(Color.Transparent);
            }
            else
            {

                buttonSupprTeam4.FillColor = new Color(255,0,0);
                messageSuppr4 = new Text("X", font, 25);
                messageSuppr4.FillColor = new Color(0, 0, 0);
                messageSuppr4.Position = new Vector2f(490, 310);
                if (status[3] == "selected")
                {
                    buttonAddTeam4.FillColor = new Color(255, 160, 122);
                }
                messageTeam4 = new Text("ARCHER : " + teamComposition[3, 0].ToString() + "\n DRAKE : " + teamComposition[3, 1].ToString() + "\n GOBLIN : " + teamComposition[3, 2].ToString() + "\n PALADIN : " + teamComposition[3, 3].ToString() + "\n BALISTA : " + teamComposition[3, 4].ToString() + "\n CATAPULT : " + teamComposition[3, 5].ToString(), font, 15);
                messageTeam4.FillColor = new Color(0, 0, 0);
                messageTeam4.Position = new Vector2f(399, 322);
            }
            
            buttons[0] = buttonAddTeam1;
            buttons[1] = buttonAddTeam2;
            buttons[2] = buttonAddTeam3;
            buttons[3] = buttonAddTeam4;
            buttons[4] = buttonPlay;
            buttons[5] = buttonArcher;
            buttons[6] = buttonDrake;
            buttons[7] = buttonGobelin;
            buttons[8] = buttonPaladin;
            buttons[9] = buttonBalista;
            buttons[10] = buttonCatapult;
            buttons[11] =buttonSupprTeam3;
            buttons[12] =buttonSupprTeam4;
            buttons[13] = buttonAddRemove;
            buttons[14] = button1;
            buttons[15] = button10;
            buttons[16] = button100;


            foreach (var t in buttons)
            {
                _window.Draw(t);
            }
            
            messages[0] = messageTeam1;
            messages[1] = messageTeam2;
            messages[2] = messageTeam3;
            messages[3] = messageTeam4;
            messages[4] = messageSuppr3;
            messages[5] = messageSuppr4;
            messages[6] = text1;
            messages[7] = text10;
            messages[8] = text100;
            messages[9] = textAddRemove;

            foreach (var t in messages)
            {
                t.Draw(_window, rs);
                _window.Draw(t);
            }

            return buttons;
        }
    }
}
/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents main menu of the game.
 * 
 * Design patterns: 
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class MenuMain : Menu
    {
        private DespatBreakout game;
        SpriteBatch spriteBatch;

        public MenuMain(DespatBreakout game) : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            LoadContent();
            buttons.Add(new Button(game, DespatBreakout.GameState.MissionChoice));
            buttons[0].Initialize(menuFont, 300, 150, "NEW GAME", "grey_button15.png");
            buttons.Add(new Button(game, DespatBreakout.GameState.Achievements));
            buttons[1].Initialize(menuFont, 300, 220, "ACHIEVEMENTS", "grey_button15.png");
            buttons.Add(new Button(game, DespatBreakout.GameState.Tutorial));
            buttons[2].Initialize(menuFont, 300, 290, "HOW TO PLAY", "grey_button15.png");
            buttons.Add(new Button(game, DespatBreakout.GameState.Exit));
            buttons[3].Initialize(menuFont, 300, 360, "EXIT", "grey_button15.png");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            activeButton = buttons[activeButtonIndex];
            activeButton.isHoovered = true;
            base.Initialize();
        }

    }
}

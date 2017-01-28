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

namespace DespatBreakout
{
    class MenuMain : Menu
    {
        DespatBreakout game;
        SpriteBatch spriteBatch;
        Rectangle destinationRectangle;
        Rectangle sourceRectangle;
        RectangleFontAdjuster helper;

        public MenuMain(DespatBreakout game) : base(game)
        {
            this.game = game;
            this.helper = new RectangleFontAdjuster();
        }

        public override void Initialize()
        {
            LoadContent();
 
            this.destinationRectangle = new Rectangle(100, 30, 580, 100);
            this.sourceRectangle = game.buttonTextures.GetTextureRectangle("grey_button08.png");

            buttons.Add(new Button(game, DespatBreakout.GameState.MissionChoice));
            buttons[0].Initialize(menuFont, 300, 180, "NEW GAME", "grey_button15.png");
            buttons.Add(new Button(game, DespatBreakout.GameState.Achievements));
            buttons[1].Initialize(menuFont, 300, 250, "ACHIEVEMENTS", "grey_button15.png");
            buttons.Add(new Button(game, DespatBreakout.GameState.Tutorial));
            buttons[2].Initialize(menuFont, 300, 320, "HOW TO PLAY", "grey_button15.png");
            buttons.Add(new Button(game, DespatBreakout.GameState.Exit));
            buttons[3].Initialize(menuFont, 300, 390, "EXIT", "grey_button15.png");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            activeButton = buttons[activeButtonIndex];
            activeButton.isHoovered = true;
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(game.buttonTextures.textureSheet, destinationRectangle, sourceRectangle, Color.White);
            helper.DrawButtonText(spriteBatch, menuFont, "DespatBreakout", destinationRectangle);

            foreach(Button b in buttons)
            {
                b.Draw(gameTime);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

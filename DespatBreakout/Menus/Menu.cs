/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a generic menu with buttons, with keyboard navigation.
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
    class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    { 
        public List<Button> buttons = new List<Button> { };
        public SpriteFont menuFont;
        private DespatBreakout game;
        SpriteBatch spriteBatch;
        public Button activeButton;
        public int activeButtonIndex = 0;

        public Menu(DespatBreakout game) : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            menuFont = game.Content.Load<SpriteFont>("Fonts/MainMenu");
            base.LoadContent();
        }

        public override void Initialize()
        {

            LoadContent();
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.currentKeyboardState.IsKeyDown(Keys.Down) && game.previousKeyboardState.IsKeyUp(Keys.Down))
            {
                activeButton.isHoovered = false;
                activeButtonIndex = (activeButtonIndex + 1) % buttons.Count;
                activeButton = buttons[activeButtonIndex];
                activeButton.isHoovered = true;
            }
            if (game.currentKeyboardState.IsKeyDown(Keys.Up) && game.previousKeyboardState.IsKeyUp(Keys.Up))
            {
                activeButton.isHoovered = false;
                activeButtonIndex = (activeButtonIndex - 1);
                if (activeButtonIndex < 0) activeButtonIndex = buttons.Count - 1;
                activeButton = buttons[activeButtonIndex];
                activeButton.isHoovered = true;
            }
            if (game.currentKeyboardState.IsKeyDown(Keys.Enter) && game.previousKeyboardState.IsKeyUp(Keys.Enter))
            {
                activeButton.Click();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach(Button b in buttons)
            {
                b.Draw(gameTime);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

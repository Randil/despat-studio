using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class MissionScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        SpriteBatch spriteBatch;
        Player player;
        public BrickWall bricks;

        public MissionScreen(Game1 game) : base(game)
        {
            this.game = game;
        }

           protected override void LoadContent()
        {
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
            //if (Game1.Instance.currentKeyboardState.IsKeyDown(Keys.Down) && Game1.Instance.previousKeyboardState.IsKeyUp(Keys.Down))
            //{
            //    activeButton.isHoovered = false;
            //    activeButtonIndex = (activeButtonIndex + 1) % buttons.Count;
            //    activeButton = buttons[activeButtonIndex];
            //    activeButton.isHoovered = true;
            //}
            //if (Game1.Instance.currentKeyboardState.IsKeyDown(Keys.Up) && Game1.Instance.previousKeyboardState.IsKeyUp(Keys.Up))
            //{
            //    activeButton.isHoovered = false;
            //    activeButtonIndex = (activeButtonIndex - 1);
            //    if (activeButtonIndex < 0) activeButtonIndex = buttons.Count - 1;
            //    activeButton = buttons[activeButtonIndex];
            //    activeButton.isHoovered = true;
            //}
            //if (Game1.Instance.currentKeyboardState.IsKeyDown(Keys.Enter) && Game1.Instance.previousKeyboardState.IsKeyUp(Keys.Enter))
            //{
            //    activeButton.Click();
            //}
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            bricks.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}

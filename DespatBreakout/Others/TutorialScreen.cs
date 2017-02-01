/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a static tutorial screen.
 * 
 * Design patterns: 
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    class TutorialScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D screen;
        DespatBreakout game;
        SpriteBatch spriteBatch;

        public TutorialScreen(DespatBreakout game)
            : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            screen = game.Content.Load<Texture2D>("Graphics/Tutorial");
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
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(screen, new Rectangle(0, 0, 
                game.GraphicsDevice.PresentationParameters.BackBufferWidth, 
                game.GraphicsDevice.PresentationParameters.BackBufferHeight), 
                Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

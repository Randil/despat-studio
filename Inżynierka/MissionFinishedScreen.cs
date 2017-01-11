using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter 
{
    class MissionFinishedScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        SpriteBatch spriteBatch;
        double time;

        public MissionFinishedScreen(Game1 game)
            : base(game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(Game1.Instance.GraphicsDevice);
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(double time)
        {
            LoadContent();
            this.time = time;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(Game1.Instance.Content.Load<SpriteFont>("Fonts/MainMenu"),
            "CONGRATULATIONS, YOU HAVE WON!", new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(Game1.Instance.Content.Load<SpriteFont>("Fonts/MainMenu"),
            "You have completed this level in: " + time + " seconds", new Vector2(100, 200), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

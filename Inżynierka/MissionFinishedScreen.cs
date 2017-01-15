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
        private DespatBreakout game;
        SpriteBatch spriteBatch;
        string finishString;
        double time;

        public MissionFinishedScreen(DespatBreakout game)
            : base(game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(double time, string finishString)
        {
            LoadContent();
            this.time = time;
            this.finishString = finishString;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(game.Content.Load<SpriteFont>("Fonts/MainMenu"),
            finishString, new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(game.Content.Load<SpriteFont>("Fonts/MainMenu"),
            "You have played this level for: " + time + " seconds", new Vector2(100, 200), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

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
        Ball ball;

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
            player = new Player(Game1.Instance);
            player.Initialize("paddle_medium.png",
                (Game1.Instance.GraphicsDevice.Viewport.Width - Game1.Instance.gameTextures.getTextureRectangle("paddle_medium.png").Width) / 2,
                Game1.Instance.GraphicsDevice.Viewport.Height - 50);
            ball = new Ball(Game1.Instance);
            ball.Initialize(new StrategyFlyBy(bricks, ball, player), "ball_normal.png",
                (Game1.Instance.GraphicsDevice.Viewport.Width - Game1.Instance.gameTextures.getTextureRectangle("ball_normal.png").Width) / 2,
                 Game1.Instance.GraphicsDevice.Viewport.Height - 50 - Game1.Instance.gameTextures.getTextureRectangle("ball_normal.png").Height);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            ball.Update(gameTime);
            player.Update(gameTime);
            bricks.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            ball.Draw(gameTime);
            player.Draw(gameTime);
            bricks.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}

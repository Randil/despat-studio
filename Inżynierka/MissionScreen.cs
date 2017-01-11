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
        Paddle player;
        public BrickWall bricks;
        Ball ball;
        bool onGoing;
        GameTime startTime;
        double time;
        MissionFinishedScreen finishScreen;
        MissionFinishedProxy finishedProxy;
        IBallCollisionStrategy collisionStrategy;

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
            spriteBatch = new SpriteBatch(Game1.Instance.GraphicsDevice);
            onGoing = false;

            player = new Paddle(Game1.Instance);
            player.Initialize("paddle_medium.png",
                (Game1.Instance.GraphicsDevice.Viewport.Width - Game1.Instance.gameTextures.getTextureRectangle("paddle_medium.png").Width) / 2,
                Game1.Instance.GraphicsDevice.Viewport.Height - 50);

            ball = new Ball(Game1.Instance);
            ball.Initialize("ball_normal.png",
                (Game1.Instance.GraphicsDevice.Viewport.Width - Game1.Instance.gameTextures.getTextureRectangle("ball_normal.png").Width) / 2,
                 Game1.Instance.GraphicsDevice.Viewport.Height - 50 - Game1.Instance.gameTextures.getTextureRectangle("ball_normal.png").Height);

            collisionStrategy = new StrategyNormal(bricks, ball, player);

            startTime = new GameTime();
            finishScreen = new MissionFinishedScreen(Game1.Instance);
            finishedProxy = new MissionFinishedProxy();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (bricks.wall.Count != 0)
            {
                if (onGoing == false)
                {
                    startTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
                    onGoing = true;
                }

                collisionStrategy.CheckCollisions();
                ball.Update(gameTime);
                player.Update(gameTime);
                bricks.Update(gameTime); 
            }
            else if (onGoing == true)  //End of the mission
            {
                time = gameTime.TotalGameTime.TotalSeconds - startTime.TotalGameTime.TotalSeconds;
                finishedProxy.missionFinished(time, finishScreen);
                onGoing = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

                ball.Draw(gameTime);
                player.Draw(gameTime);
                bricks.Draw(gameTime);

                if (bricks.wall.Count == 0)
                {
                    finishScreen.Draw(gameTime);   
                }

            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}

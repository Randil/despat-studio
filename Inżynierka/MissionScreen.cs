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
        private DespatBreakout game;
        SpriteBatch spriteBatch;
        public Paddle player;
        public BrickWall bricks;
        public List<Ball> balls;
        List<Ball> ballsToRemove;
        public List<Bonus> bonuses;
        List<Bonus> bonusesToRemove;
        bool onGoing;
        bool finished;
        GameTime startTime;
        double time;
        MissionFinishedScreen finishScreen;
        MissionFinishedProxy finishedProxy;

        public MissionScreen(DespatBreakout game) : base(game)
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
            onGoing = false;
            finished = false;
            player = new Paddle(game);
            player.Initialize("paddle_medium.png",
                (game.GraphicsDevice.Viewport.Width - game.gameTextures.GetTextureRectangle("paddle_medium.png").Width) / 2,
                game.GraphicsDevice.Viewport.Height - 50);


            Ball ball = new Ball(game);
            IBallCollisionStrategy collisionStrategy = new StrategyNormal(bricks, ball, player);

            ball.Initialize("ball_normal.png", collisionStrategy,
                (game.GraphicsDevice.Viewport.Width - game.gameTextures.GetTextureRectangle("ball_normal.png").Width) / 2,
                 game.GraphicsDevice.Viewport.Height - 50 - game.gameTextures.GetTextureRectangle("ball_normal.png").Height);

            balls = new List<Ball> {ball};
            ballsToRemove = new List<Ball> {};
            bonuses = new List<Bonus> { };
            bonusesToRemove = new List<Bonus> { };

            startTime = new GameTime();
            finishScreen = new MissionFinishedScreen(game);
            finishedProxy = new MissionFinishedProxy();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (finished == false)
            {
                if (onGoing == false)
                {
                    startTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
                    onGoing = true;
                }

                time = gameTime.TotalGameTime.TotalSeconds - startTime.TotalGameTime.TotalSeconds;
                foreach(Ball b in balls)
                    b.Update(gameTime);
                RemoveBalls();

                foreach (Bonus bo in bonuses)
                    bo.Update(gameTime);
                RemoveBonuses();

                player.Update(gameTime);
                bricks.Update(gameTime); 
                
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            foreach (Ball b in balls)
            b.Draw(gameTime);

            foreach (Bonus bo in bonuses)
                bo.Draw(gameTime);

            player.Draw(gameTime);
            bricks.Draw(gameTime);

            if (finished == true) finishScreen.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void BallFalled(Ball ball)
        {
            ballsToRemove.Add(ball);
        }

        public void RemoveBalls()
        {
            foreach (Ball b in ballsToRemove)
                balls.Remove(b);
            ballsToRemove.Clear();
            if (balls.Count == 0)
                MissionFailed();
        }
        public void RemoveBonuses()
        {
            foreach(Bonus b in bonusesToRemove)
                bonuses.Remove(b);
            bonusesToRemove.Clear();
        }
        public void BonusCollected(Bonus bonus)
        {
            bonusesToRemove.Add(bonus);
        }
        public void MissionSuccess()
        {
            finished = true;
            finishedProxy.MissionFinished(time, finishScreen, "Congratulations, you have won!");
            onGoing = false;
        }

        public void MissionFailed()
        {
            finished = true;
            finishedProxy.MissionFinished(time, finishScreen, "You failed!");
            onGoing = false;
        }


    }
}

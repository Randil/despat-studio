/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a game level. It contains instances of balls, player paddle, bricks and bonuses.
 * Class handles delay at the start of the mission.
 * MissionScreen can be initialized via a MissionSave object or only with a BrickWall (then it sets default values for other components).
 * 
 * 
 * Design patterns: Memento, Command
 ---------------------------------------------------------------------------------------------------------*/

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
        bool onGoing;
        bool finished;
        double delay;
        MissionFinishedScreen finishScreen;
        MissionFinishedProxy finishedProxy;
        
        public GameTime startTime;
        public double time;

        public Paddle player;
        public BrickWall bricks;

        public List<Ball> balls;
        List<Ball> ballsToRemove;
        public List<Bonus> bonuses;
        List<Bonus> bonusesToRemove;
        public List<IBonusCommand> bonusEffects;
        List<IBonusCommand> bonusEffectsToRemove;

        public MissionScreen(DespatBreakout game) : base(game)
        {
            this.game = game;
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(BrickWall bWall)
        {          
            LoadContent();
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            onGoing = false;
            finished = false;

            this.bricks = bWall;

            player = new Paddle(game);
            player.Initialize("paddle_medium.png",
                (game.GraphicsDevice.Viewport.Width - game.gameTextures.GetTextureRectangle("paddle_medium.png").Width) / 2,
                game.GraphicsDevice.Viewport.Height - 50);


            Ball ball = new Ball(game);
            IBallCollisionStrategy collisionStrategy = new StrategyNormal(bricks, ball, player);

            ball.Initialize("ball_normal.png", collisionStrategy,
                (game.GraphicsDevice.Viewport.Width - game.gameTextures.GetTextureRectangle("ball_normal.png").Width) / 2,
                 game.GraphicsDevice.Viewport.Height - 50 - game.gameTextures.GetTextureRectangle("ball_normal.png").Height,
                 0f, -500f);

            balls = new List<Ball> {ball};
            ballsToRemove = new List<Ball> {};
            bonuses = new List<Bonus> { };
            bonusesToRemove = new List<Bonus> { };
            bonusEffects = new List<IBonusCommand> { };
            bonusEffectsToRemove = new List<IBonusCommand> { };

            startTime = new GameTime();
            delay = 1.5d;
            finishScreen = new MissionFinishedScreen(game);
            finishedProxy = new MissionFinishedProxy();
            base.Initialize();
        }

        public void Initialize(MissionSave save)
        {
            LoadContent();
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            onGoing = true;
            finished = false;

            this.bricks = save.bricks;

            player = new Paddle(game);
            player.Initialize(save.playerTexture, save.playerX, save.playerY);

            balls = new List<Ball> { };

            foreach(MissionSave.BallState ballState in save.balls)
            {
                IBallCollisionStrategy collisionStrategy;
                Ball ball = new Ball(game);
                if (ballState.textureName.Equals("ball_big.png"))
                    collisionStrategy = new StrategyFireball(bricks, ball, player);
                else collisionStrategy = new StrategyNormal(bricks, ball, player);
                ball.Initialize(ballState.textureName, collisionStrategy, (int)ballState.x, (int)ballState.y, ballState.xSpeed, ballState.ySpeed);
                balls.Add(ball);
            }
           
            ballsToRemove = new List<Ball> { };
            bonuses = save.bonuses;
            bonusesToRemove = new List<Bonus> { };
            bonusEffects = save.bonusEffects;
            bonusEffectsToRemove = new List<IBonusCommand> { };

            startTime = save.startTime;
            time = save.timePlayed;
            delay = time + 1.5d;
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

                if (time < delay) // We want some time before everything starts running
                {
                    foreach (Ball b in balls)
                        b.ResetGameTime(gameTime);

                    foreach (Bonus bo in bonuses)
                        bo.ResetGameTime(gameTime);

                    player.ResetGameTime(gameTime);
                }
                else 
                {
                    foreach (Ball b in balls)
                        b.Update(gameTime);
                    RemoveBalls();

                    foreach (Bonus bo in bonuses)
                        bo.Update(gameTime);
                    RemoveBonuses();

                    AquireBonuses();

                    player.Update(gameTime);
                    bricks.Update(gameTime);
                }
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
        public void AquireBonuses()
        {
            foreach(IBonusCommand bonus in bonusEffects)
            {
                bonus.GrantBonus();
                bonusEffectsToRemove.Add(bonus);
            }
            foreach(IBonusCommand bonus in bonusEffectsToRemove)
            {
                bonusEffects.Remove(bonus);
            }
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

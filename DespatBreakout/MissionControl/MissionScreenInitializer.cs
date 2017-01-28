/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class is a helper of MissionScreen. It takes away all initialization methods for purpose of code clarity.
 * 
 * Design patterns: Factory
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    class MissionScreenInitializer
    {
        MissionScreen mission;

        public MissionScreenInitializer(MissionScreen mission)
        {
            this.mission = mission;
        }

        public void Initialize(BrickWall bWall) //Dodać factory
        {
            this.mission.spriteBatch = new SpriteBatch(this.mission.game.GraphicsDevice);
            this.mission.onGoing = false;
            this.mission.finished = false;

            this.mission.bricks = bWall;

            this.mission.player = new Paddle(this.mission.game);
            this.mission.player.Initialize("paddle_medium.png",
                (this.mission.game.GraphicsDevice.Viewport.Width - this.mission.game.gameTextures.GetTextureRectangle("paddle_medium.png").Width) / 2,
                this.mission.game.GraphicsDevice.Viewport.Height - 50);


            Ball ball = new Ball(this.mission.game);
            IBallCollisionStrategy collisionStrategy = new StrategyNormal(this.mission.bricks, ball, this.mission.player);

            ball.Initialize("ball_normal.png", collisionStrategy,
                (this.mission.game.GraphicsDevice.Viewport.Width - this.mission.game.gameTextures.GetTextureRectangle("ball_normal.png").Width) / 2,
                 this.mission.game.GraphicsDevice.Viewport.Height - 50 - this.mission.game.gameTextures.GetTextureRectangle("ball_normal.png").Height,
                 0f, -500f);

            this.mission.balls = new List<Ball> { ball };
            this.mission.ballsToRemove = new List<Ball> { };
            this.mission.bonuses = new List<Bonus> { };
            this.mission.bonusesToRemove = new List<Bonus> { };
            this.mission.bonusEffects = new List<IBonusCommand> { };
            this.mission.bonusEffectsToRemove = new List<IBonusCommand> { };

            this.mission.startTime = new GameTime();
            this.mission.delay = 1.5d;
            this.mission.finishScreen = new MissionFinishedScreen(this.mission.game);
            this.mission.finishedProxy = new MissionFinishedProxy();
        }

        public void Initialize(MissionSave save)
        {
            this.mission.spriteBatch = new SpriteBatch(this.mission.game.GraphicsDevice);
            this.mission.onGoing = true;
            this.mission.finished = false;

            this.mission.bricks = save.bricks;

            this.mission.player = new Paddle(this.mission.game);
            this.mission.player.Initialize(save.playerTexture, save.playerX, save.playerY);

            this.mission.balls = new List<Ball> { };

            foreach (MissionSave.BallState ballState in save.balls)
            {
                IBallCollisionStrategy collisionStrategy;
                Ball ball = new Ball(this.mission.game);
                if (ballState.textureName.Equals("ball_big.png"))
                    collisionStrategy = new StrategyFireball(this.mission.bricks, ball, this.mission.player);
                else collisionStrategy = new StrategyNormal(this.mission.bricks, ball, this.mission.player);
                ball.Initialize(ballState.textureName, collisionStrategy, (int)ballState.x, (int)ballState.y, ballState.xSpeed, ballState.ySpeed);
                this.mission.balls.Add(ball);
            }

            this.mission.ballsToRemove = new List<Ball> { };
            this.mission.bonuses = save.bonuses;
            this.mission.bonusesToRemove = new List<Bonus> { };
            this.mission.bonusEffects = save.bonusEffects;
            this.mission.bonusEffectsToRemove = new List<IBonusCommand> { };

            this.mission.startTime = save.startTime;
            this.mission.time = save.timePlayed;
            this.mission.delay = this.mission.time + 1.5d;
            this.mission.finishScreen = new MissionFinishedScreen(this.mission.game);
            this.mission.finishedProxy = new MissionFinishedProxy();
        }

    }
}

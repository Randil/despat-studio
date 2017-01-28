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
            mission.spriteBatch = new SpriteBatch(mission.game.GraphicsDevice);
            mission.onGoing = false;
            mission.finished = false;

            mission.bricks = bWall;

            mission.player = new Paddle(mission.game);
            mission.player.Initialize("paddle_medium.png",
                (mission.game.GraphicsDevice.Viewport.Width - mission.game.gameTextures.GetTextureRectangle("paddle_medium.png").Width) / 2,
                mission.game.GraphicsDevice.Viewport.Height - 50);


            Ball ball = new Ball(mission.game);
            IBallCollisionStrategy collisionStrategy = new StrategyNormal(mission.bricks, ball, mission.player);

            ball.Initialize("ball_normal.png", collisionStrategy,
                (mission.game.GraphicsDevice.Viewport.Width - mission.game.gameTextures.GetTextureRectangle("ball_normal.png").Width) / 2,
                 mission.game.GraphicsDevice.Viewport.Height - 50 - mission.game.gameTextures.GetTextureRectangle("ball_normal.png").Height,
                 0f, -500f);

            mission.balls = new List<Ball> { ball };
            mission.ballsToRemove = new List<Ball> { };
            mission.bonuses = new List<Bonus> { };
            mission.bonusesToRemove = new List<Bonus> { };
            mission.bonusEffects = new List<IBonusCommand> { };
            mission.bonusEffectsToRemove = new List<IBonusCommand> { };

            mission.startTime = new GameTime();
            mission.delay = 1.5d;
            mission.finishScreen = new MissionFinishedScreen(mission.game);
            mission.finishedProxy = new MissionFinishedProxy();
        }

        public void Initialize(MissionSave save)
        {
            mission.spriteBatch = new SpriteBatch(mission.game.GraphicsDevice);
            mission.onGoing = true;
            mission.finished = false;

            mission.bricks = save.bricks;

            mission.player = new Paddle(mission.game);
            mission.player.Initialize(save.playerTexture, save.playerX, save.playerY);

            mission.balls = new List<Ball> { };

            foreach (MissionSave.BallState ballState in save.balls)
            {
                IBallCollisionStrategy collisionStrategy;
                Ball ball = new Ball(mission.game);
                if (ballState.textureName.Equals("ball_big.png"))
                    collisionStrategy = new StrategyFireball(mission.bricks, ball, mission.player);
                else collisionStrategy = new StrategyNormal(mission.bricks, ball, mission.player);
                ball.Initialize(ballState.textureName, collisionStrategy, (int)ballState.x, (int)ballState.y, ballState.xSpeed, ballState.ySpeed);
                mission.balls.Add(ball);
            }

            mission.ballsToRemove = new List<Ball> { };
            mission.bonuses = save.bonuses;
            mission.bonusesToRemove = new List<Bonus> { };
            mission.bonusEffects = save.bonusEffects;
            mission.bonusEffectsToRemove = new List<IBonusCommand> { };

            mission.startTime = save.startTime;
            mission.time = save.timePlayed;
            mission.delay = mission.time + 1.5d;
            mission.finishScreen = new MissionFinishedScreen(mission.game);
            mission.finishedProxy = new MissionFinishedProxy();
        }

    }
}

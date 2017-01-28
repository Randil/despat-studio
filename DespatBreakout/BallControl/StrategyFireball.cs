/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a collision strategy used by fireballs 
 * (reflecting from paddle and walls, ability to fall down, immediately destroying bricks without being reflected).
 * 
 * Design patterns: Strategy
 ---------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    class StrategyFireball : IBallCollisionStrategy
    {
        public Ball ball;
        public BrickWall bricks;
        public Paddle player;

        List<IBrick> bricksToDestroy;
        bool justReflected = false; 

        public StrategyFireball(BrickWall bricks, Ball ball, Paddle player)
        {
            this.ball = ball;
            this.bricks = bricks;
            this.player = player;
            bricksToDestroy = new List<IBrick> { };
        }

        public void CheckCollisions()
        {
           foreach (IBrick b in bricks.wall)
           {
               if (b.GetDestinationRectangle().Intersects(ball.destinationRectangle))
               {
                   bricksToDestroy.Add(b);
               }
           }

           foreach (IBrick b in bricksToDestroy)
                b.Destroy(b);

           bricksToDestroy.Clear();

           if (ball.destinationRectangle.Intersects(player.destinationRectangle))
           { 
               if (justReflected == false) CalculateReflectionPaddle(); 
           }
           else justReflected = false;

           if (ball.y <= 0) CalculateReflectionWall(Ball.HitSide.top);
           if (ball.y >= player.game.GraphicsDevice.Viewport.Height - ball.sourceRectangle.Height)
               CalculateReflectionWall(Ball.HitSide.bottom);
           if (ball.x <= 0) CalculateReflectionWall(Ball.HitSide.left);
           if (ball.x >= player.game.GraphicsDevice.Viewport.Width - ball.sourceRectangle.Width)
               CalculateReflectionWall(Ball.HitSide.right);
        }

        public void CalculateReflectionBrick(Ball.HitSide hitSide, IBrick brick)
        {
            justReflected = true;
            /// <remarks>
            /// Notice that effects depending on HitSide are reversed in comparison to CalculateReflectionWall
            /// Thats because the ball is INSIDE game screen and OUTSIDE all the bricks - TOP border of the game screen
            /// Should reflect the ball in the same way BOTTOM border of a brick does etc.
            /// </remarks>
            switch (hitSide)
            {
                case Ball.HitSide.bottom:
                    ball.ySpeed = -ball.ySpeed;
                    break;
                case Ball.HitSide.top:
                    ball.ySpeed = -ball.ySpeed;
                    break;
                case Ball.HitSide.right:
                    ball.xSpeed = -ball.xSpeed;
                    break;
                case Ball.HitSide.left:
                    ball.xSpeed = -ball.xSpeed;
                    break;
            }

        }

        public void CalculateReflectionWall(Ball.HitSide hitSide)
        {
            switch (hitSide)
            {
                case Ball.HitSide.top:
                    ball.ySpeed = -ball.ySpeed;
                    break;
                case Ball.HitSide.bottom: // Ball fell down      
                    ball.FallDown();
                    break;
                case Ball.HitSide.left:
                    ball.xSpeed = -ball.xSpeed;
                    break;
                case Ball.HitSide.right:
                    ball.xSpeed = -ball.xSpeed;
                    break;
            }

        }

        public void CalculateReflectionPaddle()
        {
            justReflected = true;

            float paddleMiddle = (player.x + player.destinationRectangle.Width / 2);
            float ballMiddle = (ball.x + ball.destinationRectangle.Width / 2);

            float reflectionFactor = (paddleMiddle - ballMiddle) / 100;
            float xChange = ball.maxSpeed - ball.maxSpeed * (1 - Math.Abs(reflectionFactor));
            ball.ySpeed = -ball.maxSpeed + xChange;

            if (reflectionFactor > 0) ball.xSpeed = xChange;
            else ball.xSpeed = -xChange;
        }

        public IBallCollisionStrategy Duplicate(Ball ball)
        {
            IBallCollisionStrategy strategy = new StrategyFireball(this.bricks, ball, this.player);
            return strategy;
        }

    }
}

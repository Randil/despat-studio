/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a collision strategy used by fireballs 
 * (reflecting from paddle and walls, ability to fall down, immediately destroying bricks without being reflected).
 * 
 * Design patterns: Strategy
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
            this.bricksToDestroy = new List<IBrick> { };
        }

        public void CheckCollisions()
        {
            foreach (IBrick b in this.bricks.wall)
           {
               if (b.GetDestinationRectangle().Intersects(this.ball.destinationRectangle))
               {
                   this.bricksToDestroy.Add(b);
               }
           }

           foreach (IBrick b in this.bricksToDestroy)
                b.Destroy(b);

           this.bricksToDestroy.Clear();

           if (this.ball.destinationRectangle.Intersects(this.player.destinationRectangle))
           {
               if (this.justReflected == false) this.CalculateReflectionPaddle(); 
           }
           else this.justReflected = false;

           if (this.ball.y <= 0) this.CalculateReflectionWall(Ball.HitSide.top);
           if (this.ball.y >= this.player.game.GraphicsDevice.Viewport.Height - this.ball.sourceRectangle.Height)
               this.CalculateReflectionWall(Ball.HitSide.bottom);
           if (this.ball.x <= 0) this.CalculateReflectionWall(Ball.HitSide.left);
           if (this.ball.x >= this.player.game.GraphicsDevice.Viewport.Width - this.ball.sourceRectangle.Width)
               this.CalculateReflectionWall(Ball.HitSide.right);
        }

        public void CalculateReflectionBrick(Ball.HitSide hitSide, IBrick brick)
        {
            this.justReflected = true;
            /// <remarks>
            /// Notice that effects depending on HitSide are reversed in comparison to CalculateReflectionWall
            /// Thats because the ball is INSIDE game screen and OUTSIDE all the bricks - TOP border of the game screen
            /// Should reflect the ball in the same way BOTTOM border of a brick does etc.
            /// </remarks>
            switch (hitSide)
            {
                case Ball.HitSide.bottom:
                    this.ball.ySpeed = -this.ball.ySpeed;
                    break;
                case Ball.HitSide.top:
                    this.ball.ySpeed = -this.ball.ySpeed;
                    break;
                case Ball.HitSide.right:
                    this.ball.xSpeed = -this.ball.xSpeed;
                    break;
                case Ball.HitSide.left:
                    this.ball.xSpeed = -this.ball.xSpeed;
                    break;
            }
        }

        public void CalculateReflectionWall(Ball.HitSide hitSide)
        {
            switch (hitSide)
            {
                case Ball.HitSide.top:
                    this.ball.ySpeed = -this.ball.ySpeed;
                    break;
                case Ball.HitSide.bottom: // Ball fell down      
                    this.ball.FallDown();
                    break;
                case Ball.HitSide.left:
                    this.ball.xSpeed = -this.ball.xSpeed;
                    break;
                case Ball.HitSide.right:
                    this.ball.xSpeed = -this.ball.xSpeed;
                    break;
            }
        }

        public void CalculateReflectionPaddle()
        {
            this.justReflected = true;

            float paddleMiddle = (this.player.x + this.player.destinationRectangle.Width / 2);
            float ballMiddle = (this.ball.x + this.ball.destinationRectangle.Width / 2);

            float reflectionFactor = (paddleMiddle - ballMiddle) / 100;
            float xChange = this.ball.maxSpeed - this.ball.maxSpeed * (1 - Math.Abs(reflectionFactor));
            this.ball.ySpeed = -this.ball.maxSpeed + xChange;

            if (reflectionFactor > 0) this.ball.xSpeed = xChange;
            else this.ball.xSpeed = -xChange;
        }

        public IBallCollisionStrategy Duplicate(Ball ball)
        {
            IBallCollisionStrategy strategy = new StrategyFireball(this.bricks, ball, this.player);
            return strategy;
        }
    }
}

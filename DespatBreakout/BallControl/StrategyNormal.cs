/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a collision strategy with normal behaviour 
 * (reflecting from paddle, walls and bricks, ability to fall down).
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

    public class StrategyNormal : IBallCollisionStrategy
    {
        public Ball ball;
        public BrickWall bricks;
        public Paddle player;

        bool justReflected = false; 
        bool intersectionOccured = false;

        public StrategyNormal(BrickWall bricks, Ball ball, Paddle player)
        {
            this.ball = ball;
            this.bricks = bricks;
            this.player = player;
        }

        public void CheckCollisions()
        {
            this.intersectionOccured = false;
           foreach (IBrick b in this.bricks.wall)
           {
               if (b.GetDestinationRectangle().Intersects(this.ball.destinationRectangle))
               {
                   this.intersectionOccured = true;
                   if (this.justReflected == false)
                   {
                       // Left/right
                       if (this.ball.destinationRectangle.Left <= b.GetDestinationRectangle().Right &&
                           this.ball.destinationRectangle.Right >= b.GetDestinationRectangle().Right &&
                           this.ball.xSpeed > 0)
                       { this.CalculateReflectionBrick(Ball.HitSide.left, b); }
                       else if (this.ball.destinationRectangle.Right >= b.GetDestinationRectangle().Left &&
                           this.ball.destinationRectangle.Left <= b.GetDestinationRectangle().Left &&
                           this.ball.xSpeed > 0)
                       { this.CalculateReflectionBrick(Ball.HitSide.right, b); }

                       // Top/bottom
                       else if (this.ball.destinationRectangle.Top <= b.GetDestinationRectangle().Bottom)
                       { this.CalculateReflectionBrick(Ball.HitSide.bottom, b); }
                       else if (this.ball.destinationRectangle.Bottom >= b.GetDestinationRectangle().Top)
                       { this.CalculateReflectionBrick(Ball.HitSide.top, b); }
                   }

                   if (this.justReflected == true)
                   {
                       b.Hit(b);
                       break;
                   }
               }
           }
           if (this.intersectionOccured == false) this.justReflected = false;

           if (this.ball.destinationRectangle.Intersects(this.player.destinationRectangle))
           { if (this.justReflected == false) this.CalculateReflectionPaddle(); }
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
            // Notice that effects depending on HitSide are reversed in comparison to CalculateReflectionWall
            // Thats because the ball is INSIDE game screen and OUTSIDE all the bricks - TOP border of the game screen
            // Should reflect the ball in the same way BOTTOM border of a brick does etc.
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
            float ballMiddle = (this.ball.x + (this.ball.destinationRectangle.Width / 2));

            float reflectionFactor = (paddleMiddle - ballMiddle) / 100;
            float xChange = this.ball.maxSpeed - (this.ball.maxSpeed * (1 - Math.Abs(reflectionFactor)));
            this.ball.ySpeed = -this.ball.maxSpeed + xChange;

            if (reflectionFactor > 0) this.ball.xSpeed = xChange;
            else this.ball.xSpeed = -xChange;
        }

        public IBallCollisionStrategy Duplicate(Ball ball)
        {
            IBallCollisionStrategy strategy = new StrategyNormal(this.bricks, ball, this.player);
            return strategy;
        }
    }
}

/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a collision strategy used for testing purpose 
 * (reflecting from paddle, walls and floor, not interfering with bricks).
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

    public class StrategyFlyBy : IBallCollisionStrategy
    {
        public Ball ball;
        public BrickWall bricks;
        public Paddle player;

        bool justReflected = false;

        public StrategyFlyBy(BrickWall bricks, Ball ball, Paddle player)
        {
            this.ball = ball;
            this.bricks = bricks;
            this.player = player;
        }

       public void CheckCollisions()
        {
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
        }

        public void CalculateReflectionWall(Ball.HitSide hitSide)
        {
            switch (hitSide)
            {
                case Ball.HitSide.top:
                    this.ball.ySpeed = -this.ball.ySpeed;
                    break;
                case Ball.HitSide.bottom:
                    this.ball.ySpeed = -this.ball.ySpeed;
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
            IBallCollisionStrategy strategy = new StrategyFlyBy(this.bricks, ball, this.player);
            return strategy;
        }
    }
}

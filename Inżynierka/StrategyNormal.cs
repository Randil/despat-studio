using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
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
           intersectionOccured = false;
           foreach(IBrick b in bricks.wall)
           {
               if (b.GetDestinationRectangle().Intersects(ball.destinationRectangle))
               {
                   intersectionOccured = true;
                   if (justReflected == false)
                   {
                       //Left/right
                       if (ball.destinationRectangle.Left <= b.GetDestinationRectangle().Right &&
                           ball.destinationRectangle.Right >= b.GetDestinationRectangle().Right &&
                           ball.xSpeed > 0)
                       { CalculateReflectionBrick(Ball.hitSide.left, b); }
                       else if (ball.destinationRectangle.Right >= b.GetDestinationRectangle().Left &&
                           ball.destinationRectangle.Left <= b.GetDestinationRectangle().Left &&
                           ball.xSpeed > 0)
                       { CalculateReflectionBrick(Ball.hitSide.right, b); }

                       //Top/bottom
                       else if (ball.destinationRectangle.Top <= b.GetDestinationRectangle().Bottom)
                       { CalculateReflectionBrick(Ball.hitSide.bottom, b); }
                       else if (ball.destinationRectangle.Bottom >= b.GetDestinationRectangle().Top)
                       { CalculateReflectionBrick(Ball.hitSide.top, b); }
                   }
                   if (justReflected == true)
                   {
                       b.Hit(b);
                       break;
                   }
               }
           }
           if (intersectionOccured == false) justReflected = false;

            if (ball.destinationRectangle.Intersects(player.destinationRectangle))
                { if (justReflected == false) CalculateReflectionPaddle(); }
            else justReflected = false;

            if (ball.y <= 0) CalculateReflectionWall(Ball.hitSide.top);
            if (ball.y >= player.game.GraphicsDevice.Viewport.Height - ball.sourceRectangle.Height) 
                CalculateReflectionWall(Ball.hitSide.bottom);
            if (ball.x <= 0) CalculateReflectionWall(Ball.hitSide.left);
            if (ball.x >= player.game.GraphicsDevice.Viewport.Width - ball.sourceRectangle.Width) 
                CalculateReflectionWall(Ball.hitSide.right);
        }
        public void CalculateReflectionBrick(Ball.hitSide hitSide, IBrick brick)
        {
            justReflected = true;
            //Notice that effects depending on HitSide are reversed in comparison to CalculateReflectionWall
            //Thats because the ball is INSIDE game screen and OUTSIDE all the bricks - TOP border of the game screen
            //Should reflect the ball in the same way BOTTOM border of a brick does etc.
            switch (hitSide)
            {
                case Ball.hitSide.bottom:
                    ball.ySpeed = -ball.ySpeed;
                    break;
                case Ball.hitSide.top:
                    ball.ySpeed = -ball.ySpeed;
                    break;
                case Ball.hitSide.right:
                    ball.xSpeed = -ball.xSpeed;
                    break;
                case Ball.hitSide.left:
                    ball.xSpeed = -ball.xSpeed;
                    break;
            }

        }
        public void CalculateReflectionWall(Ball.hitSide hitSide)
        {
            switch (hitSide)
            {
                case Ball.hitSide.top:
                    ball.ySpeed = -ball.ySpeed;
                    break;
                case Ball.hitSide.bottom: //Ball fell down      
                    ball.FallDown();
                    break;
                case Ball.hitSide.left:
                    ball.xSpeed = -ball.xSpeed;
                    break;
                case Ball.hitSide.right:
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
            IBallCollisionStrategy strategy = new StrategyNormal(this.bricks, ball, this.player);
            return strategy;
        }

    }
}

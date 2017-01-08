using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class StrategyFlyBy //: IBallCollisionStrategy
    {
        public Ball ball;
        public BrickWall bricks;
        public Player player;
        bool justReflected = false;

        public StrategyFlyBy(BrickWall bricks, Ball ball, Player player)
        {
            this.ball = ball;
            this.bricks = bricks;
            this.player = player;
        }

       public void CheckCollisions()
        {
            if (ball.destinationRectangle.Intersects(player.destinationRectangle))
                { if (justReflected == false) CalculateReflectionPaddle(); }
            else justReflected = false;

            if (ball.y <= 0) CalculateReflectionWall(Ball.hitSide.top);
            if (ball.y >= Game1.Instance.GraphicsDevice.Viewport.Height - ball.sourceRectangle.Height) 
                CalculateReflectionWall(Ball.hitSide.bottom);
            if (ball.x <= 0) CalculateReflectionWall(Ball.hitSide.left);
            if (ball.x >= Game1.Instance.GraphicsDevice.Viewport.Width - ball.sourceRectangle.Width) 
                CalculateReflectionWall(Ball.hitSide.right);
        }
        void CalculateReflectionBrick()
        {

        }
        void CalculateReflectionWall(Ball.hitSide hitSide)
        {
            switch (hitSide)
            {
                case Ball.hitSide.top:
                    ball.ySpeed = -ball.ySpeed;
                    break;
                case Ball.hitSide.bottom:
                    ball.ySpeed = -ball.ySpeed;
                    break;
                case Ball.hitSide.left:
                    ball.xSpeed = -ball.xSpeed;
                    break;
                case Ball.hitSide.right:
                    ball.xSpeed = -ball.xSpeed;
                    break;
            }

        }
        void CalculateReflectionPaddle()
        {
            justReflected = true;

            float paddleMiddle = (player.x + player.destinationRectangle.Width / 2);
            float ballMiddle = (ball.x + ball.destinationRectangle.Width / 2);

            float reflectionFactor = (paddleMiddle - ballMiddle)/100;
            float xChange = ball.maxSpeed - ball.maxSpeed * (1 - Math.Abs(reflectionFactor));
            ball.ySpeed = -ball.maxSpeed + xChange;

            if (reflectionFactor > 0) ball.xSpeed = xChange;
            else ball.xSpeed =  -xChange;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
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

        }
        public void CalculateReflectionWall(Ball.hitSide hitSide)
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
        public void CalculateReflectionPaddle()
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

        public IBallCollisionStrategy Duplicate(Ball ball)
        {
            IBallCollisionStrategy strategy = new StrategyFlyBy(this.bricks, ball, this.player);
            return strategy;
        }

    }
}

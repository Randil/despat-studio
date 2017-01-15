using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BonusAditionalBall : Bonus
    {
        public BonusAditionalBall(DespatBreakout game, Paddle player) : base(game, player)
        {
            this.player = player;
            this.game = game;
        }

        public override void GrantBonus()
        {
            List<Ball> ballsToAdd = new List<Ball> { };
            foreach(Ball ball in game.activeMission.balls)
            {
                ballsToAdd.Add(ball.Duplicate());
                ball.xSpeed += 50;
                ball.ySpeed -= 50;
            }

            foreach (Ball ball in ballsToAdd)
            {
                ball.xSpeed -= 50;
                ball.ySpeed += 50;
                game.activeMission.balls.Add(ball);
            }

            game.activeMission.BonusCollected(this);
        }
    }
}

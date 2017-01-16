using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BonusCommandFireball : IBonusCommand
    {
        DespatBreakout game;
        Paddle player;
        public BonusCommandFireball(DespatBreakout game, Paddle player)
        {
            this.player = player;
            this.game = game;
        }

        public void GrantBonus()
        {
            Ball ball = game.activeMission.balls[0];
            ball.textureName = "ball_big.png";
            ball.sourceRectangle = game.gameTextures.GetTextureRectangle("ball_big.png");
            ball.destinationRectangle.Height = ball.sourceRectangle.Height;
            ball.destinationRectangle.Width = ball.sourceRectangle.Width;

            ball.collisionStrategy = new StrategyFireball(game.activeMission.bricks, ball, game.activeMission.player);

        }
    }
}

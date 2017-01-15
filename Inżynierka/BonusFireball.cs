using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BonusFireball : Bonus
    {
        public BonusFireball(DespatBreakout game, Paddle player) : base(game, player)
        {
            this.player = player;
            this.game = game;
        }

        public override void GrantBonus()
        {
            Ball ball = game.activeMission.balls[0];
            ball.textureName = "ball_big.png";
            ball.sourceRectangle = game.gameTextures.GetTextureRectangle("ball_big.png");
            ball.destinationRectangle.Height = ball.sourceRectangle.Height;
            ball.destinationRectangle.Width = ball.sourceRectangle.Width;

            ball.collisionStrategy = new StrategyFireball(game.activeMission.bricks, ball, game.activeMission.player);

            game.activeMission.BonusCollected(this);
        }
    }
}

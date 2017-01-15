using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BonusSmallPaddle : Bonus
    {
        public BonusSmallPaddle(DespatBreakout game, Paddle player) : base(game, player)
        {
            this.player = player;
            this.game = game;
        }

        public override void GrantBonus()
        {
            player.sourceRectangle = game.gameTextures.GetTextureRectangle("paddle_small.png");
            int shift = player.sourceRectangle.Width - player.destinationRectangle.Width;
            player.x -= shift / 2;
            player.destinationRectangle.X -= shift / 2;
            player.destinationRectangle.Height = player.sourceRectangle.Height;
            player.destinationRectangle.Width = player.sourceRectangle.Width;

            game.activeMission.BonusCollected(this);
        }
    }
}

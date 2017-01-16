using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BonusCommandBigPaddle : IBonusCommand
    {
        DespatBreakout game;
        Paddle player;
        public BonusCommandBigPaddle(DespatBreakout game, Paddle player)
        {
            this.player = player;
            this.game = game;
        }

        public void GrantBonus()
        {
            player.sourceRectangle = game.gameTextures.GetTextureRectangle("paddle_big.png");
            player.texture = "paddle_big.png";
            int shift = player.sourceRectangle.Width - player.destinationRectangle.Width;
            player.x -= shift / 2;
            player.destinationRectangle.X -= shift / 2;
            player.destinationRectangle.Height = player.sourceRectangle.Height;
            player.destinationRectangle.Width = player.sourceRectangle.Width;

        }

    }
}

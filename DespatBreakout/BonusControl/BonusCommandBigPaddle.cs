/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a command which swiches player paddle to 'big'.
 * 
 * Design patterns: Command
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
            this.player.sourceRectangle = this.game.gameTextures.GetTextureRectangle("paddle_big.png");
            this.player.texture = "paddle_big.png";
            int shift = this.player.sourceRectangle.Width - this.player.destinationRectangle.Width;
            this.player.x -= shift / 2;
            this.player.destinationRectangle.X -= shift / 2;
            this.player.destinationRectangle.Height = this.player.sourceRectangle.Height;
            this.player.destinationRectangle.Width = this.player.sourceRectangle.Width;
        }
    }
}

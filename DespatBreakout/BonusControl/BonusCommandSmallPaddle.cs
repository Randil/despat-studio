﻿/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a command which swiches player paddle to 'small'.
 * 
 * Design patterns: Command
 ---------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BonusCommandSmallPaddle : IBonusCommand
    {
        DespatBreakout game;
        Paddle player;
        public BonusCommandSmallPaddle(DespatBreakout game, Paddle player)
        {
            this.player = player;
            this.game = game;
        }

        public void GrantBonus()
        {
            player.sourceRectangle = game.gameTextures.GetTextureRectangle("paddle_small.png");
            player.texture = "paddle_small.png";
            int shift = player.sourceRectangle.Width - player.destinationRectangle.Width;
            player.x -= shift / 2;
            player.destinationRectangle.X -= shift / 2;
            player.destinationRectangle.Height = player.sourceRectangle.Height;
            player.destinationRectangle.Width = player.sourceRectangle.Width; 
        }
    }
}
/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class contains steering component for player paddle.
 * 
 * Design patterns: Component
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    class PaddleSteering
    {
        Paddle player;
        GameTime previousGameTime;
        int delta, max;

        public PaddleSteering(Paddle player)
        {
            this.player = player;
            delta = 0; max = 0;
            previousGameTime = new GameTime();
        }

        public void Update(GameTime gameTime)
        {
           max = player.game.GraphicsDevice.Viewport.Width - player.sourceRectangle.Width;
           delta = gameTime.TotalGameTime.Milliseconds - previousGameTime.TotalGameTime.Milliseconds;
           if (delta < 0) delta = 1000 + delta;
           if (player.game.currentKeyboardState.IsKeyDown(Keys.Left)) 
                 player.x -= (player.movementSpeed * delta / 1000);
           if (player.game.currentKeyboardState.IsKeyDown(Keys.Right)) 
                 player.x += (player.movementSpeed * delta / 1000);

            if(player.x < 0) player.x = 0;
            if(player.x  > max) player.x = max;

            previousGameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
        }

        public void ResetGameTime(GameTime gameTime)
        {
            previousGameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
        }



    }
}

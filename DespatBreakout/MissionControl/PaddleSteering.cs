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
            this.delta = 0; 
            this.max = 0;
            this.previousGameTime = new GameTime();
        }

        public void Update(GameTime gameTime)
        {
            this.max = this.player.game.GraphicsDevice.Viewport.Width - this.player.sourceRectangle.Width;
           this.delta = gameTime.TotalGameTime.Milliseconds - this.previousGameTime.TotalGameTime.Milliseconds;
           if (this.delta < 0) this.delta = 1000 + this.delta;
           if (this.player.game.currentKeyboardState.IsKeyDown(Keys.Left))
               this.player.x -= (this.player.movementSpeed * this.delta / 1000);
           if (this.player.game.currentKeyboardState.IsKeyDown(Keys.Right))
               this.player.x += (this.player.movementSpeed * this.delta / 1000);

           if (this.player.x < 0) this.player.x = 0;
            if (this.player.x > this.max) this.player.x = this.max;

            this.previousGameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
        }

        public void ResetGameTime(GameTime gameTime)
        {
            this.previousGameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
        }



    }
}

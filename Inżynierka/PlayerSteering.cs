using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class PlayerSteering
    {
        Player player;

        public PlayerSteering(Player player)
        {
            this.player = player;
        }

        public void Update(GameTime gameTime)
        {
             if (Game1.Instance.currentKeyboardState.IsKeyDown(Keys.Left)) 
                 player.x -= player.movementSpeed;
             if (Game1.Instance.currentKeyboardState.IsKeyDown(Keys.Right)) 
                 player.x += player.movementSpeed;

            if(player.x < 0) player.x = 0;
        }




    }
}

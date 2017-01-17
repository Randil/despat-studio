/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a brick extension which makes it more difficult do destroy.
 * Brick decorators can be used in any order.
 * 
 * Design patterns: Decorator
 ---------------------------------------------------------------------------------------------------------*/


using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BrickHard : IBrick
    {

        IBrick baseBrick;
        public int health;

        public BrickHard(IBrick brick, int health)
        {
            baseBrick = brick;
            this.health = health;
        }

        public void Destroy(IBrick brick)
        {
            baseBrick.Destroy(brick);
        }

        public void Hit(IBrick brick)
        {
            if (health <= 0)
                baseBrick.Hit(brick);
            else health--;
        }

        public void Subscribe(IBrickObserver observer)
        {
            baseBrick.Subscribe(observer);
        }

        public void Unsubscribe(IBrickObserver observer)
        {
            baseBrick.Unsubscribe(observer);
        }

        public void LoadContent()
        {
            baseBrick.LoadContent();
        }

        public void Initialize(string textureName, int x, int y)
        {
            baseBrick.Initialize(textureName, x, y);
        }

        public void Update(GameTime gameTime)
        {
            baseBrick.Update(gameTime);
        }

        public Rectangle GetDestinationRectangle()
        {
            return baseBrick.GetDestinationRectangle();
        }

        public void Draw(GameTime gameTime)
        {
             baseBrick.Draw(gameTime);
        }

    }
}

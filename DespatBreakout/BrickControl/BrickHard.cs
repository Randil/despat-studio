/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a brick extension which makes it more difficult do destroy.
 * Brick decorators can be used in any order.
 * 
 * Design patterns: Decorator
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    class BrickHard : IBrick
    {
        public int health;

        IBrick baseBrick;

        public BrickHard(IBrick brick, int health)
        {
            this.baseBrick = brick;
            this.health = health;
        }

        public void Destroy(IBrick brick)
        {
            this.baseBrick.Destroy(brick);
        }

        public void Hit(IBrick brick)
        {
            if (this.health <= 0)
                this.baseBrick.Hit(brick);
            else this.health--;
        }

        public void Subscribe(IBrickObserver observer)
        {
            this.baseBrick.Subscribe(observer);
        }

        public void Unsubscribe(IBrickObserver observer)
        {
            this.baseBrick.Unsubscribe(observer);
        }

        public void LoadContent()
        {
            this.baseBrick.LoadContent();
        }

        public void Initialize(string textureName, int x, int y)
        {
            this.baseBrick.Initialize(textureName, x, y);
        }

        public void Update(GameTime gameTime)
        {
            this.baseBrick.Update(gameTime);
        }

        public Rectangle GetDestinationRectangle()
        {
            return this.baseBrick.GetDestinationRectangle();
        }

        public void Draw(GameTime gameTime)
        {
            this.baseBrick.Draw(gameTime);
        }
    }
}

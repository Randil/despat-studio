using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BrickInvisible : IBrick
    {
        IBrick baseBrick;
        public bool visible;
        
        public BrickInvisible(IBrick brick)
        {
            baseBrick = brick;
            visible = false;
        }

        public void Destroy(IBrick brick)
        {
            baseBrick.Destroy(brick);
        }

        public void Hit(IBrick brick)
        {
            baseBrick.Hit(brick);
            visible = true;
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

        public void Initialize(String textureName, int x, int y)
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
            if (visible == true)
            {
                baseBrick.Draw(gameTime);
            }
        }

    }
}

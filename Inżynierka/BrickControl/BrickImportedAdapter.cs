using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class BrickImportedAdapter : IBrick
    {
        BrickImported baseBrick;
        DespatBreakout game;
        List<IBrickObserver> observers;

        public BrickImportedAdapter(DespatBreakout game)
        {
            this.game = game;
            baseBrick = new BrickImported(game);
        }
        public void Destroy(IBrick brick)
        {
            foreach (IBrickObserver o in observers)
                o.BrickDestroyed(brick);

            // TODO: Visual or sound effect?
        }

        public void Hit(IBrick brick)
        {
            baseBrick.HitByBall();
                if (baseBrick.RemainingHits == 0)
            brick.Destroy(brick);
        }
        public void Subscribe(IBrickObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IBrickObserver observer)
        {
            observers.Remove(observer);
        }
        public void LoadContent()
        {
            BrickImported.LoadTextures(game);
        }
        public void Initialize(String textureName, int x, int y)
        {
            LoadContent();
            baseBrick.Position = new Vector2(x, y);
            baseBrick.Size = new Vector2(100, 40);
            baseBrick.UpdateDrawingRectangles(new Rectangle(0, 0, 100, 100));
            baseBrick.Color = Color.White;
            baseBrick.Reset();
            observers = new List<IBrickObserver> { };
        }
        public void Update(GameTime gameTime)
        {
            baseBrick.Update(gameTime);
        }
        public Rectangle GetDestinationRectangle()
        {
            return baseBrick.mDrawingRectangle;
        }
        public void Draw(GameTime gameTime)
        {
             baseBrick.Draw(gameTime);
        }
    }
}

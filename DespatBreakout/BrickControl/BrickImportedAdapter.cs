/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This is an adapter for BrickImported class. It serves as a base (equivalent for Brick) for bricks decorators classes.
 * 
 * Design patterns: Adapter, Decorator, Observer
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    class BrickImportedAdapter : IBrick
    {
        BrickImported baseBrick;
        DespatBreakout game;
        List<IBrickObserver> observers;

        public BrickImportedAdapter(DespatBreakout game)
        {
            this.game = game;
            this.baseBrick = new BrickImported(game);
        }

        public void Destroy(IBrick brick)
        {
            foreach (IBrickObserver o in this.observers)
                o.BrickDestroyed(brick);

            // TODO: Visual or sound effect?
        }

        public void Hit(IBrick brick)
        {
            this.baseBrick.HitByBall();
            if (this.baseBrick.RemainingHits == 0)
            brick.Destroy(brick);
        }

        public void Subscribe(IBrickObserver observer)
        {
            this.observers.Add(observer);
        }

        public void Unsubscribe(IBrickObserver observer)
        {
            this.observers.Remove(observer);
        }

        public void LoadContent()
        {
            BrickImported.LoadTextures(this.game);
        }

        public void Initialize(string textureName, int x, int y)
        {
            this.LoadContent();
            this.baseBrick.Position = new Vector2(x, y);
            this.baseBrick.Size = new Vector2(100, 40);
            this.baseBrick.UpdateDrawingRectangles(new Rectangle(0, 0, 100, 100));
            this.baseBrick.Color = Color.White;
            this.baseBrick.Reset();
            this.observers = new List<IBrickObserver> { };
        }

        public void Update(GameTime gameTime)
        {
            this.baseBrick.Update(gameTime);
        }

        public Rectangle GetDestinationRectangle()
        {
            return this.baseBrick.mDrawingRectangle;
        }

        public void Draw(GameTime gameTime)
        {
            this.baseBrick.Draw(gameTime);
        }
    }
}

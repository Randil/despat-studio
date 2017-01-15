using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class Brick : Microsoft.Xna.Framework.DrawableGameComponent, IBrick
    {
        int x, y;
        private Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        string textureName;
        DespatBreakout game;
        SpriteBatch spriteBatch;
        List<IBrickObserver> observers;

         public Brick(DespatBreakout game) : base(game)
        {
            this.game = game;
            observers = new List<IBrickObserver> { };
        }

        public void Destroy(IBrick brick)
        {
            foreach (IBrickObserver o in observers)
                o.BrickDestroyed(brick);

            //TODO: Visual or sound effect?
        }

        public void Hit(IBrick brick)
        {
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

         new public void LoadContent()
        {
            base.LoadContent();
        }

           public void Initialize(String textureName, int x, int y)
        {

            LoadContent();
            this.x = x;
            this.y = y;
            this.textureName = textureName;

            sourceRectangle = game.gameTextures.GetTextureRectangle(textureName);
            destinationRectangle = new Rectangle(x, y, sourceRectangle.Width, sourceRectangle.Height);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(game.gameTextures.textureSheet,
                destinationRectangle,
                sourceRectangle,
                Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        public Rectangle GetDestinationRectangle()
        {
            return this.destinationRectangle;
        }

    }
}

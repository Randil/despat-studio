using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class Brick : Microsoft.Xna.Framework.DrawableGameComponent
    {
        int x, y;
        private Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        string textureName;
        Game1 game;
        SpriteBatch spriteBatch;
        List<IBrickObserver> observers;

         public Brick(Game1 game) : base(game)
        {
            this.game = game;
            observers = new List<IBrickObserver> { };
        }

        public void destroy()
        {
            foreach (IBrickObserver o in observers)
                o.BrickDestroyed(this);

            //TODO: Visual or sound effect?
        }

        public void subscribe(IBrickObserver observer)
        {
            observers.Add(observer);
        }

        public void unsubscribe(IBrickObserver observer)
        {
            observers.Remove(observer);
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

           public void Initialize(String textureName, int x, int y)
        {

            LoadContent();
            this.x = x;
            this.y = y;
            this.textureName = textureName;

            sourceRectangle = Game1.Instance.gameTextures.getTextureRectangle(textureName);
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
            spriteBatch.Draw(Game1.Instance.gameTextures.textureSheet,
                destinationRectangle,
                sourceRectangle,
                Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
       
    }
}

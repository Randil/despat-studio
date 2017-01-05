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
        Texture2D texture;
        Game1 game;
        SpriteBatch spriteBatch;

         public Brick(Game1 game) : base(game)
        {
            this.game = game;
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(Texture2D texture, int x, int y)
        {

            LoadContent();
            this.texture = texture;
            this.x = x;
            this.y = y;
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
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
       
    }
}

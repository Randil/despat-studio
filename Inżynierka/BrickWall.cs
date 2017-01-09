using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class BrickWall : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public List<Brick> wall;
        Game1 game;
        SpriteBatch spriteBatch;

         public BrickWall(Game1 game) : base(game)
        {
            this.game = game;
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(List<Brick> wall)
        {

            LoadContent();
            this.wall = wall;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Brick b in wall)
                b.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Brick b in wall)
                b.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void removeBrick(Brick brick)
        {
            wall.Remove(brick);
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class BrickWall : Microsoft.Xna.Framework.DrawableGameComponent, IBrickObserver
    {
        public List<IBrick> wall;
        DespatBreakout game;
        SpriteBatch spriteBatch;

         public BrickWall(DespatBreakout game) : base(game)
        {
            this.game = game;
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(List<IBrick> wall)
        {

            LoadContent();
            this.wall = wall;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IBrick b in wall)
                b.Update(gameTime);
            if (wall.Count == 0)
                game.activeMission.MissionSuccess();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (IBrick b in wall)
                b.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void BrickDestroyed(IBrick brick)
        {
            wall.Remove(brick);
        }

    }
}

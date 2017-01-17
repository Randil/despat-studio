/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents all the bricks in the inividual level.
 * It could be also represented only with list of bricks in the MissionScreen class, but passing it as an argument
 * and handling Update and Draw methods seem to be more elegant this way.
 * BrickWall is registered as an observer of its bricks. It is used to remove a brick from BrickWall after it is destroyed 
 * (collision strategies work on particular bricks, so it is a convenient solution).
 * 
 * Design patterns: Observer
 ---------------------------------------------------------------------------------------------------------*/


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

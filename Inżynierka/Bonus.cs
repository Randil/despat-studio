﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class Bonus : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Paddle player;
        public DespatBreakout game;
        float x, y;
        public Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        SpriteBatch spriteBatch;
        GameTime previousGameTime;
        string textureName;
        float ySpeed = 200f;
        int delta;

        public Bonus(DespatBreakout game, Paddle player) : base(game)
        {
            this.player = player;
            this.game = game;
        }
        public virtual void GrantBonus()
        {

        }

        new public void LoadContent()
        {
            base.LoadContent();
        }
        public void Initialize(String textureName, int x, int y)
        {
            LoadContent();
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.textureName = textureName;
            this.x = x;
            this.y = y;
            sourceRectangle = game.gameTextures.GetTextureRectangle(textureName);
            destinationRectangle = new Rectangle(x, y, sourceRectangle.Width, sourceRectangle.Height);
            previousGameTime = new GameTime();
        }
        override public void Update(GameTime gameTime)
        {
            if (destinationRectangle.Intersects(player.destinationRectangle))
                GrantBonus();

            float delta = gameTime.TotalGameTime.Milliseconds - previousGameTime.TotalGameTime.Milliseconds;
            previousGameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            if (delta < 0) delta = 1000 + delta;

            this.y += (ySpeed * delta / 1000);

            destinationRectangle = new Rectangle((int)x, (int)y, sourceRectangle.Width, sourceRectangle.Height);

            base.Update(gameTime);
        }
        override public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(game.gameTextures.textureSheet,
                destinationRectangle,
                sourceRectangle,
                Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}

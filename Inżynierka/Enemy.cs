using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespatShooter 
{
    class Enemy
    {
        public Texture2D EnemyTexture;
        public Vector2 Position;
        public bool Active;
        public int Health;
  
        public int Damage;
        public int Value;
        float enemyMoveSpeed;

        public int Width
        {
            get { return EnemyTexture.Width; }
        }

        public int Height
        {
            get { return EnemyTexture.Height; }
        }


        public void Initialize(Texture2D texture, Vector2 position)
        {

            EnemyTexture = texture;
            Position = position;
            Active = true;
            Health = 10;
            Value = 100;
            enemyMoveSpeed = 3f;

        }

        public void Update(GameTime gameTime)
        {
            Position.Y += enemyMoveSpeed;

            // If the enemy is past the screen or its health reaches 0 then deactivate it

            if (Position.Y > 600 || Health <= 0)
            {

                // By setting the Active flag to false, the game will remove this objet from the

                // active game list

                Active = false;

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EnemyTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f,
            SpriteEffects.None, 0f);
        }

    }
}

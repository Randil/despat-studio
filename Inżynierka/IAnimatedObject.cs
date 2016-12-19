using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespatShooter
{
    interface IAnimatedObject
    {
        void Update(GameTime gameTime);
        void Initialize(Texture2D texture, Vector2 position);
        void Draw(SpriteBatch spriteBatch);

    }
}

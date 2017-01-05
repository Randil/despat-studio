using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class MainMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public List<Button> buttons = new List<Button> { };
        private SpriteFont menuFont;
        private Game game;
        SpriteBatch spriteBatch;

        public MainMenu(Game1 game) : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            menuFont = game.Content.Load<SpriteFont>("Fonts/MainMenu");
            base.LoadContent();
        }

        public override void Initialize()
        {

            LoadContent();
            buttons.Add(new Button(Game1.Instance));
            buttons[0].Initialize(menuFont, new Vector2(100, 100), "NEW GAME");
            buttons.Add(new Button(Game1.Instance));
            buttons[1].Initialize(menuFont, new Vector2(100, 200), "ACHIEVEMENTS");
            buttons.Add(new Button(Game1.Instance));
            buttons[2].Initialize(menuFont, new Vector2(100, 300), "HOW TO PLAY");
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

            foreach(Button b in buttons)
            {
                b.Draw(gameTime);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    

    }
}

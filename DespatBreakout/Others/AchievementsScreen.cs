/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a generic menu with buttons, with keyboard navigation.
 * 
 * Design patterns: 
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    class AchievementsScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        DespatBreakout game;
        SpriteBatch spriteBatch;

        AchievementsManager manager;
        RectangleFontAdjuster helper;
        Rectangle bricksRectangle;
        Rectangle finishedRectangle;
        Rectangle failedRectangle;
        Rectangle sourceRectangle;
        SpriteFont font;

        public AchievementsScreen(DespatBreakout game)
            : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("Fonts/MainMenu");
            base.LoadContent();
        }

        public void Initialize(AchievementsManager manager)
        {
            LoadContent();
            this.manager = manager;
            this.helper = new RectangleFontAdjuster();
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            this.bricksRectangle = new Rectangle(100, 30, 580, 100);
            this.finishedRectangle = new Rectangle(100, 130, 580, 100);
            this.failedRectangle = new Rectangle(100, 230, 580, 100);
            this.sourceRectangle = game.buttonTextures.GetTextureRectangle("grey_button08.png");

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        { 
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(game.buttonTextures.textureSheet, bricksRectangle, sourceRectangle, Color.White);
            helper.DrawButtonText(spriteBatch, font, "TOTAL BRICKS DESTROYED: " + manager.bricksDestroyed, bricksRectangle);

            spriteBatch.Draw(game.buttonTextures.textureSheet, finishedRectangle, sourceRectangle, Color.White);
            helper.DrawButtonText(spriteBatch, font, "TOTAL MISSIONS COMPLETED: " + manager.missionsFinished, finishedRectangle);

            spriteBatch.Draw(game.buttonTextures.textureSheet, failedRectangle, sourceRectangle, Color.White);
            helper.DrawButtonText(spriteBatch, font, "TOTAL MISSIONS FAILED: " + manager.missionsFailed, failedRectangle);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

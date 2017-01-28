/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents the screen drawn over MissionScreen after level finishes.
 * 
 * Design patterns: 
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout 
{
    public class MissionFinishedScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        DespatBreakout game;
        SpriteBatch spriteBatch;
        string finishString;
        double time;
        Rectangle destinationRectangle;
        Rectangle sourceRectangle;
        Rectangle upperTextRectangle;
        Rectangle lowerTextRectangle;
        SpriteFont font;
        RectangleFontAdjuster helper;

        public MissionFinishedScreen(DespatBreakout game)
            : base(game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            helper = new RectangleFontAdjuster();
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(double time, string finishString)
        {
            LoadContent();
            this.time = time;
            this.finishString = finishString;
            this.font = game.Content.Load<SpriteFont>("Fonts/MainMenu");
            this.destinationRectangle = new Rectangle(100, 100, 600, 300);
            this.upperTextRectangle = new Rectangle(110, 110, 580, 120);
            this.lowerTextRectangle = new Rectangle(110, 200, 580, 150);
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

            spriteBatch.Draw(game.buttonTextures.textureSheet, destinationRectangle, sourceRectangle, Color.White);

            helper.DrawButtonText(spriteBatch, font, finishString, upperTextRectangle);

            helper.DrawButtonText(spriteBatch, font, "You have played this level for: " + (int)time + " seconds", lowerTextRectangle);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class menu of choosing mission. It is parsing XML with level list, and prepares buttons according to it.
 * Optionally if a saved game exists, it adds aditional button to continue the mission.
 * 
 * Design patterns: Memento
 ---------------------------------------------------------------------------------------------------------*/


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DespatBreakout
{
    class MenuMissions : Menu
    {
        public int indexStart = 0;
        public int indexEnd;
        public XmlDocument missionsXML;

        DespatBreakout game;
        SpriteBatch spriteBatch;
        Rectangle arrowUpSource;
        Rectangle arrowUpDestination;
        Rectangle arrowDownSource;
        Rectangle arrowDownDestination;

        public MenuMissions(DespatBreakout game) : base(game)
        {
            this.game = game;
        }

        /// <summary>
        /// This could also be done with an external factory, as in MissionScreen - MissionParser
        /// </summary>
        public void Initialize(XmlDocument missionsXML)
        {
            LoadContent();
            this.missionsXML = missionsXML;
            string text;
            string filename;
            string besttime;
            int height = 80;

            if (game.missionSave.saved == true)
            {
                text = "Continue saved game";
                ButtonMission button = new ButtonMission(game, DespatBreakout.GameState.Mission, game.missionSave);
                button.Initialize(menuFont, 300, height, text, "grey_button15.png");
                height += 70;
                buttons.Add(button);
            }

            foreach (XmlNode node in missionsXML.DocumentElement.ChildNodes)
            {
                text = node.Attributes["name"].InnerText;
                filename = node.Attributes["file"].InnerText;
                besttime = node.Attributes["bestTime"].InnerText;
                XmlDocument scenarioXML = new XmlDocument();
                scenarioXML.Load("..\\..\\..\\..\\Content\\Levels\\" + filename);
                ButtonMission button = new ButtonMission(game, DespatBreakout.GameState.Mission, scenarioXML);
                button.Initialize(menuFont, 300, height, text, "grey_button15.png");
                height += 70;
                buttons.Add(button);
            }

            if (buttons.Count() > 5) indexEnd = 4;
            else indexEnd = buttons.Count() - 1;

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            activeButton = buttons[activeButtonIndex];
            activeButton.isHoovered = true;

            arrowUpSource = game.buttonTextures.GetTextureRectangle("grey_sliderUp.png");
            arrowUpDestination = new Rectangle(381, 30, arrowUpSource.Width, arrowUpSource.Height);
            arrowDownSource = game.buttonTextures.GetTextureRectangle("grey_sliderDown.png");
            arrowDownDestination = new Rectangle(381, 420, arrowDownSource.Width, arrowDownSource.Height);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.currentKeyboardState.IsKeyDown(Keys.Down) && game.previousKeyboardState.IsKeyUp(Keys.Down))
            {
                activeButton.isHoovered = false;
                activeButtonIndex = activeButtonIndex + 1;

                if (activeButtonIndex > buttons.Count - 1)
                {
                    activeButtonIndex = 0;
                    if (buttons.Count > 5)
                        indexEnd = 4;
                    else indexEnd = buttons.Count - 1;
                    
                    indexStart = 0;
                }
                else if (activeButtonIndex > indexEnd)
                {
                    indexEnd++;
                    indexStart++;
                }

                activeButton = buttons[activeButtonIndex];
                activeButton.isHoovered = true;
            }

            if (game.currentKeyboardState.IsKeyDown(Keys.Up) && game.previousKeyboardState.IsKeyUp(Keys.Up))
            {
                activeButton.isHoovered = false;
                activeButtonIndex = (activeButtonIndex - 1);
                if (activeButtonIndex < 0)
                {
                    activeButtonIndex = buttons.Count - 1;
                    if (activeButtonIndex > 4)
                        indexStart = activeButtonIndex - 4;
                    else indexStart = 0;
                    indexEnd = activeButtonIndex;
                }
                else if (activeButtonIndex < indexStart)
                {
                    indexEnd--;
                    indexStart--;
                }
                activeButton = buttons[activeButtonIndex];
                activeButton.isHoovered = true;
            }

            if (game.currentKeyboardState.IsKeyDown(Keys.Enter) && game.previousKeyboardState.IsKeyUp(Keys.Enter))
            {
                activeButton.Click();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (indexStart > 0)
                spriteBatch.Draw(game.buttonTextures.textureSheet, arrowUpDestination, arrowUpSource, Color.White);
                
            for (int i = indexStart; i <= indexEnd; i++)
            {
                buttons[i].destinationRectangle.Y = 80 + (70 * (i - indexStart));
                buttons[i].Draw(gameTime);
            }

            if (indexEnd < buttons.Count - 1)
                spriteBatch.Draw(game.buttonTextures.textureSheet, arrowDownDestination, arrowDownSource, Color.White);

            spriteBatch.End();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DespatShooter
{
    class MenuMissions : Menu
    {
        private Game game;
        SpriteBatch spriteBatch;
        public XmlDocument missionsXML;
     

        public MenuMissions(Game1 game) : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(XmlDocument missionsXML)
        {
            LoadContent();
            this.missionsXML = missionsXML;
            string text;
            string filename;
            string besttime;
            int height = 150;

            foreach (XmlNode node in missionsXML.DocumentElement.ChildNodes)
            {
                text = node.Attributes["name"].InnerText;
                filename = node.Attributes["file"].InnerText;
                besttime = node.Attributes["bestTime"].InnerText;
                XmlDocument scenarioXML = new XmlDocument();
                scenarioXML.Load("..\\..\\..\\..\\Content\\Levels\\" + filename);
                ButtonMission button = new ButtonMission(Game1.Instance, Game1.GameState.Mission, scenarioXML);
                button.Initialize(menuFont, 150, height, text, "grey_button15.png");
                height += 70;
                buttons.Add(button);
            }

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            activeButton = buttons[activeButtonIndex];
            activeButton.isHoovered = true;
            base.Initialize();
        }

    }
}

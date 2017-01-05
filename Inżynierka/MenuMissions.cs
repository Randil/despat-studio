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
        public XmlDocument missionsXML = new XmlDocument();
     

        public MenuMissions(Game1 game) : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            missionsXML.Load("..\\..\\..\\..\\missions.xml");
            base.LoadContent();
        }

        public override void Initialize()
        {
            LoadContent();
            buttons.Add(new ButtonMission(Game1.Instance, missionsXML));
            buttons[0].Initialize(menuFont, 150, 150, "MISSION1", "grey_button15.png", Game1.GameState.Mission);
            buttons.Add(new ButtonMission(Game1.Instance, missionsXML));
            buttons[1].Initialize(menuFont, 150, 220, "MISSION2", "grey_button15.png", Game1.GameState.MainMenu);
            buttons.Add(new ButtonMission(Game1.Instance, missionsXML));
            buttons[2].Initialize(menuFont, 150, 290, "MISSION3", "grey_button15.png", Game1.GameState.MainMenu);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            activeButton = buttons[activeButtonIndex];
            activeButton.isHoovered = true;
            base.Initialize();
        }

    }
}

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
    class ButtonMission : Button
    {
        private Game game;
        XmlDocument missionScenario;

        public ButtonMission(Game1 game, Game1.GameState clickDestination, XmlDocument missionScenario)
            : base(game, clickDestination)
        {
            this.game = game;
            this.clickDestination = clickDestination;
            this.missionScenario = missionScenario;
        }


        public override void Click()
        {
            Game1.Instance.missionParser.createMission(missionScenario);
        }
    }
}

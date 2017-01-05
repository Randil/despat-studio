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

        public ButtonMission(Game1 game, XmlDocument missionScenario)
            : base(game)
        {
            this.missionScenario = missionScenario;
            this.game = game;
        }

        public override void Click()
        {
            Game1.Instance.missionParser.createMission(missionScenario);
        }
    }
}

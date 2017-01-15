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
        XmlDocument missionScenario;

        public ButtonMission(DespatBreakout game, DespatBreakout.GameState clickDestination, XmlDocument missionScenario)
            : base(game, clickDestination)
        {
            this.game = game;
            this.clickDestination = clickDestination;
            this.missionScenario = missionScenario;
        }


        public override void Click()
        {
            DespatBreakout.Instance.missionParser.CreateMission(missionScenario);
        }
    }
}

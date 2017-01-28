/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class is a button extension that represents a button in MissionMenu
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
    using System.Xml;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class ButtonMission : Button
    {
        XmlDocument missionScenario;
        MissionSave parsedMission = null;

        public ButtonMission(DespatBreakout game, DespatBreakout.GameState clickDestination, XmlDocument missionScenario)
            : base(game, clickDestination)
        {
            this.game = game;
            this.clickDestination = clickDestination;
            this.missionScenario = missionScenario;
        }

        public ButtonMission(DespatBreakout game, DespatBreakout.GameState clickDestination, MissionSave missionSave)
            : base(game, clickDestination)
        {
            this.game = game;
            this.clickDestination = clickDestination;
            this.parsedMission = missionSave;
        }


        public override void Click()
        {
            if (parsedMission != null)
            {
                DespatBreakout.Instance.activeMission.Initialize(parsedMission);
                DespatBreakout.Instance.currentGameState = DespatBreakout.GameState.Mission;
            }
            else
            {
                DespatBreakout.Instance.activeMission.Initialize(DespatBreakout.Instance.missionParser.CreateMission(missionScenario));
                DespatBreakout.Instance.currentGameState = DespatBreakout.GameState.Mission;
            }
        }
    }
}

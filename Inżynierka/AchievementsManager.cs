using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DespatShooter
{
    public class AchievementsManager : IBrickObserver
    {

        public int missionsFinished;
        public int bricksDestroyed;
        XmlDocument achievementsXML;

        public AchievementsManager (XmlDocument achievementsXML)
        {
            this.achievementsXML = achievementsXML;

            XmlNode missionsFinishedNode = achievementsXML.SelectSingleNode("descendant::missionsFinished");
            missionsFinished = Int32.Parse(missionsFinishedNode.Attributes["number"].InnerText);

            XmlNode bricksDestroyedNode = achievementsXML.SelectSingleNode("descendant::bricksDestroyed");
            bricksDestroyed = Int32.Parse(bricksDestroyedNode.Attributes["number"].InnerText);

        }

        public void BrickDestroyed(Brick brick)
        {
            bricksDestroyed++;
        }

        public void MissionFinished(double time)
        {
            missionsFinished++;
        }

        public void SaveAchievements()
        {

        }

    }
}

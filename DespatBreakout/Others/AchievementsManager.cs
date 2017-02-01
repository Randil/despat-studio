/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class contains information about player achievements throughout the game. 
 * Class shows two approaches to tracking achievements: being an Observer of certain classes, and using a logging Proxy.
 * Class also handles serialization of achievements data between sessions - they are updated every time level is finished.
 * 
 * Design patterns: Proxy, Observer
 ---------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DespatBreakout
{
    public class AchievementsManager : IBrickObserver
    {

        public int missionsFinished;
        public int missionsFailed;
        public int bricksDestroyed;

        XmlDocument achievementsXML;
        string pathXML;

        XmlNode missionsFinishedNode;
        XmlNode missionsFailedNode;
        XmlNode bricksDestroyedNode;

        public AchievementsManager(XmlDocument achievementsXML, string path)
        {
            this.achievementsXML = achievementsXML;
            this.pathXML = path;

            missionsFinishedNode = achievementsXML.SelectSingleNode("descendant::missionsFinished");
            this.missionsFinished = Int32.Parse(missionsFinishedNode.Attributes["number"].InnerText);

            missionsFailedNode = achievementsXML.SelectSingleNode("descendant::missionsFailed");
            this.missionsFailed = Int32.Parse(missionsFailedNode.Attributes["number"].InnerText);

            bricksDestroyedNode = achievementsXML.SelectSingleNode("descendant::bricksDestroyed");
            this.bricksDestroyed = Int32.Parse(bricksDestroyedNode.Attributes["number"].InnerText);
        }

        public void BrickDestroyed(IBrick brick)
        {
            this.bricksDestroyed++;
        }

        public void MissionFinished(double time, bool result)
        {
            if (result)
                this.missionsFinished++;
            else
                this.missionsFailed++;

            SaveAchievements();
        }

        public void SaveAchievements()
        {
            missionsFinishedNode.Attributes["number"].Value = "" + missionsFinished;
            missionsFailedNode.Attributes["number"].Value = "" + missionsFailed;
            bricksDestroyedNode.Attributes["number"].Value = "" + bricksDestroyed;
            achievementsXML.Save(pathXML);
        }

    }
}

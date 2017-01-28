/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class contains information about player achievements throughout the game. 
 * Class shows two approaches to tracking achievements: being an Observer of certain classes, and using a logging Proxy.
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

        public void BrickDestroyed(IBrick brick)
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

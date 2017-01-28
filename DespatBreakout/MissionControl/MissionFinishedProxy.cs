/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class is a logging proxy, called after mission finishes. It passes information about finished mission to AchievementsManager
 * and initializes MissionFinishedScreen.
 * 
 * Design patterns: Proxy
 ---------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    public class MissionFinishedProxy
    {
        public MissionFinishedProxy()
        {

        }
        public void MissionFinished(double time, MissionFinishedScreen finishScreen, string text)
        {
            DespatBreakout.Instance.achievements.MissionFinished(time);
            finishScreen.Initialize(time, text);
        }
                      
    }
}

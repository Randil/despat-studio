using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class MissionFinishedProxy
    {
        public MissionFinishedProxy()
        {

        }
        public void missionFinished(double time, MissionFinishedScreen finishScreen)
        {
            Game1.Instance.achievements.MissionFinished(time);
            finishScreen.Initialize(time);
        }
                      
    }
}

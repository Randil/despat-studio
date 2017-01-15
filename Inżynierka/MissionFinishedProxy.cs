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
        public void MissionFinished(double time, MissionFinishedScreen finishScreen, string text)
        {
            DespatBreakout.Instance.achievements.MissionFinished(time);
            finishScreen.Initialize(time, text);
        }
                      
    }
}

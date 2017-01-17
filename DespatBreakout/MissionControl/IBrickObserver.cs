/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This interface is implemented by every class that needs to be informed about a brick being destroyed.
 * 
 * Design patterns: Observer
 ---------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public interface IBrickObserver
    {
        void BrickDestroyed(IBrick brick);
    }
}

/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This is an interface implemented by BonusCommands.
 * 
 * Design patterns: Command
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBonusCommand
    {
        void GrantBonus();
    }
}

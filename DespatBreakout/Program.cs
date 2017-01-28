/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This is the main entry point for the application. 
 * It was written according to instructions from monogame documentation - see more at http://www.monogame.net/.
 * 
 * Design patterns:
 ---------------------------------------------------------------------------------------------------------*/

using System;

namespace DespatBreakout
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = DespatBreakout.Instance)
                game.Run();
        }
    }
}

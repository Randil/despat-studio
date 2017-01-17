/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This is the main entry point for the application.
 * 
 * Design patterns:
 ---------------------------------------------------------------------------------------------------------*/

using System;

namespace DespatShooter
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

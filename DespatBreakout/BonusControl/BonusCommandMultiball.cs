/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a command which spawns a duplicate of every ball currently in the game.
 * 
 * Design patterns: Command, Prototype
 ---------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    class BonusCommandMultiball : IBonusCommand
    {
        DespatBreakout game;
        Paddle player;
        public BonusCommandMultiball(DespatBreakout game, Paddle player)
        {
            this.player = player;
            this.game = game;
        }

        public void GrantBonus()
        {
            List<Ball> ballsToAdd = new List<Ball> { };
            foreach (Ball ball in game.activeMission.balls)
            {
                ballsToAdd.Add(ball.Duplicate());
                ball.xSpeed += 50;
                ball.ySpeed -= 50;
            }

            foreach (Ball ball in ballsToAdd)
            {
                ball.xSpeed -= 50;
                ball.ySpeed += 50;
                game.activeMission.balls.Add(ball);
            }
        }
    }
}

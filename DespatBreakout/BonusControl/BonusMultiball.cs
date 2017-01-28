/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a bonus spawning a new ball.
 * Instead of directly changing a player, it creates a BonusCommand, which is afterwards handled by MissionScreen.
 * This approach allows the mission to be easly initialized with a set of bonuses.
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
    using Microsoft.Xna.Framework;

    class BonusAditionalBall : Bonus
    {
        public BonusAditionalBall(DespatBreakout game, Paddle player) : base(game, player)
        {
            this.player = player;
            this.game = game;
        }

        public override void CollectBonus()
        {
            game.activeMission.BonusCollected(this);
            game.activeMission.bonusEffects.Add(new BonusCommandMultiball(game, player));
        }
    }
}

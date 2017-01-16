using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
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

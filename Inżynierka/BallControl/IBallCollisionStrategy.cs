using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public interface IBallCollisionStrategy
    {
        void CheckCollisions();
        void CalculateReflectionBrick(Ball.HitSide hitSide, IBrick brick);
        void CalculateReflectionWall(Ball.HitSide hitSide);
        void CalculateReflectionPaddle();
        IBallCollisionStrategy Duplicate(Ball ball);
    }
}

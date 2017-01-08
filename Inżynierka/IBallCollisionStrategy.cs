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
        void CalculateReflectionBrick();
        void CalculateReflectionWall();
        void CalculateReflectionPaddle();
    }
}

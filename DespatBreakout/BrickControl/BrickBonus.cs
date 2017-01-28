/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a brick extension, making it spawn a bonus after being destroyed.
 * Brick decorators can be used in any order.
 * 
 * Design patterns: Decorator
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    class BrickBonus : IBrick
    {
        IBrick baseBrick;

        public BrickBonus(IBrick brick)
        {
            this.baseBrick = brick;
        }

        public void Destroy(IBrick brick)
        {
            this.SpawnBonus();
            this.baseBrick.Destroy(brick);
        }

        public void Hit(IBrick brick)
        {
            this.baseBrick.Hit(brick);
        }

        public void Subscribe(IBrickObserver observer)
        {
            this.baseBrick.Subscribe(observer);
        }

        public void Unsubscribe(IBrickObserver observer)
        {
            this.baseBrick.Unsubscribe(observer);
        }

        public void LoadContent()
        {
            this.baseBrick.LoadContent();
        }

        public void Initialize(string textureName, int x, int y)
        {
            this.baseBrick.Initialize(textureName, x, y);
        }

        public void Update(GameTime gameTime)
        {
            this.baseBrick.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            this.baseBrick.Draw(gameTime);
        }

        public Rectangle GetDestinationRectangle()
        {
            return this.baseBrick.GetDestinationRectangle();
        }

        public void SpawnBonus()
        {
            // spawn random bonus
            int rand = DespatBreakout.Instance.rand.Next(1, 5);
            switch (rand)
            {
                case 1:
                    {
                        Bonus bonus = new BonusFireball(DespatBreakout.Instance, DespatBreakout.Instance.activeMission.player);
                        bonus.Initialize("bonus_powerBalls.png", this.GetDestinationRectangle().X, 0);
                        DespatBreakout.Instance.activeMission.bonuses.Add(bonus);
                        break;
                    }

                case 2:
                    {
                        Bonus bonus = new BonusSmallPaddle(DespatBreakout.Instance, DespatBreakout.Instance.activeMission.player);
                        bonus.Initialize("bonus_smallPaddle.png", this.GetDestinationRectangle().X, 0);
                        DespatBreakout.Instance.activeMission.bonuses.Add(bonus);
                        break;
                    }

                case 3:
                    {
                        Bonus bonus = new BonusBigPaddle(DespatBreakout.Instance, DespatBreakout.Instance.activeMission.player);
                        bonus.Initialize("bonus_bigPaddle.png", this.GetDestinationRectangle().X, 0);
                        DespatBreakout.Instance.activeMission.bonuses.Add(bonus);
                        break;
                    }

                default:
                    {
                        Bonus bonus = new BonusAditionalBall(DespatBreakout.Instance, DespatBreakout.Instance.activeMission.player);
                        bonus.Initialize("bonus_multiBalls.png", this.GetDestinationRectangle().X, 0);
                        DespatBreakout.Instance.activeMission.bonuses.Add(bonus);
                        break;
                    }
            }
        }
    }
}
/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a game level. It contains instances of balls, player paddle, bricks and bonuses.
 * Class handles delay at the start of the mission.
 * MissionScreen can be initialized via a MissionSave object or only with a BrickWall (then it sets default values for other components).
 * 
 * 
 * Design patterns: Memento, Command, Factory
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    public class MissionScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //MissionScreen is using an external initializer, so its fields have to be public
        public DespatBreakout game;
        public SpriteBatch spriteBatch;
        public bool onGoing;
        public bool finished;
        public double delay;
        public MissionFinishedScreen finishScreen;
        public MissionFinishedProxy finishedProxy;
        
        public GameTime startTime;
        public double time;

        public Paddle player;
        public BrickWall bricks;

        public List<Ball> balls;
        public List<Ball> ballsToRemove;
        public List<Bonus> bonuses;
        public  List<Bonus> bonusesToRemove;
        public List<IBonusCommand> bonusEffects;
        public List<IBonusCommand> bonusEffectsToRemove;

        MissionScreenInitializer initializer;

        public MissionScreen(DespatBreakout game) : base(game)
        {
            this.game = game;
            this.initializer = new MissionScreenInitializer(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(BrickWall bWall)
        {
            LoadContent();
            initializer.Initialize(bWall);
            base.Initialize();
        }

        public void Initialize(MissionSave save)
        {
            LoadContent();
            initializer.Initialize(save);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (finished == false)
            {
                if (onGoing == false)
                {
                    startTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
                    onGoing = true;
                }

                time = gameTime.TotalGameTime.TotalSeconds - startTime.TotalGameTime.TotalSeconds;

                if (time < delay) // We want some time before everything starts running
                {
                    foreach (Ball b in balls)
                        b.ResetGameTime(gameTime);

                    foreach (Bonus bo in bonuses)
                        bo.ResetGameTime(gameTime);

                    player.ResetGameTime(gameTime);
                }
                else 
                {
                    foreach (Ball b in balls)
                        b.Update(gameTime);
                    RemoveBalls();

                    foreach (Bonus bo in bonuses)
                        bo.Update(gameTime);
                    RemoveBonuses();

                    AquireBonuses();

                    player.Update(gameTime);
                    bricks.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Ball b in balls)
            b.Draw(gameTime);

            foreach (Bonus bo in bonuses)
                bo.Draw(gameTime);

            player.Draw(gameTime);
            bricks.Draw(gameTime);

            if (finished == true) finishScreen.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void BallFalled(Ball ball)
        {
            ballsToRemove.Add(ball);
        }

        public void RemoveBalls()
        {
            foreach (Ball b in ballsToRemove)
                balls.Remove(b);
            ballsToRemove.Clear();
            if (balls.Count == 0)
                MissionFailed();
        }
        public void RemoveBonuses()
        {
            foreach(Bonus b in bonusesToRemove)
                bonuses.Remove(b);
            bonusesToRemove.Clear();
        }
        public void BonusCollected(Bonus bonus)
        {
            bonusesToRemove.Add(bonus);
        }
        public void AquireBonuses()
        {
            foreach(IBonusCommand bonus in bonusEffects)
            {
                bonus.GrantBonus();
                bonusEffectsToRemove.Add(bonus);
            }
            foreach(IBonusCommand bonus in bonusEffectsToRemove)
            {
                bonusEffects.Remove(bonus);
            }
        }

        public void MissionSuccess()
        {
            finished = true;
            finishedProxy.MissionFinished(time, finishScreen, "Congratulations, you have won!");
            onGoing = false;
        }

        public void MissionFailed()
        {
            finished = true;
            finishedProxy.MissionFinished(time, finishScreen, "You failed!");
            onGoing = false;
        }


    }
}

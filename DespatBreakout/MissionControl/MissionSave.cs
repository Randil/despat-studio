/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class is used to store mission state after pressing ESC in game.
 * Note that it does not implement serialization, so save will be lost after leaving the game.
 * 
 * Design patterns: Memento
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    public class MissionSave
    {
        DespatBreakout game;

        public GameTime startTime;
        public double timePlayed;
        public String playerTexture;
        public float playerX, playerY;

        public struct BallState
        {
            // In our example type of ball strategy changes along with ball texture
            // Aditional parameters would be required if changes werent tied this way
            public string textureName;
            public float x, y;
            public float xSpeed, ySpeed;
        }
        public List<BallState> balls;

        // Find a balance between keeping only data which is nescessary to recreate the object
        // And convinience in saving and restoring it
        // Concrete implementation will differ with every use case
        public BrickWall bricks;
        public List<Bonus> bonuses;
        public List<IBonusCommand> bonusEffects;

        public bool saved;

        public MissionSave(DespatBreakout game)
        {
            this.game = game;
            this.saved = false;
        }

        public void SaveMissionState(MissionScreen mission)
        {

            balls = new List<BallState> { };

            this.startTime = mission.startTime;
            this.timePlayed = mission.time;

            this.playerTexture = mission.player.texture;
            this.playerX = mission.player.x;
            this.playerY = mission.player.y;

            foreach(Ball b in mission.balls)
            {
                BallState ballState = new BallState();
                ballState.textureName = b.textureName;
                ballState.x = b.x;
                ballState.y = b.y;
                ballState.xSpeed = b.xSpeed;
                ballState.ySpeed = b.ySpeed;
                this.balls.Add(ballState);
            }

            this.bricks = mission.bricks;
            this.bonuses = mission.bonuses;
            this.bonusEffects = mission.bonusEffects;

            this.saved = true;
        }


    }
}

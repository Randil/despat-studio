using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DespatShooter
{
    public class MissionParser
    {
        List<Brick> wall;
        Game1 game;
        Texture2D brickTexture;
        BrickWall bWall;

        public MissionParser(Game1 game)
        {
            this.game = game;
            this.brickTexture = Game1.Instance.Content.Load<Texture2D>("Graphics\\Enemy");
            wall = new List<Brick> { };
            bWall = new BrickWall(game);
        }

        public void createMission(XmlDocument scenario)
        {
            string type;
            int x, y;
            foreach (XmlNode node in scenario.DocumentElement.ChildNodes)
            {
                type = node.Attributes["type"].InnerText;
                x = Int32.Parse(node.Attributes["x"].InnerText);
                y = Int32.Parse(node.Attributes["x"].InnerText);
                Brick brick = new Brick(game);
                brick.Initialize(brickTexture, x, y);
                wall.Add(brick);
            }
            bWall.Initialize(wall);
            Game1.Instance.activeMission.bricks = bWall;
            Game1.Instance.activeMission.Initialize();
            Game1.Instance.currentGameState = Game1.GameState.Mission;

        }
    }
}

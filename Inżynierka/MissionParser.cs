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
        BrickWall bWall;

        public MissionParser(Game1 game)
        {
            this.game = game;
            wall = new List<Brick> { };
            bWall = new BrickWall(game);
        }

        public void createMission(XmlDocument scenario)
        {
            string texture;
            int x, y;
            foreach (XmlNode node in scenario.DocumentElement.ChildNodes)
            {
                texture = node.Attributes["texture"].InnerText;
                x = Int32.Parse(node.Attributes["x"].InnerText);
                y = Int32.Parse(node.Attributes["y"].InnerText);
                Brick brick = new Brick(game);
                brick.Initialize(texture, x, y);
                brick.subscribe(bWall);
                brick.subscribe(Game1.Instance.achievements);
                wall.Add(brick);
            }
            bWall.Initialize(wall);
            Game1.Instance.activeMission.bricks = bWall;
            Game1.Instance.activeMission.Initialize();
            Game1.Instance.currentGameState = Game1.GameState.Mission;

        }
    }
}

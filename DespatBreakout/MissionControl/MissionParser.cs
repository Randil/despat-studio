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
        List<IBrick> wall;
        DespatBreakout game;
        BrickWall bWall;

        public MissionParser(DespatBreakout game)
        {
            this.game = game;
        }

        public void CreateMission(XmlDocument scenario)
        {
            wall = new List<IBrick> { };
            bWall = new BrickWall(game);
            string texture;
            int x, y;

            foreach (XmlNode node in scenario.SelectSingleNode("descendant::bricks").ChildNodes)
            {
                texture = node.Attributes["texture"].InnerText;
                x = Int32.Parse(node.Attributes["x"].InnerText);
                y = Int32.Parse(node.Attributes["y"].InnerText);
                IBrick brick;
                if(node.Attributes["imported"] != null)
                    if (Int32.Parse(node.Attributes["imported"].InnerText) == 1)
                            brick = new BrickImportedAdapter(game);
                    else brick = new Brick(game);
                else brick = new Brick(game);

                brick.Initialize(texture, x, y);
                brick.Subscribe(bWall);
                brick.Subscribe(game.achievements);

                if(node.Attributes["hardness"] != null)
                brick = new BrickHard(brick, Int32.Parse(node.Attributes["hardness"].InnerText));

                if (node.Attributes["invisible"] != null)
                if(Int32.Parse(node.Attributes["invisible"].InnerText) == 1)   
                    brick = new BrickInvisible(brick);

                if (node.Attributes["bonus"] != null)
                    if (Int32.Parse(node.Attributes["bonus"].InnerText) == 1)
                        brick = new BrickBonus(brick);

                wall.Add(brick);
            }

            bWall.Initialize(wall);
            game.activeMission.Initialize(bWall);
            game.currentGameState = DespatBreakout.GameState.Mission;

        }
    }
}

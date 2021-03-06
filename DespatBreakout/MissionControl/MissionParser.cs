﻿/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class is parsing XML mission scenarios to prepare bricks for MissionScreen.
 * 
 * Design patterns: Interpreter, Factory
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DespatBreakout
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

        public BrickWall CreateMission(XmlDocument scenario)
        {
            this.wall = new List<IBrick> { };
            this.bWall = new BrickWall(this.game);
            string texture;
            int x, y;

            foreach (XmlNode node in scenario.SelectSingleNode("descendant::bricks").ChildNodes)
            {
                texture = node.Attributes["texture"].InnerText;
                x = Int32.Parse(node.Attributes["x"].InnerText);
                y = Int32.Parse(node.Attributes["y"].InnerText);
                IBrick brick;
                if (node.Attributes["imported"] != null)
                    if (Int32.Parse(node.Attributes["imported"].InnerText) == 1)
                        brick = new BrickImportedAdapter(this.game);
                    else brick = new Brick(game);
                else brick = new Brick(game);

                brick.Initialize(texture, x, y);
                brick.Subscribe(this.bWall);
                brick.Subscribe(this.game.achievements);

                if (node.Attributes["hardness"] != null)
                brick = new BrickHard(brick, Int32.Parse(node.Attributes["hardness"].InnerText));

                if (node.Attributes["invisible"] != null)
                if (Int32.Parse(node.Attributes["invisible"].InnerText) == 1)   
                    brick = new BrickInvisible(brick);

                if (node.Attributes["bonus"] != null)
                    if (Int32.Parse(node.Attributes["bonus"].InnerText) == 1)
                        brick = new BrickBonus(brick);

                this.wall.Add(brick);
            }

            this.bWall.Initialize(this.wall);
            return this.bWall;
        }
    }
}

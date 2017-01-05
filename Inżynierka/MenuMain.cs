﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class MenuMain : Menu
    {
        private Game game;
        SpriteBatch spriteBatch;

        public MenuMain(Game1 game) : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            LoadContent();
            buttons.Add(new Button(Game1.Instance));
            buttons[0].Initialize(menuFont, 300, 150, "NEW GAME", "grey_button15.png", Game1.GameState.MissionChoice);
            buttons.Add(new Button(Game1.Instance));
            buttons[1].Initialize(menuFont, 300, 220, "ACHIEVEMENTS", "grey_button15.png", Game1.GameState.Achievements);
            buttons.Add(new Button(Game1.Instance));
            buttons[2].Initialize(menuFont, 300, 290, "HOW TO PLAY", "grey_button15.png", Game1.GameState.Tutorial);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            activeButton = buttons[activeButtonIndex];
            activeButton.isHoovered = true;
            base.Initialize();
        }

    }
}

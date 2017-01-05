using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;


namespace DespatShooter
{
    //This class contains main game loop and handles user input. Main menu and mission class are initialized here.

    //Pattern - Game Loop, see more at http://gameprogrammingpatterns.com/game-loop.html

    public class Game1 : Game
    {
        private static Game1 instance;

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        MouseState currentMouseState;
        MouseState previousMouseState;

        enum GameState
        {
            MainMenu, Mission, Achievements, MissionChoice,
        }
        GameState currentGameState = GameState.MainMenu;

        MainMenu menu;

        private Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public static Game1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game1();
                }
                return instance;
            }
        }

        protected override void Initialize()
        {
            menu = new MainMenu(this);
            menu.Initialize();
            LoadContent();

        }

        /// LoadContent will be called once per game and is the place to load all of your content.
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        /// UnloadContent will be called once per game and is the place to unload game-specific content.

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    menu.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);



            spriteBatch.Begin();

            switch(currentGameState)
            {
                case GameState.MainMenu:
                    menu.Draw(gameTime);
                    break;
            }
        
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}

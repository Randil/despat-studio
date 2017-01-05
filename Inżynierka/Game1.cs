using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Xml;


namespace DespatShooter
{
    //This class contains main game loop and handles user input. Main menu and mission class are initialized here.

    //Pattern - Game Loop, see more at http://gameprogrammingpatterns.com/game-loop.html

    public class Game1 : Game
    {
        private static Game1 instance;

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;


        public XmlDocument buttonTexturesXML = new XmlDocument();
        public TextureSheet buttonTextures;


        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;

        public enum GameState
        {
            MainMenu, Mission, Achievements, MissionChoice, Tutorial
        }
        public GameState currentGameState = GameState.MainMenu;

        MenuMain menu;
        MenuMissions missionsMenu;
        public MissionParser missionParser;
        public MissionScreen activeMission;

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
            buttonTexturesXML.Load("..\\..\\..\\..\\greySheet.xml");
            buttonTextures = new TextureSheet(buttonTexturesXML);
            menu = new MenuMain(this);
            menu.Initialize();

            missionsMenu = new MenuMissions(this);
            missionsMenu.Initialize();

            missionParser = new MissionParser(this);
            activeMission = new MissionScreen(this);
            

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
                    {
                        menu.Update(gameTime);
                        break;
                    }
                case GameState.MissionChoice:
                    {
                        missionsMenu.Update(gameTime);
                        break;
                    }
                case GameState.Mission:
                    {
                        activeMission.Update(gameTime);
                        break;
                    }
                default : Exit(); break;
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
                    {
                        menu.Draw(gameTime);
                        break;
                    }
                case GameState.MissionChoice:
                    {
                        missionsMenu.Draw(gameTime);
                        break;
                    }
                case GameState.Mission:
                    {
                        activeMission.Draw(gameTime);
                        break;
                    }
            }
        
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}

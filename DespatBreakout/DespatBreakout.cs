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

    public class DespatBreakout : Game
    {
        private static DespatBreakout instance;

        GraphicsDeviceManager graphics;
        MenuMain menu;
        MenuMissions missionsMenu;

        public SpriteBatch spriteBatch;

        public XmlDocument buttonTexturesXML = new XmlDocument();
        public TextureSheet buttonTextures;
        public XmlDocument gameTexturesXML = new XmlDocument();
        public TextureSheet gameTextures;

        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;

        public GameTime currentGameTime;
        public MissionParser missionParser;
        public MissionScreen activeMission;
        public MissionSave missionSave; 
        public XmlDocument missionsXML = new XmlDocument();
        public AchievementsManager achievements;
        public XmlDocument achievementsXML = new XmlDocument();
        public Random rand = new Random();
        public GameState currentGameState = GameState.MainMenu;

        private DespatBreakout()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";
        }

        public enum GameState
        {
            MainMenu, Mission, Achievements, MissionChoice, Tutorial, Exit
        }


        public static DespatBreakout Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DespatBreakout();
                }
                return instance;
            }
        }

        protected override void Initialize()
        {
            missionParser = new MissionParser(this);
            activeMission = new MissionScreen(this);
            missionSave = new MissionSave(this);

            buttonTexturesXML.Load("..\\..\\..\\..\\Content\\Graphics\\greySheet.xml");
            buttonTextures = new TextureSheet(buttonTexturesXML);
            gameTexturesXML.Load("..\\..\\..\\..\\Content\\Graphics\\assetsSheet.xml");
            gameTextures = new TextureSheet(gameTexturesXML);
            menu = new MenuMain(this);
            menu.Initialize();

            missionsXML.Load("..\\..\\..\\..\\Content\\Levels\\missions.xml");
            missionsMenu = new MenuMissions(this);
            missionsMenu.Initialize(missionsXML);

            achievementsXML.Load("..\\..\\..\\..\\Content\\achievements.xml");
            achievements = new AchievementsManager(achievementsXML);

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
            currentGameTime = gameTime;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (currentGameState == GameState.Mission)
                {
                    missionSave.SaveMissionState(activeMission);
                    missionsMenu = new MenuMissions(this);
                    missionsMenu.Initialize(missionsXML);
                }

                currentGameState = GameState.MainMenu;
            }

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
            GraphicsDevice.Clear(Color.CornflowerBlue);

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

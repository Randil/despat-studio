/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class contains main game loop. 
 * It was written according to instructions from monogame documentation - see more at http://www.monogame.net/.
 * Class handles loading and unloading content, initializes all the menus, keeps instances of most of the game components.
 * 
 * This is a singleton, there can be only one DespatBreakout class in program. This also means that every class can gain
 * access to its public members, calling DespatBreakout.Instance.
 * This is a state machine, which means its Draw and Update methods behave differently whether GameState is surrently set
 * as MainMenu, Mission, MissionChoice etc.
 * 
 * 
 * Design patterns: GameLoop, UpdateMethod, Singleton, State
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Xml;


namespace DespatBreakout
{
    public class DespatBreakout : Game
    {
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

        private static DespatBreakout instance;

        GraphicsDeviceManager graphics;
        MenuMain menu;
        MenuMissions missionsMenu;

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
            LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            missionParser = new MissionParser(this);
            activeMission = new MissionScreen(this);
            missionSave = new MissionSave(this);

            
            buttonTextures = new TextureSheet(buttonTexturesXML);
            gameTextures = new TextureSheet(gameTexturesXML);
            achievements = new AchievementsManager(achievementsXML);

            menu = new MenuMain(this);
            menu.Initialize();
            missionsMenu = new MenuMissions(this);
            missionsMenu.Initialize(missionsXML);
        }

        /// LoadContent will be called once per game and is the place to load all of your content.
        protected override void LoadContent()
        {
            buttonTexturesXML.Load("..\\..\\..\\..\\Content\\Graphics\\greySheet.xml");
            gameTexturesXML.Load("..\\..\\..\\..\\Content\\Graphics\\assetsSheet.xml");
            missionsXML.Load("..\\..\\..\\..\\Content\\Levels\\missions.xml");
            achievementsXML.Load("..\\..\\..\\..\\Content\\achievements.xml");
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

            //At ay given time pressing esc will result in returning to main menu
            //If game was on run while doing this, its state will be saved
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (currentGameState == GameState.Mission && activeMission.finished == false)
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
            GraphicsDevice.Clear(Color.MidnightBlue);

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

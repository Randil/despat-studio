using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;


namespace DespatShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        MouseState currentMouseState;
        MouseState previousMouseState;
        float playerMoveSpeed;

        // Enemies

        Texture2D enemyTexture;

        List<Enemy> enemies;

        // The rate at which the enemies appear

        TimeSpan enemySpawnTime;

        TimeSpan previousSpawnTime;

        // A random number generator

        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            playerMoveSpeed = 4.0f;
            // Initialize the enemies list

            enemies = new List<Enemy>();

            // Set the time keepers to zero

            previousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns

            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Initialize our random number generator

            random = new Random();


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width/2, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height - 50);
            player.Initialize(Content.Load<Texture2D>("Graphics/player"), playerPosition);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            enemyTexture = Content.Load<Texture2D>("Graphics/enemy");


            // TODO: use this.Content to load your game content here
        }

        private void AddEnemy()
        {

            // Randomly generate the position of the enemy

            Vector2 position = new Vector2(random.Next(100, GraphicsDevice.Viewport.Width - 100), enemyTexture.Height / 2);

            // Create an enemy

            Enemy enemy = new Enemy();

            // Initialize the enemy

            enemy.Initialize(enemyTexture, position);

            // Add the enemy to the active enemies list

            enemies.Add(enemy);

        }

        private void UpdateEnemies(GameTime gameTime)
    {

        if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
        {
        previousSpawnTime = gameTime.TotalGameTime;
        AddEnemy();
        }


        for (int i = enemies.Count - 1; i >= 0; i--)
        {

                enemies[i].Update(gameTime);

                if (enemies[i].Active == false)

                {
                 enemies.RemoveAt(i);
                }

        }

    }

        private void UpdateCollision()

{

    // Use the Rectangle’s built-in intersect function to

    // determine if two objects are overlapping

Rectangle rectangle1;

Rectangle rectangle2;

    // Only create the rectangle once for the player

rectangle1 = new Rectangle((int)player.Position.X,

(int)player.Position.Y,

player.Width,

player.Height);

    // Do the collision between the player and the enemies

for (int i = 0; i <enemies.Count; i++)

{

rectangle2 = new Rectangle((int)enemies[i].Position.X,

(int)enemies[i].Position.Y,

enemies[i].Width,

enemies[i].Height);

    // Determine if the two objects collided with each

    // other

if (rectangle1.Intersects(rectangle2))

{

     // Subtract the health from the player based on

     // the enemy damage

     player.Health -= enemies[i].Damage;

        // Since the enemy collided with the player

        // destroy it

     enemies[i].Health = 0;

     // If the player health is less than zero we died

if (player.Health <= 0)

    player.Active = false;

}

}

}

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            UpdatePlayer(gameTime);
            UpdateEnemies(gameTime);
            UpdateCollision(); 

            base.Update(gameTime);
        }
        private void UpdatePlayer(GameTime gameTime)
        {
            // Use the Keyboard / Dpad

            if (currentKeyboardState.IsKeyDown(Keys.Left) )
            {
                player.Position.X -= playerMoveSpeed * gameTime.ElapsedGameTime.Milliseconds/10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player.Position.X += playerMoveSpeed * gameTime.ElapsedGameTime.Milliseconds/10;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                player.Position.Y -= playerMoveSpeed * gameTime.ElapsedGameTime.Milliseconds/10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                player.Position.Y += playerMoveSpeed * gameTime.ElapsedGameTime.Milliseconds/10;
            }
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
             player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GhostWhite);
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            player.Draw(spriteBatch);
            // Draw the Enemies

            for (int i = 0; i < enemies.Count; i++)
            {

                enemies[i].Draw(spriteBatch);

            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ADGTerri
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables and Properties

        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_WIDTH = 800;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState curKeyboardState;

        Camera m_camera;

        Texture2D bg;

        Player player;

        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
        }

        protected override void Initialize()
        {
            //initialize keyboard state
            curKeyboardState = new KeyboardState();

            //initialize player
            player = new Player();

            //initialize camera
            m_camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Load the player
            player.LoadContent(this.Content);
            
            //Load background
            //TODO: extract background into a level class - med priority
            bg = Content.Load<Texture2D>("bg");

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            curKeyboardState = Keyboard.GetState();

            if (curKeyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            player.Update(gameTime, GraphicsDevice, curKeyboardState);    

            m_camera.Update(gameTime, player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_camera.transform);
            
            //Draw Background
            spriteBatch.Draw(bg, new Vector2(0, -1400), Color.White);
            
            //Draw Player
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

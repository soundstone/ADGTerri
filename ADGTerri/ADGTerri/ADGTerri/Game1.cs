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
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables and Properties

        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_WIDTH = 800;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch spriteBatchHUD;

        static bool exitGame;

        public static SpriteFont fontSmall;
        public static SpriteFont fontLarge;
        public static SpriteFont titleFont;

        public static Texture2D SprSinglePixel;

        Camera m_camera;

        Level[] level = new Level[5];

        public static Texture2D bg;
        public static Texture2D playerTex;

        public Player player;
        Vector2 playerStartPosition;
        Texture2D playerTexture;

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
            exitGame = false;
            IsMouseVisible = true;
            Window.Title = "Thanksgiving Run";            

            //initialize camera
            m_camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchHUD = new SpriteBatch(GraphicsDevice);

            fontLarge = Content.Load<SpriteFont>(@"Fonts\FontLarge");
            fontSmall = Content.Load<SpriteFont>(@"Fonts\FontSmall");
            titleFont = Content.Load<SpriteFont>(@"Fonts\TitleFont");
            SprSinglePixel = Content.Load<Texture2D>(@"Textures\SinglePixel");
            bg = Content.Load<Texture2D>(@"Textures\bg");
            playerTex = Content.Load<Texture2D>(@"Textures\sprite");
            player = new Player(playerStartPosition);

            MenuManager.CreateMenuItems();
            

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape)
                    || exitGame)
                this.Exit();

            InputHelper.UpdateStates();
            GameManager.Update(gameTime);

            player.Update(gameTime);
            m_camera.Update(gameTime, player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_camera.transform);
            spriteBatchHUD.Begin();
            
            GameManager.Draw(spriteBatch, spriteBatchHUD);
            
            if(GameManager.gameState == GameState.Playing)
                player.Draw(spriteBatch);
            
            spriteBatch.End();
            spriteBatchHUD.End();

            base.Draw(gameTime);
        }

        public static void ExitGame()
        {
            exitGame = true;
        }
    }
}

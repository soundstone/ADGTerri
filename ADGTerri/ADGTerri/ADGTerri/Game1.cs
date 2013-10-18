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
        //constants to define Client Screen size
        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_WIDTH = 800;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState curKeyboardState;

        Camera m_camera;

        Texture2D bg;

        Texture2D sprite;
        public Rectangle spriteRect;
        public Vector2 spritePos;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Assign Client screen size
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
        }

        protected override void Initialize()
        {
            curKeyboardState = new KeyboardState();

            m_camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load generic test sprite
            sprite = Content.Load<Texture2D>("sprite");
            spritePos = new Vector2(400, 300);

            bg = Content.Load<Texture2D>("bg");

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            curKeyboardState = Keyboard.GetState();

            if (curKeyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            spriteRect = new Rectangle((int)spritePos.X, (int)spritePos.Y, sprite.Width, sprite.Height);



            //"Gravity"
            spritePos.Y += 5;

            if (curKeyboardState.IsKeyDown(Keys.W))
                spritePos.Y -= 10;
            //if (curKeyboardState.IsKeyDown(Keys.Down))
            //    spritePos.Y += 5;
            if (curKeyboardState.IsKeyDown(Keys.D))
                spritePos.X += 5;
            if (curKeyboardState.IsKeyDown(Keys.A))
                spritePos.X -= 5;

            //Boundery collision
            if (spritePos.X < 0)
                spritePos.X = 0;
            if (spritePos.Y < -1325)
                spritePos.Y = -1325;
            if (spritePos.X + spriteRect.Width > SCREEN_WIDTH)
                spritePos.X = SCREEN_WIDTH - spriteRect.Width;
            if (spritePos.Y + spriteRect.Height > SCREEN_HEIGHT)
                spritePos.Y = SCREEN_HEIGHT - spriteRect.Height;

            m_camera.Update(gameTime, this);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_camera.transform);
            spriteBatch.Draw(bg, new Vector2(0, -1400), Color.White);
            spriteBatch.Draw(sprite, spritePos, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

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

        Texture2D platformSmall;
        Vector2 platformPos;
        Rectangle platformRect;

        Texture2D platformMed;
        Vector2 medPos;
        Rectangle medRect;

        Texture2D platformLarge;
        Vector2 largePos;

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

            medRect = new Rectangle((int)medPos.X, (int)medPos.Y, platformMed.Width, platformMed.Height);
            platformRect = new Rectangle((int)platformPos.X, (int)platformPos.Y, platformSmall.Width, platformSmall.Height);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load small platform sprite
            platformSmall = Content.Load<Texture2D>("platformSmall");
            platformPos = new Vector2(50, SCREEN_HEIGHT - 120);

            platformMed = Content.Load<Texture2D>("platformMedium");
            medPos = new Vector2(200, SCREEN_HEIGHT - 200);

            platformLarge = Content.Load<Texture2D>("platformLarge");
            largePos = new Vector2(400, SCREEN_HEIGHT - 300);

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

            PlatformCollision();
            
            m_camera.Update(gameTime, this);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_camera.transform);
            spriteBatch.Draw(bg, new Vector2(0, SCREEN_HEIGHT-bg.Height), Color.White);
            spriteBatch.Draw(platformSmall, platformPos, Color.White);
            spriteBatch.Draw(platformMed, medPos, Color.White);
            spriteBatch.Draw(platformLarge, largePos, Color.White);
            spriteBatch.Draw(sprite, spritePos, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //simple platform collision
        //Needs to be tweeked for better accuracy
        void PlatformCollision()
        { 
            //Small Platform collision
            if (spritePos.X + 30 >= platformPos.X && spritePos.X + 30 <= platformPos.X + platformSmall.Width)
                 if(spritePos.Y + sprite.Height >= platformPos.Y && spritePos.Y + sprite.Height <= platformPos.Y + platformSmall.Height)
                    spritePos.Y = platformPos.Y - sprite.Height;

            //Medium Platform Collision
            if (spritePos.X + 30 >= medPos.X && spritePos.X + 30 <= medPos.X + platformMed.Width)
                if (spritePos.Y + sprite.Height >= medPos.Y && spritePos.Y + sprite.Height <= medPos.Y + platformMed.Height)
                    spritePos.Y = medPos.Y - sprite.Height;

            //Large Platform Collision
            if (spritePos.X + 30 >= largePos.X && spritePos.X + 30 <= largePos.X + platformLarge.Width)
                if (spritePos.Y + sprite.Height >= largePos.Y && spritePos.Y + sprite.Height <= largePos.Y + platformLarge.Height)
                    spritePos.Y = largePos.Y - sprite.Height;
        }

    }
}

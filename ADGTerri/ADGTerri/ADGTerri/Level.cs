using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ADGTerri
{
    class Level
    {
        struct BackgroundItem
        {
            Texture2D texture;
            Vector2 position;
            float speed;
            float layerDepth;

            public BackgroundItem(Texture2D tex, Vector2 pos, float spd, float layerDepth)
            {
                this.texture = tex;
                this.position = pos;
                this.speed = spd;
                this.layerDepth = layerDepth;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(this.texture, this.position, null, Color.White, 0f,
                    Vector2.Zero, 1f, SpriteEffects.None, this.layerDepth);
            }
        }

        List<BackgroundItem> backgrounds;
        public List<Actor> Actors;
        public Rectangle PlayBounds;
        //GraphicsDevice graphicsDevice;
        //Camera m_camera;
        Camera2 m_camera;
        public Player player;
        Vector2 playerStartPosition;
        Texture2D backgroundTexture;
        Texture2D playerTexture;


        public Level(Texture2D bgTex)
        {
            backgrounds = new List<BackgroundItem>();
            Actors = new List<Actor>();
            Initialize();
            this.backgroundTexture = bgTex;
        }
        //public Level(Vector2 startPos, Texture2D bgTexture, Texture2D playerTex)
        //{
        //    backgrounds = new List<BackgroundItem>();
        //    this.playerTexture = playerTex;
        //    Actors = new List<Actor>();
        //    this.playerStartPosition = startPos;
        //    this.backgroundTexture = bgTexture;
        //    Initialize();
            
        //}

        public void Initialize()
        {
            
            //m_camera = new Camera(gd.Viewport);
            player = new Player(playerStartPosition, playerTexture);
        }
        
        public void Update(GameTime gameTime)
        {
            KeyboardState curKeyboardState = Keyboard.GetState();

            //update actors
            for (int i = 0; i < Actors.Count; i++)
            {
                Actors[i].Update(gameTime);
            }

            //update player
            player.Update(gameTime); 
            //update camera position
            UpdateCameraPosition();
            
        }

        public void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
        {
            //spriteBatch.Draw(Game1.SprSinglePixel, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.DarkRed);
           // spriteBatchHUD.DrawString(Game1.fontSmall, "Playing Game!", new Vector2(20, 150), Color.White);
            
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_camera.transform);

            //draw Back-background
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);

            //draw backgrounds
            for (int i = 0; i < backgrounds.Count; i++)
            {
                backgrounds[i].Draw(spriteBatch);
            }

            //draw actors
            for (int i = 0; i < Actors.Count; i++)
            {
                Actors[i].Draw(spriteBatch);
            }
            
            player.Draw(spriteBatch);

            //spriteBatch.End();
        }

        #region Helper functions

        public void AddBackgroundItem(Texture2D texture, Vector2 position,
            float speed, float layerDepth)
        {
            backgrounds.Add(new BackgroundItem(texture, position, speed, layerDepth));
        }

        private void UpdateCameraPosition()
        {
            Vector2 center = new Vector2(0, player.playerPos.Y + (player.Height / 2) - 250);

            if (center.Y > Game1.SCREEN_HEIGHT - 600)
                center.Y = Game1.SCREEN_HEIGHT - 600;
            if (center.Y < -1350)
                center.Y = -1350;

            //Camera2.Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
        #endregion

    }
}

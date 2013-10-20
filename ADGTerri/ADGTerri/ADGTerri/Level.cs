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
        public Player player;
        Texture2D backgroundTexture;
        List<Platform> platforms;


        public List<Platform> Platforms
        {
            get { return platforms; }
        }
        public Level(Texture2D bgTex, Player p)
        {
            backgrounds = new List<BackgroundItem>();
            platforms = new List<Platform>();
            Actors = new List<Actor>();
            this.backgroundTexture = bgTex;
            player = p;            
        }
        
        public void Update(GameTime gameTime)
        {
            KeyboardState curKeyboardState = Keyboard.GetState();

            //update actors
            for (int i = 0; i < Actors.Count; i++)
            {
                Actors[i].Update(gameTime);
            }

            foreach (Platform platform in platforms)
            {
                //check collision and move if needed.
                //if (player.playerPos.X + 30 >= platform.Position.X &&
                //    player.playerPos.X + 30 <= platform.Position.X + platform.Texture.Width)
                //{
                //    if (player.playerPos.Y + player.Height >= platform.Position.Y &&
                //        player.playerPos.Y + player.Height <= platform.Position.Y + platform.Texture.Height)
                //    {
                //        player.playerPos.Y = platform.Position.Y - player.Height;
                //    }
                //}

                if (platform.MOVE)
                    platform.MovePlatform();
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
        {
            //draw Back-background
            spriteBatch.Draw(backgroundTexture, new Vector2(0, Game1.SCREEN_HEIGHT - backgroundTexture.Height), Color.White);

            //draw backgrounds
            for (int i = 0; i < backgrounds.Count; i++)
            {
                backgrounds[i].Draw(spriteBatch);
            }

            //draw platforms
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }

            //draw actors
            for (int i = 0; i < Actors.Count; i++)
            {
              //  Actors[i].Draw(spriteBatch);
            }
        }

        #region Helper functions

        public void AddBackgroundItem(Texture2D texture, Vector2 position,
            float speed, float layerDepth)
        {
            backgrounds.Add(new BackgroundItem(texture, position, speed, layerDepth));
        }

        public void AddPlatform(Texture2D tex, Vector2 pos, bool move, float spd)
        {
            platforms.Add(new Platform(tex, pos, move, spd));
        }

        #endregion

    }
}

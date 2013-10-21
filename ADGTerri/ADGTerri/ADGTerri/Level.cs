﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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
        List<Obstacle> obstacles;
        Song levelSong;
        int checkIntro = 0;


        public List<Platform> Platforms
        {
            get { return platforms; }
        }

        public List<Obstacle> Obstacles
        {
            get { return obstacles; }
        }

        public Level(Texture2D bgTex, Player p, Song song)
        {
            backgrounds = new List<BackgroundItem>();
            platforms = new List<Platform>();
            obstacles = new List<Obstacle>();
            Actors = new List<Actor>();
            this.backgroundTexture = bgTex;
            player = p;
            levelSong = song;   
        }
        
        public void Update(GameTime gameTime)
        {
            KeyboardState curKeyboardState = Keyboard.GetState();

            //update actors
            for (int i = 0; i < Actors.Count; i++)
            {
                Actors[i].Update(gameTime);
            }

            //foreach (Platform platform in platforms)
            //{
            //        if (platform.MOVE)
            //        platform.MovePlatform();
            //}
        }

        public void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
        {
            PlayIntro();

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

            //draw obstacles
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Draw(spriteBatch);
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

        public void AddObstacle(Texture2D tex, Vector2 pos, bool move, float spd)
        {
            obstacles.Add(new Obstacle(tex, pos, move, spd));
        }

        private void PlayIntro()
        {
            if (checkIntro >= 1)
            {
            }
            else 
                MediaPlayer.Play(levelSong);
            checkIntro = 1;
        }
        #endregion

    }
}

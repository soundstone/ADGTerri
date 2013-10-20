using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace ADGTerri
{
    enum GameState
    {
        Intro,
        MainMenu,
        HowToPlay,
        Playing
    }

    static class GameManager
    {
        public static GameState gameState = GameState.MainMenu;
        public static List<Level> Levels = new List<Level>();
        private static int currentLevel = 0;
        private static Player gplayer;
        private static Vector2 respawnLocation;
        private static ContentManager Content;
        private static GraphicsDeviceManager graphics;
        

        //logo Display variables
        private static Texture2D logoTexture;
        private static Vector2 logoCenter;
        private static int alphaValue = 1;
        private static int fadeIncrement = 3;
        private static double fadeDelay = .020;
        private static int delayer = 1;
        private static int checkIntro = 0;
        private static double frameTime;

        //level audio
        static Song levelSong;

        public static int CurrentLevel
        {
            get { return currentLevel; }
        }

        public static Vector2 RespawnLocation
        {
            get { return respawnLocation; }
            set { respawnLocation = value; }
        }

        public static void Initialize(ContentManager content, Player gamePlayer, GraphicsDeviceManager graph)
        {
            Content = content;
            gplayer = gamePlayer;
            graphics = graph;
            logoTexture = Content.Load<Texture2D>(@"Textures\RedTeam");
            logoCenter = new Vector2(logoTexture.Width / 2, logoTexture.Height / 2);
            levelSong = Content.Load<Song>(@"Music\Kalimba");
        }

        public static void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Intro:
                    {
                        //fade in logo
                        fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                        if (fadeDelay <= 0)
                        {
                            //reset fade delay
                            fadeDelay = .045;

                            //increment/decrement the fade value of logo
                            alphaValue += fadeIncrement;

                            if (alphaValue <= 0)
                                fadeIncrement *= -1;
                        }

                        if (delayer <= 400)
                            delayer++;
                        else
                            delayer = 401;

                        frameTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

                        break;
                    }
                case GameState.MainMenu:
                    MenuManager.Update();
                    break;

                case GameState.HowToPlay:
                    //await input to go back to main menu
                    if (InputHelper.WasButtonPressed(Buttons.A)
                        || InputHelper.WasKeyPressed(Keys.Space)
                        || InputHelper.WasKeyPressed(Keys.Enter))
                    {
                        gameState = GameState.MainMenu;
                    }
                    break;

                case GameState.Playing:
                    {
                        //update level
                        Levels[currentLevel].Update(gameTime);

                        foreach (Platform platform in Levels[currentLevel].Platforms)
                        {
                            //check collision and move if needed.
                            if (gplayer.playerPos.X + 30 >= platform.Position.X &&
                                gplayer.playerPos.X + 30 <= platform.Position.X + platform.Texture.Width)
                            {
                                if (gplayer.playerPos.Y + gplayer.Height >= platform.Position.Y &&
                                    gplayer.playerPos.Y + gplayer.Height <= platform.Position.Y + platform.Texture.Height)
                                {
                                    gplayer.playerPos.Y = platform.Position.Y - gplayer.Height;
                                    gplayer.startY = gplayer.playerPos.Y;
                                }
                                else
                                {
                                    gplayer.startY = 550;
                                }
                            }
                            
                        }
                            break;
                    }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
        {
            switch (gameState)
            {
                case GameState.Intro:
                    {
                        if (delayer >= 400)
                        {
                            gameState = GameState.MainMenu;
                        }
                        else
                        {
                            graphics.GraphicsDevice.Clear(Color.White);
                            DrawLogo(spriteBatchHUD);
                        }
                        break;
                    }

                case GameState.MainMenu:
                    MenuManager.Draw(spriteBatchHUD);
                    break;

                case GameState.HowToPlay:
                    spriteBatchHUD.Draw(Game1.SprSinglePixel, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.DarkGreen);
                    spriteBatchHUD.DrawString(Game1.fontLarge, "HTP Screen", new Vector2(160, 100), Color.White);

                    break;

                case GameState.Playing:
                    CreateLevel();
                    Levels[currentLevel].Draw(spriteBatch, spriteBatchHUD);
                    break;
            }
        }

        public static void CreateLevel()
        {
            Level level = new Level(Game1.bg, gplayer, levelSong);

            Levels.Add(level);
            level.Actors.Add(new Player(new Vector2(0, 550), graphics));

            level.player = level.Actors[level.Actors.Count - 1] as Player;

            level.AddPlatform(Game1.platformSmallTex, new Vector2(50, Game1.SCREEN_HEIGHT - 120),
                false, 0f);
            level.AddPlatform(Game1.platformMedTex, new Vector2(200, Game1.SCREEN_HEIGHT - 200),
                false, 0f);
            level.AddPlatform(Game1.platformLargeTex, new Vector2(400, Game1.SCREEN_HEIGHT - 300),
                false, 0f);
                
        }

        private static void DrawLogo(SpriteBatch spriteBatchHUD)
        {
            spriteBatchHUD.Draw(logoTexture, new Rectangle(0, 0, logoTexture.Width, logoTexture.Height),
                new Color(255, 255, 255, (byte)MathHelper.Clamp(alphaValue, 0, 255)));
        }
    }
}

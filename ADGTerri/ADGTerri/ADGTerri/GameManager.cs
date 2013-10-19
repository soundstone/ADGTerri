using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace ADGTerri
{
    enum GameState
    {
        MainMenu,
        HowToPlay,
        Playing
    }

    static class GameManager
    {
        public static GameState gameState = GameState.MainMenu;
        public static List<Level> Levels = new List<Level>();
        public static int CurrentLevel = 0;

        public static void Update(GameTime gameTime)
        {
            switch (gameState)
            {
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
                    //update level
                    Levels[CurrentLevel].Update(gameTime);
                    break;
            }
        }

        public static void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    MenuManager.Draw(spriteBatchHUD);
                    break;

                case GameState.HowToPlay:
                    spriteBatchHUD.Draw(Game1.SprSinglePixel, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.DarkGreen);
                    spriteBatchHUD.DrawString(Game1.fontLarge, "HTP Screen", new Vector2(179, 540), Color.White);

                    break;

                case GameState.Playing:
                    CreateLevel();
                    Levels[CurrentLevel].Draw(spriteBatch, spriteBatchHUD);
                    break;
            }
        }

        public static void CreateLevel()
        {
            Level level = new Level(Game1.bg);

            Levels.Add(level);
            level.Actors.Add(new Player(new Vector2(400, 300)));

            level.player = level.Actors[level.Actors.Count - 1] as Player;
        }
    }
}

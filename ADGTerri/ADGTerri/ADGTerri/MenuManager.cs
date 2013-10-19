using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ADGTerri
{
    enum MenuState
    {
        TitleScreen,
        MainMenu
    }


    struct MenuItem
    {
        string text;
        Vector2 position;

        public MenuItem(string text, Vector2 pos)
        {
            this.text = text;
            this.position = pos;
        }

        public void Draw(SpriteBatch spriteBatchHUD, Color drawColor)
        {
            spriteBatchHUD.DrawString(Game1.fontSmall, text, position, drawColor);
        }
    }

    static class MenuManager
    {
        static Color ColorStandard = Color.White;
        static Color ColorSelected = Color.Khaki;

        static List<MenuItem> menuItems = new List<MenuItem>();

        static int currentMenuItem = 0;
        public static MenuState MenuState = MenuState.TitleScreen;

        public static void Update()
        {
            switch (MenuState)
            {
                case MenuState.TitleScreen:
                    //Await input to move to the main menu
                    if (InputHelper.WasButtonPressed(Buttons.A)
                        || InputHelper.WasButtonPressed(Buttons.Start)
                        || InputHelper.WasKeyPressed(Keys.Enter)
                        || InputHelper.WasKeyPressed(Keys.Space))
                    {
                        MenuState = MenuState.MainMenu;
                        
                    }
                    break;

                case MenuState.MainMenu:
                    //menu navigation
                    if (InputHelper.WasButtonPressed(Buttons.DPadUp)
                        || (InputHelper.NGS.ThumbSticks.Left.Y < 0.3 &&
                            InputHelper.OGS.ThumbSticks.Left.Y > 0.3)
                        || InputHelper.WasKeyPressed(Keys.Up))
                    {
                        currentMenuItem--;
                        if (currentMenuItem < 0)
                            currentMenuItem = menuItems.Count - 1;
                    }
                    if (InputHelper.WasButtonPressed(Buttons.DPadDown)
                        || (InputHelper.NGS.ThumbSticks.Left.Y < -0.3 &&
                            InputHelper.OGS.ThumbSticks.Left.Y > -0.3)
                        || InputHelper.WasKeyPressed(Keys.Down))
                    {
                        currentMenuItem++;
                        if (currentMenuItem >= menuItems.Count)
                            currentMenuItem = 0;
                    }

                    //menu item actions
                    if (InputHelper.WasButtonPressed(Buttons.A)
                        || InputHelper.WasKeyPressed(Keys.Enter)
                        || InputHelper.WasKeyPressed(Keys.Space))
                    {
                        switch (currentMenuItem)
                        {
                            case 0:
                                //Begin Game
                                GameManager.gameState = GameState.Playing;
                                break;

                            case 1:
                                //How To Play
                                GameManager.gameState = GameState.HowToPlay;
                                break;

                            case 2:
                                //Exit Game
                                Game1.ExitGame();
                                break;
                        }
                    }
                    break;

                   
            }
        }

        public static void Draw(SpriteBatch spriteBatchHUD)
        {
            switch (MenuState)
            {
                case MenuState.TitleScreen:
                    spriteBatchHUD.DrawString(Game1.fontLarge, "Thanksgiving Run", new Vector2(187, 100), Color.White);
                    spriteBatchHUD.DrawString(Game1.fontLarge, "Press Start", new Vector2(200, 400), Color.White);
                    break;

                case MenuState.MainMenu:
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        if (i == currentMenuItem)
                            menuItems[i].Draw(spriteBatchHUD, ColorSelected);
                        else
                            menuItems[i].Draw(spriteBatchHUD, ColorStandard);
                    }
                    break;
            }
        }

        public static void CreateMenuItems()
        {
            menuItems.Add(new MenuItem("Begin Game", new Vector2(45, 50)));
            menuItems.Add(new MenuItem("How To Play", new Vector2(45, 120)));
            menuItems.Add(new MenuItem("Exit Game", new Vector2(45, 190)));
        }
    }
}

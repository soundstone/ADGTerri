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
    public class Player
    {
        #region Variables

        const int SCREEN_WIDTH = 800;
        const int SCREEN_HEIGHT = 600;
        const int floorLevel = -1325;
        
        Texture2D playerTexture;
        public Rectangle playerRect;
        public Vector2 playerPos;

        #endregion

        #region Ctor

        public Player()
        {
        }

        #endregion

        #region XNA Methods

        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("sprite");
            playerPos = new Vector2(400, 300);
        }

        public Vector2 PlayerPos
        {
            get { return playerPos; }
        }

        public int Width
        {
            get { return playerTexture.Width; }
        }

        public int Height
        {
            get { return playerTexture.Height; }
        }

        public void Update(GameTime gameTime, GraphicsDevice device, KeyboardState keystate)
        {
            //Players collision
            #region Collision

            //player collision box
            playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, Width, Height);

            if (playerPos.X < 0)
                playerPos.X = 0;
            if (playerPos.Y < floorLevel) 
                playerPos.Y  = floorLevel;
            if (playerPos.X + Width > SCREEN_WIDTH)
                playerPos.X = SCREEN_WIDTH - Width;
            if (playerPos.Y + Height > SCREEN_HEIGHT)
                playerPos.Y = SCREEN_HEIGHT - Height;
            #endregion

            //gravity on player 
            #region Gravity
            //TODO: apply physics here - high priority
            playerPos.Y += 5;
            
            #endregion 

            //Players input controls
            #region Input
            
            //TODO: fix jump here - high priority
            if (keystate.IsKeyDown(Keys.W))
                playerPos.Y -= 10;
            if (keystate.IsKeyDown(Keys.D))
                playerPos.X += 5;
            if (keystate.IsKeyDown(Keys.A))
                playerPos.X -= 5;

            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, playerPos, Color.White);
        }

        #endregion
    }
}

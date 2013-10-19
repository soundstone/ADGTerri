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
    public enum PlayerState
    {
        Idle,
        Walking,
        Jumping
    };

    public class Player : Actor
    {
        #region Variables

        const int SCREEN_WIDTH = 800;
        const int SCREEN_HEIGHT = 600;
        const int floorLevel = -1325;

        public PlayerState state;

        Texture2D playerTexture;
        public Rectangle playerRect;
        public Vector2 playerPos;
        float speed;
        float layerDepth;

        #region Physics Variables

        //initial gravity velocity
        Vector2 GRAVITY = new Vector2(0f, 9.80f);
        Vector2 velocity;

        #endregion


        //movement
        Vector2 jumpingPosition;
        float landingHeight; //need to know where landing of jump is
        #endregion

        #region Ctor

        public Player(Vector2 position, Texture2D playerTex)
            :base(position)
        {
            this.playerTexture = playerTex;
            this.playerPos = position;
        }

        #endregion

        #region XNA Methods

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

        public override void Update(GameTime gameTime)
        {
            
            //move in desired direction
            if (InputHelper.NGS.ThumbSticks.Left.X > 0.3
                || InputHelper.WasKeyPressed(Keys.D))
                this.playerPos.X += 5;
            if (InputHelper.NGS.ThumbSticks.Left.X < -0.3
                || InputHelper.WasKeyPressed(Keys.A))
                this.playerPos.X -= 5;
            if (InputHelper.WasButtonPressed(Buttons.A) 
                || InputHelper.WasKeyPressed(Keys.W))
                this.playerPos.Y += velocity.Y;
            if (InputHelper.IsKeyHeld(Keys.A))
                this.playerPos.X -= 5;
            if (InputHelper.IsKeyHeld(Keys.D))
                this.playerPos.X += 5;
            if (InputHelper.IsKeyHeld(Keys.W))
                this.playerPos.Y -= 10;

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

            ////gravity on player 
            //#region Gravity
            ////TODO: apply physics here - high priority
            playerPos.Y += 5;
            ////delta time 1.0f / 1 second
            //float dT = (float)gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            //velocity += GRAVITY * dT;
            //playerPos += velocity * dT;
            
            //#endregion 

            //Movement

            KeyboardState keystate = Keyboard.GetState();
            switch (state)
            {
                case PlayerState.Idle:
                case PlayerState.Walking:
                    if (keystate.IsKeyDown(Keys.A))
                    {
                        landingHeight = playerPos.Y;
                        jumpingPosition = playerPos;
                        
                    }
                    break;
                case PlayerState.Jumping:
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, playerPos, Color.White);
            base.Draw(spriteBatch);
        }

        #endregion


    }
}

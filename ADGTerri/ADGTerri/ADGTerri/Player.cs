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
        public float startY;
        float speed;
        float layerDepth;
        public bool jumping;
        float jumpSpeed = 0;
        public static GraphicsDeviceManager gDevice;

        #region Physics Variables

        //initial gravity velocity
        //Vector2 GRAVITY = new Vector2(0f, 9.80f);
        private Vector2 velocity;
        const float gravity = 100f;
        float moveSpeed = 500f;
        //float jumpSpeed = 2000f;

        #endregion


        //movement
        //Vector2 jumpingPosition;
        //float landingHeight; //need to know where landing of jump is
        #endregion

        #region Ctor

        public Player(Vector2 position, GraphicsDeviceManager graphics)
            :base(position)
        {
            this.playerTexture = Game1.playerTex;
            this.playerPos = position;
            velocity = new Vector2(0, 0);
            gDevice = graphics;
            this.startY = playerPos.Y;
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

            playerPos += velocity;

            if (InputHelper.IsKeyHeld(Keys.A))
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputHelper.IsKeyHeld(Keys.D))
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                    velocity.X = 0;
            if(jumping)
            {
                playerPos.Y += jumpSpeed;
                jumpSpeed += 1;
                    if(playerPos.Y >= startY)
                    {
                        playerPos.Y = startY;
                        jumping = false;
                    }
            }
            else if( (InputHelper.WasKeyPressed(Keys.W)))
            {
                jumping = true;
                jumpSpeed = -20;
            }
  
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
           // playerPos.Y += 5;
            ////delta time 1.0f / 1 second
            //float dT = (float)gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            //velocity += GRAVITY * dT;
            //playerPos += velocity * dT;
            
            //#endregion 

            //Movement

            //KeyboardState keystate = Keyboard.GetState();
            //switch (state)
            //{
            //    case PlayerState.Idle:
            //    case PlayerState.Walking:
            //        if (keystate.IsKeyDown(Keys.A))
            //        {
            //            landingHeight = playerPos.Y;
            //            jumpingPosition = playerPos;
                        
            //        }
            //        break;
            //    case PlayerState.Jumping:
            //        break;
            //}

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

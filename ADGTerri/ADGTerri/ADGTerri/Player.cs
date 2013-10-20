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
        bool rolling = false;
        float rollSpeed = 25.0f;
        double rollTime, rollTimer = 0;

        bool bash = false;

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
            #region Input
            #region Moving left / right and Jump

            if (InputHelper.IsKeyHeld(Keys.A))
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputHelper.IsKeyHeld(Keys.D))
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                    velocity.X = 0;
            
            if (jumping)
            {
                playerPos.Y += jumpSpeed;
                jumpSpeed += 1;
                if (playerPos.Y >= startY)
                {
                    playerPos.Y = startY;
                    jumping = false;
                }
            }
            else if ((InputHelper.WasKeyPressed(Keys.W)))
            {
                jumping = true;
                jumpSpeed = -20;
            }

            //resolves sprite not falling when moving off platform edge.
            //feel free to adjust the "gravity" as you feel necessary
            //simulates a persistant gravity effect, always pulling when not jumping
            if (!jumping)
            {
                playerPos.Y += 9.8f;
            }

            
            #endregion
            #region Peck and Roll
            if (InputHelper.WasKeyPressed(Keys.S) && rolling == false)
            {
                rolling = true;
                rollTime = gameTime.ElapsedGameTime.TotalSeconds * 100;

                if (velocity.X > 0)
                    velocity.X += rollSpeed;
                else if (velocity.X < 0)
                    velocity.X -= rollSpeed;
                else
                    velocity.X = 0;
            }
            rollTimer += rollTime;

            if (rollTimer >= 4)
            {
                rolling = false;
                rollTime = 0;
                rollTimer = 0;
            }

            //Bash/Peck
            if (InputHelper.WasKeyPressed(Keys.Space) && bash == false)
            {
                bash = true;
            }
            else
                bash = false;
            #endregion
            #endregion
            //Players collision w/ level bounds
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
            if (rolling)
                spriteBatch.DrawString(Game1.fontSmall, "Rolling!", new Vector2(20, 40), Color.Blue);

            if (bash)
                spriteBatch.DrawString(Game1.fontSmall, "Peck!", new Vector2(20, 15), Color.Black);
            base.Draw(spriteBatch);
        }

        #endregion


    }
}

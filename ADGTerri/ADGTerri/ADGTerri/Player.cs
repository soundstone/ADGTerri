using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

//for stopwatch function
using System.Diagnostics;
using System.Threading;

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

        string elapsedTime;

        public int score;

        Texture2D playerTexture;
        public Rectangle playerRect;
        public Vector2 playerPos;
        public float startY;
        //float speed;
        private bool dead = false;
        
        float layerDepth;
        public bool jumping;
        float jumpSpeed = 0;
        public bool falling = false;
        public static GraphicsDeviceManager gDevice;
        public bool rolling = false;
        public int facing = 1; //positive 1 = facing right, negetive 1 = facing left
        float rollSpeed = 25.0f;
        double rollTime, rollTimer = 0;

        public bool bash = false;
        float bashSpeed = 0.2f;
        double bashTime, bashTimer = 0;
        
        #region Physics Variables

        float elapsed; //keep track of gameTime cycles        
        private Vector2 velocity;
        Vector2 drift;
        const float gravity = 100f;
        float moveSpeed = 500f;
        Vector2 momentum;
        Vector2 force;
        float mass = 500f;
        private Vector2 fallSpeed = new Vector2(0, 20);

        Vector2 resistance = new Vector2(2, 0);

        #endregion


        public bool Dead
        {
            get { return dead; }
        }

        #endregion

        #region Ctor

        public Player(Vector2 position, GraphicsDeviceManager graphics, ContentManager content)
            :base(position)
        {
            this.playerTexture = Game1.playerTex;
            this.playerPos = position;
            velocity = new Vector2(0, 0);
            gDevice = graphics;
            this.startY = playerPos.Y;

            animations.Add("idle",
                new AnimationStrip(
                    content.Load<Texture2D>(@"Textures\thanksbird2"),
                    27,
                    "idle"));
            animations["idle"].LoopAnimation = true;
            animations.Add("run",
                new AnimationStrip(
                    content.Load<Texture2D>(@"Textures\thanksbird2"),
                    27,
                    "run"));
            animations["run"].LoopAnimation = true;
            frameWidth = 26;
            frameHeight = 28;
            CollisionRectangle = new Rectangle(9, 1, 38, 46);
            layerDepth = 0.825f;
            enabled = true;
            PlayAnimation("idle");
                    
        }

        #endregion

        #region XNA Methods

        public Vector2 PlayerPos
        {
            get { return playerPos; }
        }

        public Vector2 Momentum
        {
            get { return momentum; }

            set { momentum = value; }
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
            if (!Dead)
            {
                string newAnimation = "idle";
                
                elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                force = Vector2.Zero;
                drift = new Vector2((float)Math.Cos(velocity.X) + 4.3f, (float)Math.Sin(velocity.Y));

                playerPos += velocity + new Vector2(elapsed, elapsed);
                #region Input

                #region Moving left / right and Jump

                if (InputHelper.IsKeyHeld(Keys.A))
                {
                    facing = -1;
                    newAnimation = "run";
                    force = (drift * 20f) * -1;
                }
                else if (InputHelper.IsKeyHeld(Keys.D))
                {
                    facing = 1;
                    newAnimation = "run";
                    force = drift * 20f;
                }
                else
                {
                    velocity.X = 0;
                    force = Vector2.Zero;
                    
                    if(facing == 1)
                    {
                        if(momentum.X >= 0)
                            momentum -= resistance * 10;

                        if (momentum.X <= 0)
                            momentum.X = 0;
                    }
                    else if (facing == -1)
                    {
                        if (momentum.X <= 0)
                            momentum += resistance * 10;

                        if (momentum.X >= 0)
                            momentum.X = 0;
                    }
                }

                momentum += force;
                momentum *= 0.99f;

                velocity = momentum / mass;

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

                if (!falling)
                {
                    if (!jumping)
                    {
                        if (InputHelper.WasKeyPressed(Keys.W))
                        {
                            jumping = true;
                            jumpSpeed = -15.8f;
                            newAnimation = "jump";
                        }
                    }
                }
                if (currentAnimation == "jump")
                    newAnimation = "jump";
                if (newAnimation != currentAnimation)
                    PlayAnimation(newAnimation);

                if (!jumping)
                {
                    playerPos.Y += 15.8f;
                }

                #endregion

                #region Peck and Roll
                if (InputHelper.WasKeyPressed(Keys.S) && rolling == false)
                {
                    rolling = true;
                    rollTime = gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

                    if (velocity.X > 0)
                        velocity.X += rollSpeed;
                    else if (velocity.X < 0)
                        velocity.X -= rollSpeed;
                    else
                        velocity.X = 0;
                }
                rollTimer += rollTime;

                if (rollTimer >= 0.3f)
                {
                    rolling = false;
                    rollTime = 0;
                    rollTimer = 0;
                }

                if (InputHelper.WasKeyPressed(Keys.Space) && bash == false)
                {
                    bash = true;
                    bashTime = gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                }

                bashTimer += bashTime;

                if (bashTimer >= 0.3f)
                {
                    bash = false;
                    bashTimer = 0;
                    bashTime = 0;
                }

                #endregion

                #endregion

                //Players collision w/ level bounds
                #region Collision

                //player collision box
                playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, Width, Height);

                if (playerPos.X < 0)
                {
                    playerPos.X = 0;
                    momentum = Vector2.Zero;
                }
                if (playerPos.Y < floorLevel)
                {
                    playerPos.Y = floorLevel;
                }
                if (playerPos.X + Width > SCREEN_WIDTH)
                {
                    playerPos.X = SCREEN_WIDTH - Width;
                    momentum = Vector2.Zero;
                }
                if (playerPos.Y + Height > SCREEN_HEIGHT)
                {
                    playerPos.Y = SCREEN_HEIGHT - Height;
                    startY = playerPos.Y;
                    falling = false;
                }
                #endregion



                #region Old Code

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

                #endregion
            }
            base.Update(gameTime);
        }

        public void DrawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, playerPos, Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            #region **REMOVE BEFORE FINAL DRAFT!!***
            //Display when player activates "Roll" 
            if (rolling)
                spriteBatch.DrawString(Game1.debugFont, "Rolling!", new Vector2(20, 40), Color.Blue);

            //display current facing direction
            if (facing == 1)
                spriteBatch.DrawString(Game1.debugFont, "Facing: Right", new Vector2(20, 70), Color.Black);
            else if (facing == -1)
                spriteBatch.DrawString(Game1.debugFont, "Facing: Left", new Vector2(20, 70), Color.Black);

            //Display when player activates "Peck"
            if (bash)
                spriteBatch.DrawString(Game1.debugFont, "Peck!", new Vector2(20, 15), Color.Black);

            //Draw Player coordinates
            spriteBatch.DrawString(Game1.debugFont, "Player pos: \n (" + playerPos.X + "\n, " + playerPos.Y + ")",
                    new Vector2(SCREEN_WIDTH - 150, 15), Color.Yellow);
            #endregion

            //Draw Timer
            //  ------------ Level timer here-------------

            //Draw Score
            spriteBatch.DrawString(Game1.debugFont, "Score: " + score.ToString(), new Vector2(SCREEN_WIDTH / 2, 15), Color.Black);

            base.Draw(spriteBatch);
        }

        #endregion


    }
}

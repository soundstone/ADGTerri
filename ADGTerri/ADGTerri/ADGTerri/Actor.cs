using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ADGTerri
{
    public class Actor
    {

        #region Declarations 

        protected Vector2 position;
        protected Vector2 velocity;
        protected bool enabled;
        protected bool onGround;
        protected bool flipped = false;
        float layerDepth = 0.85f;
        Rectangle drawArea;

        //for animations
        protected int frameWidth;
        protected int frameHeight;
        protected Rectangle collisionRectangle;
        protected int collideWidth;
        protected int collideHeight;
        protected Dictionary<string, AnimationStrip> animations =
            new Dictionary<string, AnimationStrip>();
        protected string currentAnimation;

        #endregion

        #region Properties

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 WorldCenter
        {
            get
            {
                return new Vector2(
                    (int)Position.X + (int)(frameWidth / 2),
                    (int)Position.Y + (int)(frameHeight / 2));
            }
        }

        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    frameWidth,
                    frameHeight);
            }
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X + collisionRectangle.X,
                    (int)Position.Y + collisionRectangle.Y,
                    collisionRectangle.Width,
                    collisionRectangle.Height);
            }

            set { collisionRectangle = value; }
        }

        #endregion

        
        public Actor(Vector2 position)
        {
            this.position = position;

            drawArea = new Rectangle(0,0,40,40);
            layerDepth = 0.6f;
        }

        #region Helper Functions

        private void UpdateAnimation(GameTime gameTime)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                if (animations[currentAnimation].FinishedPlaying)
                {
                    PlayAnimation(animations[currentAnimation].NextAnimation);
                }
                else
                {
                    animations[currentAnimation].Update(gameTime);
                }
            }
        }

        #endregion

        #region Public Functions

        public void PlayAnimation(string name)
        {
            if (!(name == null) && animations.ContainsKey(name))
            {
                currentAnimation = name;
                animations[name].Play();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!enabled)
                return;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateAnimation(gameTime);

            if (velocity.Y != 0)
                onGround = false;

            Vector2 moveAmount = velocity * elapsed;
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!enabled)
                return;
            if (animations.ContainsKey(currentAnimation))
            {
                SpriteEffects effect = SpriteEffects.None;
                if (flipped)
                {
                    effect = SpriteEffects.FlipHorizontally;
                }

                spriteBatch.Draw(
                    animations[currentAnimation].Texture,
                    Position,
                    animations[currentAnimation].FrameRectangle,
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    2.0f,
                    effect,
                    layerDepth);
            }
        }

        #endregion
    }
}

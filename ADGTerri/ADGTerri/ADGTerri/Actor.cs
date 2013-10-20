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
        protected Vector2 position;
       // protected Vector2 velocity;
        protected bool enabled;
        protected bool onGround;
        protected bool flipped = false;
        float layerDepth;
        Rectangle drawArea;

        //for animations
        //protected int frameWidth;
        //protected int frameHeight;
        //protected Rectangle collisionRectangle;
        //protected int collideWidth;
        //protected int collideHeight;


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

        public Actor(Vector2 position)
        {
            this.position = position;

            drawArea = new Rectangle(0,0,40,40);
            layerDepth = 0.6f;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}

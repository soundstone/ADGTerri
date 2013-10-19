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
        public Vector2 position;
        float layerDepth;
        Rectangle drawArea;

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

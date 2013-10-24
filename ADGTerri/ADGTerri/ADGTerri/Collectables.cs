using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ADGTerri
{
    class Collectable
    {
        Texture2D collectTexture;
        Vector2   collectPos;
        bool visable;

        public Texture2D Texture
        {
            get { return collectTexture; }
        }

        public Vector2 Position
        {
            get { return collectPos; }
        }

        public bool Visible
        {
            get { return visable; }
            set { visable = value; }
        }
        
        public Collectable(Texture2D texture, Vector2 pos, bool vis)
        {
            this.collectTexture = texture;
            this.collectPos = pos;
            this.visable = vis;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           // if (visable)
                spriteBatch.Draw(collectTexture, collectPos, Color.White);
        }
    }
}

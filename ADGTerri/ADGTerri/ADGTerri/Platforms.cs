using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ADGTerri
{
    public enum PlatformType
    {
        SMALL,
        MEDIUM,
        LARGE
    }

    public class Platform
    {
        /// <summary>
        /// Determines which platform to draw
        /// </summary>
        public PlatformType type;
        
        Texture2D platformTexture;
        
        Vector2 platformPosition;
        
        //bounding box for platform
        Rectangle platformRect;

        /// <summary>
        /// movePlatform = 0 for static and 1 for moving. Determines if MovePlatform() is called
        /// </summary>
        bool movePlatform;

        /// <summary>
        /// speed determines how fast the platform moves
        /// </summary>
        float speed;

        public bool MOVE
        {
            get { return movePlatform; }
        }

        public Platform(Texture2D tex, Vector2 pos, Rectangle rect, bool move)
        {
            this.platformTexture = tex;
            this.platformPosition = pos;
            this.platformRect = rect;
            this.movePlatform = move;
            this.speed = 0;
        }

        public Platform(Texture2D tex, Vector2 pos, Rectangle rect, bool move, float spd)
        {
            this.platformTexture = tex;
            this.platformPosition = pos;
            this.platformRect = rect;
            this.movePlatform = move;
            this.speed = spd;
        }

        public void MovePlatform()
        {
        }
    }
}

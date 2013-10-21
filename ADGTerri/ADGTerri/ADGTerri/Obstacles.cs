using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace ADGTerri
{
    enum ObstacleType
    {
        JUMP,
        ROLL,
        PECK
    }
    public class Obstacle
    {
        //image
        Texture2D obstacleTexture;
        //position
        Vector2 obstaclePositon;
        //bounding box
        Rectangle obstacleRect;
        //does it move?
        bool moveObstacle;
        //at what speed it moves
        float speed;

        public Texture2D Texture
        {
            get { return obstacleTexture; }
        }

        public bool Move
        {
            get { return moveObstacle; }
        }

        public Vector2 Position
        {
            get { return obstaclePositon; }
        }

        public Obstacle(Texture2D tex, Vector2 pos, bool move, float spd)
        {
            this.obstacleTexture = tex;
            this.obstaclePositon = pos;
            this.obstacleRect = new Rectangle(0, 0, obstacleTexture.Width, obstacleTexture.Height);
            this.moveObstacle = move;
            this.speed = spd;
        }

        public Obstacle(Texture2D tex, Vector2 pos, Rectangle rect, bool move)
        {
            this.obstacleTexture = tex;
            this.obstaclePositon = pos;
            this.obstacleRect = rect;
            this.moveObstacle = move;
            this.speed = 0;
        }

        public Obstacle(Texture2D tex, Vector2 pos, Rectangle rect, bool move, float spd)
        {
            this.obstacleTexture = tex;
            this.obstaclePositon = pos;
            this.obstacleRect = rect;
            this.moveObstacle = move;
            this.speed = spd;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(obstacleTexture, obstaclePositon, Color.White);
        }

        public void MoveObstacle()
        {
        }
    }
}

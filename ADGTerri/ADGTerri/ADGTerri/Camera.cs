using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ADGTerri
{
    public class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 center;

        public Camera(Viewport _view)
        {
            view = _view;
        }

        public void Update(GameTime gameTime, Player player)
        {
            center = new Vector2(0, player.PlayerPos.Y + (player.Height / 2) - 250);

            if (center.Y > Game1.SCREEN_HEIGHT -600)
                center.Y = Game1.SCREEN_HEIGHT -600;
            if (center.Y < -1350)
                center.Y = -1350;

            transform = Matrix.CreateScale(new Vector3(1, 1, 0))* Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}

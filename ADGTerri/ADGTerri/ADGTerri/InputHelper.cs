using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ADGTerri
{
    static class InputHelper
    {
        /// <summary>
        /// Old gamePad States. Cast to int.
        /// </summary>
        public static GamePadState OGS = new GamePadState();

        /// <summary>
        /// New GamePad States. Cast to int.
        /// </summary>
        public static GamePadState NGS = new GamePadState();

        /// <summary>
        /// Old Keyboard State
        /// </summary>
        public static KeyboardState OKS;

        /// <summary>
        /// New Keyboard state
        /// </summary>
        public static KeyboardState NKS;

        /// <summary>
        /// Update the input device states
        /// </summary>
        public static void UpdateStates()
        {
            //update gamepad
            OGS = NGS;
            NGS = GamePad.GetState(PlayerIndex.One);

            //update keyboard
            OKS = NKS;
            NKS = Keyboard.GetState();

        }

        public static bool WasButtonPressed(Buttons button)
        {
            return NGS.IsButtonDown(button) && OGS.IsButtonUp(button);
        }

        public static bool WasKeyPressed(Keys key)
        {
            return NKS.IsKeyDown(key) && OKS.IsKeyUp(key);
        }

        public static bool IsKeyHeld(Keys key)
        {
            return NKS.IsKeyDown(key);
        }
    }
}

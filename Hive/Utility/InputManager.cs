using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Utility
{
    public static class InputManager
    {
        private static MouseState currentMouseState;
        private static MouseState previousMouseState;

        public static Rectangle MouseRectangleBounds
        {
            get
            {
                return new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
            }
        }

        public static bool Clicked
        {
            get
            {
                return previousMouseState.LeftButton == ButtonState.Pressed
                    && currentMouseState.LeftButton == ButtonState.Released;
            }
        }

        public static bool MouseDown
        {
            get
            {
                return currentMouseState.LeftButton == ButtonState.Pressed;
            }
        }

        public static void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }


    }
}

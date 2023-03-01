using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Common
{
    public abstract class ClickableEntity : DrawnEntity
    {

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        public event EventHandler Click;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        protected ClickableEntity(Texture2D texture, Vector2 position) : base(texture, position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            CheckMouse();
        }

        public void CheckMouse()
        {
            if (InputManager.MouseRectangleBounds.Intersects(Rectangle))
            {
                //Hovering logic here?

                if (InputManager.Clicked)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}

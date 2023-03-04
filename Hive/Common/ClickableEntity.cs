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
        protected bool isHovering = false;

        public event EventHandler Click;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * scale), (int)(Texture.Height * scale));
            }
        }

        protected ClickableEntity(Texture2D texture, Vector2 position, float scale) : base(texture, position, scale)
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
                isHovering = true;
                //Hovering logic here?

                if (InputManager.Clicked)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
            else
            { 
                isHovering = false;
            }
        }
    }
}

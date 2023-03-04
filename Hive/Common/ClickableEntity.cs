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

        protected bool isHovering = false;
        protected bool isPressed = false;

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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = isPressed? Color.LightGray : Color.White;
            spriteBatch.Draw(texture, Position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 1);

        }

        public void CheckMouse()
        {
            if (InputManager.MouseRectangleBounds.Intersects(Rectangle))
            {
                isHovering = true;
                //Hovering logic here?
                isPressed = InputManager.MouseDown;

                if (InputManager.Clicked)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
            else
            { 
                isHovering = false;
                isPressed = false;
            }
        }
    }
}

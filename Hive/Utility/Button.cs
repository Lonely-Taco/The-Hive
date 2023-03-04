using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Utility
{
    public class Button : ClickableEntity
    {
        protected string text;
        protected SpriteFont font;
        protected Vector2 textOffset;
        public Button(Texture2D texture, Vector2 position, string text, SpriteFont font, float scale) : base(texture, position, scale)
        {
            this.text = text;
            this.font = font;
            textOffset = new Vector2(8, 8);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, text, Position + textOffset, Color.Black,0f, Vector2.Zero, scale * 7, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

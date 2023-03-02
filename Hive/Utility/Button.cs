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
        public Button(Texture2D texture, Vector2 position, string text, SpriteFont font) : base(texture, position)
        {
            this.text = text;
            this.font = font;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, float scale = 1)
        {
            base.Draw(gameTime, spriteBatch, scale);
            spriteBatch.DrawString(font, text, position, Color.Black,0f, Vector2.Zero, scale, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Drops
{
    internal class EventText : DrawnEntity
    {
        protected string text;
        protected Color color;
        protected float alpha;
        protected float fadeOutSpeed;
        protected DropManager dropManager;

        private static SpriteFont font;

        public EventText(Vector2 position, float scale, string text, Color color, float fadeOutSpeed, DropManager dropManager) : base(null, position, scale)
        {
            this.text = text;
            this.color = color;
            this.alpha = 1f;
            this.fadeOutSpeed = fadeOutSpeed;
            this.dropManager = dropManager;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle? rectangle)
        {
            spriteBatch.DrawString(font, text, position, color * alpha, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
        }

        public override void Update(GameTime gameTime)
        {
            alpha -= fadeOutSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if(alpha < 0f) 
            { 
                dropManager.RemoveEventText(this);
            }
        }

        public static void Initialize(SpriteFont font)
        {
            EventText.font = font;
        }
    }
}

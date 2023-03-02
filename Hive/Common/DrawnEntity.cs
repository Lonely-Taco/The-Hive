using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Hive.Common
{
    public class DrawnEntity : IEntity
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 scale;
        public DrawnEntity(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, float scale = 1)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

    }
}

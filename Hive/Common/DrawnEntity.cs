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
        private Texture2D texture;
        private Vector2 position;
        protected float scale;

        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Texture { get => texture; set => texture = value; }

        public DrawnEntity(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.Position = position;
            this.scale = scale;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 1);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

    }
}

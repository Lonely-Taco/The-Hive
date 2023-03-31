using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hive.Common
{
    public class DrawnEntity : IEntity
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Color color;
        protected float scale;
        protected Guid guid;

        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Texture { get => texture; set => texture = value; }

        public DrawnEntity(Texture2D texture, Vector2 position, float scale, Color? color = null)
        {
            this.texture = texture;
            this.Position = position;
            this.scale = scale;
            this.guid = Guid.NewGuid();
            this.color = color ?? Color.White;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 1);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public Guid GetGuid()
        {
            return guid;
        }
    }
}

using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shop
{
    public abstract class Shop : DrawnEntity
    {
        protected Counter nectarCounter;
        protected Button buyButton;
        protected Vector2 iconOffset = new Vector2(0, 10);
        protected Vector2 buyButtonOffset = new Vector2(50, 10);
        protected DrawnEntity icon;


        protected Shop(Texture2D icon, Counter nectarCounter, Texture2D buttonTexture, Texture2D backgroundTexture, Vector2 position, SpriteFont font) : base(backgroundTexture, position)
        {
            this.icon = new DrawnEntity(icon, position + iconOffset);
            this.nectarCounter = nectarCounter;
            this.buyButton = new Button(buttonTexture, position + buyButtonOffset, "Buy", font);
        }

        public abstract int CurrentCost(int currentAmount);

        public abstract void Buy();

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, float scale = 1f)
        {
            base.Draw(gameTime, spriteBatch, 0.5f * scale);
            icon.Draw(gameTime, spriteBatch, 0.9f * scale);
            buyButton.Draw(gameTime, spriteBatch, 0.9f * scale);
        }

        public override void Update(GameTime gameTime)
        {
            buyButton.Update(gameTime);
        }
    }
}

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
using static System.Net.Mime.MediaTypeNames;

namespace Hive.Shop
{
    public abstract class Shop : DrawnEntity
    {
        protected Counter nectarCounter;
        protected Button buyButton;
        protected Vector2 iconOffset = new Vector2(0, 10);
        protected Vector2 buyButtonOffset = new Vector2(50, 10);
        protected DrawnEntity icon;
        protected DrawnEntity costIcon;
        protected Vector2 costIconOffset = new Vector2(0, 30);
        protected Vector2 costTextOffset = new Vector2(0, 20);
        protected SpriteFont font;
        protected Counter counter;


        protected Shop(Texture2D icon, Counter nectarCounter, Texture2D buttonTexture, Texture2D backgroundTexture, Vector2 position, SpriteFont font, Texture2D costIcon, Counter counter) : base(backgroundTexture, position)
        {
            this.icon = new DrawnEntity(icon, position + iconOffset);
            this.nectarCounter = nectarCounter;
            this.buyButton = new Button(buttonTexture, position + buyButtonOffset, "Buy", font);
            this.costIcon = new DrawnEntity(costIcon, position + costIconOffset);
            this.counter = counter;
            this.font = font;
        }

        public abstract int CurrentCost();

        public abstract void Buy();

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, float scale = 1f)
        {
            base.Draw(gameTime, spriteBatch, 0.5f * scale);
            icon.Draw(gameTime, spriteBatch, 0.9f * scale);
            costIcon.Draw(gameTime, spriteBatch, 0.9f * scale);
            //Cost string
            spriteBatch.DrawString(font, CurrentCost().ToString() , position + costTextOffset, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 1);
            buyButton.Draw(gameTime, spriteBatch, 0.9f * scale);
        }

        public override void Update(GameTime gameTime)
        {
            buyButton.Update(gameTime);
        }
    }
}

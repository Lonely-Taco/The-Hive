using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hive.Shops
{
    public abstract class Shop : DrawnEntity
    {
        protected Counter nectarCounter;
        protected Vector2 iconOffset = new Vector2(0, 10);
        protected Button buyButton;
        protected Vector2 buyButtonOffset = new Vector2(108, 3);
        protected DrawnEntity icon;
        protected Vector2 costIconOffset = new Vector2(0, 30);
        protected Vector2 costTextOffset = new Vector2(0, 20);
        protected DrawnEntity costIcon;
        protected Counter counter;

        protected static SpriteFont font;
        protected static Texture2D backgroundTexture;
        protected static Texture2D buttonTexture;
        protected static Texture2D nectarTexture;

        protected Shop(Texture2D icon, Counter nectarCounter, Vector2 position, Counter counter) : base(Shop.backgroundTexture, position)
        {
            this.icon = new DrawnEntity(icon, position + iconOffset);
            this.nectarCounter = nectarCounter;
            this.buyButton = new Button(buttonTexture, position + buyButtonOffset, "Buy", font);
            this.costIcon = new DrawnEntity(nectarTexture, position + costIconOffset);
            this.counter = counter;
            buyButton.Click += OnClick;

        }

        protected void OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(Buy);
        }

        public abstract int CurrentCost();

        public virtual async void Buy()
        {
            if (await nectarCounter.AddCount(-CurrentCost()))
            {
                await counter.AddCount(1);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, float scale = 1f)
        {
            base.Draw(gameTime, spriteBatch, 0.3f * scale);
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

        public static void Initialize(SpriteFont font, Texture2D buttonTexture, Texture2D nectarTexture, Texture2D backgroundTexture)
        {
            Shop.font = font;
            Shop.buttonTexture = buttonTexture;
            Shop.nectarTexture = nectarTexture;
            Shop.backgroundTexture = backgroundTexture;
        }

    }
}

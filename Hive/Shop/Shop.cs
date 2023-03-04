using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;

namespace Hive.Shops
{
    public abstract class Shop : DrawnEntity
    {
        protected Counter counter;
        protected Counter nectarCounter;

        protected DrawnEntity icon;
        protected Vector2 iconOffset = new Vector2(5, 6);
        protected Button buyButton;
        protected Vector2 buyButtonOffset = new Vector2(176, 4);
        protected DrawnEntity costIcon;
        protected Vector2 costIconOffset = new Vector2(120, 7);
        protected Vector2 costTextOffset = new Vector2(110, 13);

        protected static SpriteFont font;
        protected static Texture2D backgroundTexture;
        protected static Texture2D buttonTexture;
        protected static Texture2D nectarTexture;

        public event EventHandler OnBuy;

        protected Shop(Texture2D icon, Counter nectarCounter, Vector2 position, Counter counter, float scale) : base(Shop.backgroundTexture, position, 0.2f * scale)
        {
            this.icon = new DrawnEntity(icon, position + iconOffset, 0.9f * scale);
            this.nectarCounter = nectarCounter;
            this.buyButton = new Button(buttonTexture, position + buyButtonOffset, "+1", font, 0.13f * scale);
            this.costIcon = new DrawnEntity(nectarTexture, position + costIconOffset, 0.9f * scale);
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
                OnBuy?.Invoke(this, EventArgs.Empty);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            icon.Draw(gameTime, spriteBatch);
            costIcon.Draw(gameTime, spriteBatch);
            //Cost string
            spriteBatch.DrawString(font, CurrentCost().ToString() , position + costTextOffset, Color.Black, 0f, Vector2.Zero, scale * 5, SpriteEffects.None, 1);
            buyButton.Draw(gameTime, spriteBatch);
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

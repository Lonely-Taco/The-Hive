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
        protected Vector2 costTextOffset = new Vector2(100, 13);
        protected DrawnEntity costBackground;
        protected Vector2 costBackgroundOffset = new Vector2(85, 8);

        protected static SpriteFont font;
        protected static Texture2D backgroundTexture;
        protected static Texture2D buttonTexture;
        protected static Texture2D nectarTexture;
        protected static Texture2D costBackgroundTexture;

        protected Shop(Texture2D icon, Counter nectarCounter, Vector2 position, Counter counter, float scale) : base(Shop.backgroundTexture, position, 0.2f * scale)
        {
            this.icon = new DrawnEntity(icon, position + iconOffset, 0.9f * scale);
            this.nectarCounter = nectarCounter;
            this.buyButton = new Button(buttonTexture, position + buyButtonOffset, "+1", font, 0.13f * scale);
            this.costIcon = new DrawnEntity(nectarTexture, position + costIconOffset, 0.9f * scale);
            this.costBackground = new DrawnEntity(costBackgroundTexture, position + costBackgroundOffset, scale * 0.38f);
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            icon.Draw(gameTime, spriteBatch);
            costBackground.Draw(gameTime, spriteBatch);
            costIcon.Draw(gameTime, spriteBatch);
            //Cost string
            spriteBatch.DrawString(font, CurrentCost().ToString() , position + costTextOffset, Color.Black, 0f, Vector2.Zero, scale * 5, SpriteEffects.None, 1);
            buyButton.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            buyButton.Update(gameTime);
        }

        public static void Initialize(SpriteFont font, Texture2D buttonTexture, Texture2D nectarTexture, Texture2D backgroundTexture, Texture2D costBackgroundTexture)
        {
            Shop.font = font;
            Shop.buttonTexture = buttonTexture;
            Shop.nectarTexture = nectarTexture;
            Shop.backgroundTexture = backgroundTexture;

            Shop.costBackgroundTexture = costBackgroundTexture;
        }

    }
}

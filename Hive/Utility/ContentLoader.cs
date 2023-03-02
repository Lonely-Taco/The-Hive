using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Utility
{
    public class ContentLoader
    {
        public Texture2D nectarTexture;
        public Texture2D shopTexture;
        public Texture2D mapTexture;
        public Texture2D backgroundTexture;
        public Texture2D antTexture;
        public Texture2D counterTexture;
        public Texture2D buyButtonTexture;
        public Texture2D shopBackgroundTexture;

        public SpriteFont counterFont;

        public ContentLoader(HiveGame game)
        {
            nectarTexture = game.Content.Load<Texture2D>("Textures/Nectar");
            shopTexture = game.Content.Load<Texture2D>("Textures/Missing");
            mapTexture = game.Content.Load<Texture2D>("Textures/Grass");
            backgroundTexture = game.Content.Load<Texture2D>("Textures/missing");
            antTexture = game.Content.Load<Texture2D>("Textures/Ant");
            counterTexture = game.Content.Load<Texture2D>("Textures/Missing");
            buyButtonTexture = game.Content.Load<Texture2D>("Textures/BuyButton");
            shopBackgroundTexture = game.Content.Load<Texture2D>("Textures/Button");

            counterFont = game.Content.Load<SpriteFont>("Fonts/DefaultFont");
        }

    }
}

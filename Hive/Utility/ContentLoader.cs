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

        public ContentLoader(HiveGame game)
        {
            nectarTexture = game.Content.Load<Texture2D>("Textures/Nectar");
            shopTexture = game.Content.Load<Texture2D>("Textures/Missing");
            mapTexture = game.Content.Load<Texture2D>("Textures/Missing");
            backgroundTexture = game.Content.Load<Texture2D>("Textures/Missing");
            antTexture = game.Content.Load<Texture2D>("Textures/Missing");
            counterTexture = game.Content.Load<Texture2D>("Textures/Missing");
            buyButtonTexture = game.Content.Load<Texture2D>("Textures/Missing");
        }

    }
}

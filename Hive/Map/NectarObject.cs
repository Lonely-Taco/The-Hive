﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hive.Map
{
    public class NectarObject : HiveMapObject
    {
        public NectarObject(Vector2 mapCoordinates, Texture2D texture, Vector2 position) : base(mapCoordinates, texture, position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Common
{
    public interface IEntity
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public void Update(GameTime gameTime);
    }
}

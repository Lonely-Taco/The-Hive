using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hive.GameStates;

public class GameState : State
{
    public GameState(Vector2 position, float scale, List<IEntity> entities, HiveGame game) :
        base(position, scale, entities, game)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var entity in Entities)
        {
            entity.Draw(gameTime, spriteBatch);
        }
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }
    }

    public override async Task ExecuteState()
    {
        
    }
}
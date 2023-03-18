using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hive.GameStates;

public class GameState : State
{
    private DrawnEntity sceneBackground;
    
    public GameState(Vector2 position, float scale, List<IEntity> entities, HiveGame game) :
        base(position, scale, entities, game)
    {
        sceneBackground = new DrawnEntity(sceneBackgroundTexture, 
                                          new Vector2(0, 0), 
                                          scale);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        sceneBackground.Draw(gameTime, spriteBatch);
        foreach (var entity in Entities)
        {
            entity.Draw(gameTime, spriteBatch);
        }
    }

    public override void Update(GameTime gameTime)
    {
        sceneBackground.Update(gameTime);
        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }
    }

    public override async Task ExecuteState()
    {
        
    }
}
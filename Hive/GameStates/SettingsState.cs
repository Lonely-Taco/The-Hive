using System.Collections.Generic;
using System.Threading.Tasks;
using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hive.GameStates;

public class SettingsState : State
{
    protected Button submitButton;
    
    public SettingsState(Vector2 position, float scale, List<IEntity> entities, HiveGame game) : base(position, scale, entities, game)
    {
    
        submitButton = new Button(menuButtonTexture,
                                position + new Vector2(game.ScreenSizeX * .3f, game.ScreenSizeY * .10f),
                                "Submit",
                                font,
                                .20f * scale
        );
        
        entities.Add(submitButton);
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var e in Entities)
        {
            e.Update(gameTime);
        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var e in Entities)
        {
            e.Draw(gameTime, spriteBatch);
        }
        base.Draw(gameTime, spriteBatch);
    }

    public override Task ExecuteState()
    {
        throw new System.NotImplementedException();
    }
}
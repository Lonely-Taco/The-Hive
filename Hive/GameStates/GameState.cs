using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hive.GameStates;

public class GameState : State
{
    private DrawnEntity sceneBackground;
    private Button settingsButton;

    public GameState(Vector2 position, float scale, List<IEntity> entities, HiveGame game, Texture2D settingButtonTexture) :
        base(position, scale, entities, game)
    {
        sceneBackground = new DrawnEntity(sceneBackgroundTexture,
                                          new Vector2(0, 0),
                                          scale,
                                          null);

        settingsButton = new Button(settingButtonTexture,
                                    position + new Vector2(game.ScreenSizeX * .88f, game.ScreenSizeY * .1f),
                                    "",
                                    font,
                                    .04f * scale
        );

        settingsButton.Click += SettingsButtonOnClick;
    }

    private void SettingsButtonOnClick(object sender, EventArgs e)
    {
        Debug.WriteLine("Settings");
        Task.Factory.StartNew(ExecuteState);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        sceneBackground.Draw(gameTime, spriteBatch);
        settingsButton.Draw(gameTime, spriteBatch);

        foreach (var entity in Entities)
        {
            entity.Draw(gameTime, spriteBatch);
        }
    }

    public override void Update(GameTime gameTime)
    {
        sceneBackground.Update(gameTime);
        settingsButton.Update(gameTime);
        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }
    }

    public override async Task ExecuteState()
    {
        game.ChangeState(game.SettingState);
    }
}
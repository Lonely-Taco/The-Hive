using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hive.GameStates;

public class MenuState : State
{
    #region Fields

    protected DrawnEntity menuBackground;
    protected DrawnEntity sceneBackground;
    protected Button      playButton;
    protected Button      settingsButton;
    protected Button      exitButton;

    #endregion


    public MenuState(Vector2 position, float scale, List<IEntity> entities, HiveGame game) : base(
        position, scale, entities, game)
    {
        sceneBackground = new DrawnEntity(sceneBackgroundTexture,
                                          new Vector2(0, 0),
                                          scale);
        entities.Add(sceneBackground);
        
        menuBackground = new DrawnEntity(menuBackgroundTexture,
                                         new Vector2(game.ScreenSizeX * .25f, game.ScreenSizeY * .25f),
                                         .65f * scale);
        entities.Add(menuBackground);
        
        
        playButton = new Button(menuButtonTexture,
                                position + new Vector2(game.ScreenSizeX * .3f, game.ScreenSizeY * .10f),
                                "Start",
                                font,
                                .20f * scale
        );
        entities.Add(playButton);
        
        settingsButton = new Button(menuButtonTexture,
                                    position + new Vector2(game.ScreenSizeX * .3f, game.ScreenSizeY * .20f),
                                    "Config",
                                    font,
                                    .20f * scale);
        entities.Add(settingsButton);
        
        exitButton = new Button(menuButtonTexture,
                                position + new Vector2(game.ScreenSizeX * .3f, game.ScreenSizeY * .30f),
                                "Exit",
                                font,
                                .20f * scale);
        entities.Add(exitButton);
        
        playButton.Click     += PlayButtonOnClick;
        settingsButton.Click += SettingsButtonOnClick;
        exitButton.Click     += ExitButtonOnClick;
    }

    private void ExitButtonOnClick(object sender, EventArgs e)
    {
        Task.Factory.StartNew(game.Quit);
    }

    private void SettingsButtonOnClick(object sender, EventArgs e)
    {
        Debug.WriteLine("Ive been clicked");
    }

    private void PlayButtonOnClick(object sender, EventArgs e)
    {
        Task.Factory.StartNew(PlayGame);
    }

    private async void PlayGame()
    {
        await ExecuteState();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var entity in Entities)
        {
            entity.Draw(gameTime, spriteBatch);
        }
        
        spriteBatch.DrawString(font,
                               "HIVE",
                               new Vector2(game.ScreenSizeX * .30f, game.ScreenSizeY * .30f),
                               Color.Black,
                               0f,
                               Vector2.Zero,
                               scale * 6,
                               SpriteEffects.None,
                               1);
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
        game.ChangeState(game.GameState);
    }
}
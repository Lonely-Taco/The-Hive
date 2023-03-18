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
    protected Vector2     buttonOffset = new Vector2(250, 50);
    protected Vector2     textOffset   = new Vector2(100, 10);
    protected DrawnEntity menuBackground;
    protected DrawnEntity sceneBackground;
    protected Vector2     backgroundOffset = new Vector2(85, 8);
    protected Button      playButton;
    protected Button      settingsButton;

    public MenuState(Vector2 position, float scale, List<IEntity> entities, HiveGame game) : base(
        position, scale, entities, game)
    {
        playButton = new Button(menuButtonTexture, 
                                position + new Vector2(game.ScreenSizeX * .3f, game.ScreenSizeY  * .15f),
                                "Start", 
                                font, 
                                .25f * scale
                                );
        settingsButton = new Button(menuButtonTexture, 
                                    position + new Vector2(game.ScreenSizeX * .3f, game.ScreenSizeY  * .30f), 
                                    "Config", 
                                    font, 
                                    .25f * scale);
        
        menuBackground = new DrawnEntity(menuBackgroundTexture, 
                                         new Vector2(game.ScreenSizeX * .25f, game.ScreenSizeY  * .25f),
                                         .75f * scale);
        
        sceneBackground = new DrawnEntity(sceneBackgroundTexture, 
                                          new Vector2(0, 0), 
                                          scale);
        
        playButton.Click += PlayButtonOnClick;
        settingsButton.Click += SettingsButtonOnClick;
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
        sceneBackground.Draw(gameTime, spriteBatch);
        menuBackground.Draw(gameTime, spriteBatch);
        spriteBatch.DrawString(font, 
                               "HIVE", 
                               new Vector2(game.ScreenSizeX * .30f, game.ScreenSizeY  * .30f), 
                               Color.Black, 
                               0f, 
                               Vector2.Zero,
                               scale * 6,
                               SpriteEffects.None,
                               1);
        
        playButton.Draw(gameTime, spriteBatch);
        settingsButton.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        playButton.Update(gameTime);
        settingsButton.Update(gameTime);
    }

    public override async Task ExecuteState()
    {
        Debug.WriteLine("starting game");
        game.ChangeState(game.GameState);
    }
}
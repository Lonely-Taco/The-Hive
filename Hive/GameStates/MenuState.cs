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
    protected Vector2     buttonOffset = new Vector2(200, 10);
    protected Vector2     textOffset   = new Vector2(100, 13);
    protected DrawnEntity menuBackground;
    protected Vector2     backgroundOffset = new Vector2(85, 8);
    protected Button      playButton;

    public MenuState(Vector2 position, float scale, List<IEntity> entities, HiveGame game) : base(position, scale, entities, game)
    {
        this.playButton     =  new Button(menuButtonTexture, position + buttonOffset, "Start", font, 0.13f * scale);
        this.menuBackground =  new DrawnEntity(menuBackgroundTexture, position + backgroundOffset, scale);
        playButton.Click   += PlayButtonOnClick;
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
        menuBackground.Draw(gameTime, spriteBatch);
        spriteBatch.DrawString(font, "MENU", Position + textOffset, Color.Black, 0f, Vector2.Zero,
                               scale * 4, SpriteEffects.None, 1);

        playButton.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        this.playButton.Update(gameTime);
    }
    
    public override async Task ExecuteState()
    {
        Debug.WriteLine("starting game");
        game.ChangeState(game.GameState);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hive.GameStates;

public class SettingsState : State
{
    protected DrawnEntity menuBackground;
    protected DrawnEntity sceneBackground;
    protected Button submitButton;
    protected Button addButton;
    protected Button subtracttButton;

    private int semaphoreAmount;
    private List<ColorSetting> colorSettings;

    private int selectedColorIndex;
    private Color defaultColor = Color.Black;

    public SettingsState(Vector2 position, float scale, List<IEntity> entities, HiveGame game) : base(
        position, scale, entities, game)
    {
        menuBackground = new DrawnEntity(menuBackgroundTexture,
                                         new Vector2(game.ScreenSizeX * .2f, game.ScreenSizeY * .25f),
                                         scale);

        submitButton = new Button(buttonTexture,
                                  position + new Vector2(game.ScreenSizeX * .3f, game.ScreenSizeY * .50f),
                                  "Submit",
                                  font,
                                  .20f * scale
        );

        addButton = new Button(buttonTexture,
                               new Vector2(game.ScreenSizeX * .45f, game.ScreenSizeY * .35f),
                               "+",
                               font,
                               .12f * scale
        );

        subtracttButton = new Button(buttonTexture,
                                     new Vector2(game.ScreenSizeX * .35f, game.ScreenSizeY * .35f),
                                     "-",
                                     font,
                                     .12f * scale
        );
        submitButton.Click += SubmitButtonOnClick;


        entities.Add(menuBackground);
        entities.Add(submitButton);
        entities.Add(addButton);
        entities.Add(subtracttButton);
    }

    private void SubmitButtonOnClick(object sender, EventArgs e)
    {
        Task.Factory.StartNew(ExecuteState);
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

        spriteBatch.DrawString(font,
                               "Semaphore",
                               new Vector2(game.ScreenSizeX * .25f, game.ScreenSizeY * .35f),
                               Color.Black,
                               0f,
                               Vector2.Zero,
                               scale,
                               SpriteEffects.None,
                               1);

        spriteBatch.DrawString(font,
                               "Ant Colour",
                               new Vector2(game.ScreenSizeX * .25f, game.ScreenSizeY * .40f),
                               Color.Black,
                               0f,
                               Vector2.Zero,
                               scale,
                               SpriteEffects.None,
                               1);
    }

    public override async Task ExecuteState()
    {
        FileManager.CreateDirectory();
        _ = FileManager.WriteToFile(semaphoreAmount, GetSelectedColor());
        game.ChangeState(game.GameState);
    }

    private Color GetSelectedColor()
    {
        if (colorSettings.Count < selectedColorIndex && selectedColorIndex >= 0)
        {
            return new Color(colorSettings[selectedColorIndex].RGB);
        }
        else
        {
            return defaultColor;
        }
    }
}

public struct ColorSetting
{
    public string name;
    public Vector3 RGB;
}

public enum ColorEnum
{
    Red,
    White,
    Blue,
    Black,
}
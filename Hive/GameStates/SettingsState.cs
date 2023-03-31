using System;
using System.Collections.Generic;
using System.Linq;
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

    protected Button blueButton;
    protected Button blackButton;
    protected Button yellowButton;

    public SettingsState(Vector2 position, float scale, List<IEntity> entities, HiveGame game, SettingData settings) : base(
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

        colorSettings = new List<ColorSetting> { 
            new ColorSetting("Black", new Vector3(0, 0, 0)),
            new ColorSetting("Blue", new Vector3(10, 10, 250)),
            new ColorSetting("Yellow", new Vector3(255, 255, 0))
        };

        SetSettings(settings);

        if (selectedColorIndex == -1)
        {
            selectedColorIndex = 0;
        }

        submitButton.Click += SubmitButtonOnClick;
        addButton.Click += AddButtonOnClick;
        subtracttButton.Click += SubstractButtonOnClick;

        blackButton = new Button(buttonTexture,
                               new Vector2(game.ScreenSizeX * .35f, game.ScreenSizeY * .40f),
                               "Black",
                               font,
                               .12f * scale
        );

        blueButton = new Button(buttonTexture,
                                new Vector2(game.ScreenSizeX * .40f, game.ScreenSizeY * .40f),
                                "Blue",
                                font,
                                .12f * scale
        );

        yellowButton = new Button(buttonTexture,
                                 new Vector2(game.ScreenSizeX * .45f, game.ScreenSizeY * .40f),
                                 "Yellow",
                                 font,
                                 .12f * scale
        );

        entities.Add(menuBackground);
        entities.Add(submitButton);
        entities.Add(addButton);
        entities.Add(subtracttButton);

        entities.Add(blackButton);
        entities.Add(blueButton);
        entities.Add(yellowButton);
        blackButton.Click += BlackButtonOnClick;
        blueButton.Click += BlueButtonOnClick;
        yellowButton.Click += YellowButtonOnClick;
    }

    private void SubmitButtonOnClick(object sender, EventArgs e)
    {
        Task.Factory.StartNew(ExecuteState);
    }

    private void AddButtonOnClick(object sender, EventArgs e)
    {
        semaphoreAmount += 1;
    }
    private void SubstractButtonOnClick(object sender, EventArgs e)
    {
        semaphoreAmount = Math.Max(semaphoreAmount - 1, 0);
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
                               "Semaphore: " + semaphoreAmount.ToString(),
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
        SettingData settings = new SettingData(semaphoreAmount,GetSelectedColor());
        FileManager.CreateDirectory();
        _ = FileManager.WriteToFile(semaphoreAmount, GetSelectedColor());
        game.ChangeState(game.GameState, settings);
    }

    private Color GetSelectedColor()
    {
        if (selectedColorIndex < colorSettings.Count && selectedColorIndex >= 0)
        {
            return new Color(colorSettings[selectedColorIndex].RGB);
        }
        else
        {
            return defaultColor;
        }
    }

    public void SetSettings(SettingData settings)
    {
        semaphoreAmount = settings.semaphoreAmount;
        selectedColorIndex = colorSettings.IndexOf(colorSettings.Where(x => x.RGB.X == settings.antColor.R &&
                                                                        x.RGB.Y == settings.antColor.G &&
                                                                        x.RGB.Z == settings.antColor.B
                                                                        ).FirstOrDefault());
    }


    private void BlackButtonOnClick(object sender, EventArgs e)
    {
        selectedColorIndex = 0;
    }
    private void BlueButtonOnClick(object sender, EventArgs e)
    {
        selectedColorIndex = 1;

    }

    private void YellowButtonOnClick(object sender, EventArgs e)
    {
        selectedColorIndex = 2;

    }
}

public struct ColorSetting
{

    public string name;
    public Vector3 RGB;

    public ColorSetting(string name, Vector3 rGB)
    {
        this.name = name;
        RGB = rGB;
    }
}

public enum ColorEnum
{
    Red,
    White,
    Blue,
    Black,
}

public struct SettingData
{

    public int semaphoreAmount;
    public Color antColor;

    public SettingData(int semaphoreAmount, Color antColor)
    {
        this.semaphoreAmount = semaphoreAmount;
        this.antColor = antColor;
    }
}
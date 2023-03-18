using System.Collections.Generic;
using System.Threading.Tasks;
using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hive.GameStates;

public abstract class State : DrawnEntity
{
    protected static SpriteFont font;
    protected static Texture2D  menuButtonTexture;
    protected static Texture2D  menuBackgroundTexture;
    protected static Texture2D  sceneBackgroundTexture;

    private List<IEntity> entities = new();

    protected HiveGame game;

    public HiveGame Game
    {
        get => game;
        set => game = value;
    }

    protected List<IEntity> Entities
    {
        get => entities;
        set => entities = value;
    }

    protected State(Vector2 position, float scale, List<IEntity> entities, HiveGame game) : base(
        menuBackgroundTexture, position, 0.2f * scale)
    {
        this.entities = entities;
        this.game     = game;
    }


    public static void Initialize(SpriteFont fontSprite, Texture2D buttonTexture, Texture2D backgroundTexture, Texture2D sceneTexture)
    {
        font                   = fontSprite;
        menuBackgroundTexture  = backgroundTexture;
        menuButtonTexture      = buttonTexture;
        sceneBackgroundTexture = sceneTexture;
    }

    public abstract Task ExecuteState();

}
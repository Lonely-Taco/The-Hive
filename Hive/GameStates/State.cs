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

    protected List<IEntity> entities = new List<IEntity>();

    protected HiveGame game;

    public HiveGame Game
    {
        get => game;
        set => game = value;
    }

    public List<IEntity> Entities
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


    public static void Initialize(SpriteFont font, Texture2D menuButtonTexture, Texture2D menuBackgroundTexture)
    {
        State.font                  = font;
        State.menuBackgroundTexture = menuBackgroundTexture;
        State.menuButtonTexture     = menuButtonTexture;
    }

    public abstract Task ExecuteState();

}
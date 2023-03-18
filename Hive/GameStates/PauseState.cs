using System.Collections.Generic;
using System.Threading.Tasks;
using Hive.Common;
using Microsoft.Xna.Framework;

namespace Hive.GameStates;

public class PauseState : State
{
    public PauseState(Vector2 position, float scale, List<IEntity> entities, HiveGame game) : base(position, scale, entities, game)
    {
    }

    public override Task ExecuteState()
    {
        throw new System.NotImplementedException();
    }
}
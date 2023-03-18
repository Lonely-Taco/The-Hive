using Hive.Drops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Hive.Map
{
    internal class AntObject : HiveMapObject
    {
        private Vector2? currentDestination;
        private Vector2 currentDirection;
        private NectarObject currentTarget;
        private int speed;
        private AntState _state;

        public NectarObject CurrentTarget { get => currentTarget; set => currentTarget = value; }

        public event EventHandler OnStateChanged;

        public AntObject(int speed, Vector2 mapCoordinates, Texture2D texture, Vector2 position) : base(mapCoordinates, texture, position)
        {
            this.speed = speed;
            _state = AntState.Idle;
        }

        private NectarObject GetNearestNectar(ConcurrentDictionary<Guid, NectarObject> nectarObjects)
        {
            float shortestDistance = float.MaxValue;
            NectarObject nearestNectar = null;

            if (currentTarget!= null)
            {
                return currentTarget;  
            }

            foreach (NectarObject nectarObject in nectarObjects.Values)
            {
                var distance = Vector2.Distance(nectarObject.GetMapCoordinates(), this.GetMapCoordinates());
                
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestNectar = nectarObject;
                }
            }

            return nearestNectar;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void SetCurrentDestination(ConcurrentDictionary<Guid, NectarObject> nectarObjects)
        {
            NectarObject nectar = GetNearestNectar(nectarObjects);
            if (nectar != null)
            {
                currentDestination = nectar.Position;
                currentDirection = currentDestination.Value - Position;
                currentTarget = nectar;
                currentDirection.Normalize();
                SetState(AntState.Moving);
                return;
            }

            currentDirection = Vector2.Zero;
        }

        public void Move()
        {
            if (currentTarget == null)
            {
                return;
            }

            Position += currentDirection * speed;

            if (Vector2.Distance(Position, currentDestination.Value) < 1f)
            {
                currentTarget.Claim();
                SetState(AntState.Idle);
            }
        }
        public AntState GetState()
        {
            return _state;
        }
        public void SetState(AntState newState)
        {
            if (newState == _state) return;
            _state = newState;
            OnStateChanged.Invoke(this, EventArgs.Empty);
        }

        private void ResetTarget()
        {
            this.currentDestination = null;
            this.currentTarget = null;
            this.currentDirection = Vector2.Zero;
        }
    }
}

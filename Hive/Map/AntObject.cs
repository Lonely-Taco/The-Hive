using Hive.Drops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
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

        public AntObject(int speed,Texture2D texture, Vector2 position) : base(texture, position)
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
                var distance = Vector2.Distance(nectarObject.Position, this.Position);
                
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
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(3000));
            currentTarget = GetNearestNectar(nectarObjects);
            Vector2? currentDestination = currentTarget?.Position;
            if(currentDestination.HasValue)
            {
                this.currentDestination = currentDestination.Value;
                this.currentDirection = currentDestination.Value - Position;
                this.currentDirection.Normalize();
                SetState(AntState.Moving);
            }
            else
            {
                currentTarget = null;
            }
        }

        public void Move()
        {
            if (currentDestination == null)
            {
                return;
            }

            Position += currentDirection * speed;

            if (Vector2.Distance(Position, currentDestination.Value) < 2f)
            {
                Task.Factory.StartNew(currentTarget.Claim);
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

            switch (newState)
            {
                case AntState.Idle:
                    ResetTarget();
                    break;
                case AntState.Searching:

                    break;
                case AntState.Moving:

                    break;
                default: break;
            }

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

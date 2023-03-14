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
        private Vector2 currentDestination;
        private Vector2 currentDirection;
        private NectarObject currentTarget;

        private int speed;

        public NectarObject CurrentTarget { get => currentTarget; set => currentTarget = value; }

        public event EventHandler OnNectarPickUp;
        public event EventHandler OnNectarTargeted;

        public AntObject(int speed, Vector2 mapCoordinates, Texture2D texture, Vector2 position) : base(mapCoordinates, texture, position)
        {
            this.speed = speed;
        }

        public NectarObject GetNearestNectar(ConcurrentDictionary<Guid, NectarObject> nectarObjects)
        {
            float shortestDistance = float.MaxValue;

            NectarObject nearestNectar = null;

            if(currentTarget!= null)
            {
                return currentTarget;  
            }

            foreach (NectarObject nectarObject in nectarObjects.Values)
            {
                float distance = Vector2.Distance(nectarObject.GetMapCoordinates(), this.GetMapCoordinates());
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

        public void SetCurrentDestination(NectarObject nectar)
        {
            if (nectar != null)
            {
                currentDestination = nectar.Position;
                currentDirection = currentDestination - Position;
                currentTarget = nectar;
                currentDirection.Normalize();

                OnNectarTargeted?.Invoke(this, EventArgs.Empty);

                return;
            }

            currentDirection = Vector2.Zero;

        }

        public void Move()
        {
            //SetCurrentDestination(nectar);

            if (currentTarget == null)
            {
                return;
            }

            Position += currentDirection * speed;

            if (Vector2.Distance(Position, currentDestination) < 1f)
            {
                OnNectarPickUp?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

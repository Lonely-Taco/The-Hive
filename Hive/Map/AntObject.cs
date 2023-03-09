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
        private Destination destination;
        private NectarObject currentTarget;

        private int speed;

        public NectarObject CurrentTarget { get => currentTarget; set => currentTarget = value; }

        public event EventHandler OnNectarPickUp;

        public AntObject(int speed, Vector2 mapCoordinates, Texture2D texture, Vector2 position) : base(mapCoordinates, texture, position)
        {
            this.speed = speed;
        }

        public NectarObject GetNearestNectar(ConcurrentDictionary<Guid, NectarObject> nectarObjects)
        {
            float shortestDistance = float.MaxValue;

            NectarObject nearestNectar = null;

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

        internal void SetCurrentDestination(ConcurrentDictionary<Guid, NectarObject> nectarList)
        {
            NectarObject nectar = GetNearestNectar(nectarList);

            if (nectar != null)
            {
                currentDestination = nectar.Position;
                currentDirection = currentDestination - Position;
                currentTarget = nectar;
                currentDirection.Normalize();
            }
        }

        public void Move(ConcurrentDictionary<Guid, NectarObject> nectarList)
        {
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

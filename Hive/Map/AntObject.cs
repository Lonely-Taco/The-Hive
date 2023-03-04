using Hive.Drops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Hive.Map
{
    internal class AntObject : HiveMapObject
    {
        private Vector2 currentDestination;
        private Vector2 currentDirection;
        private Destination destination;
        private NectarObject nectar;

        private int speed;

        public NectarObject Nectar { get => nectar; set => nectar = value; }

        public event EventHandler OnNectarPickUp;

        public AntObject(int speed, Vector2 mapCoordinates, Texture2D texture, Vector2 position) : base(mapCoordinates, texture, position)
        {
            this.speed = speed;
        }

        public NectarObject GetNearestNectar(List<NectarObject> nectarObjects)
        {
            float shortestDistance = float.MaxValue;

            NectarObject nearestNectar = null;

            foreach (NectarObject nectarObject in nectarObjects)
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

            var currentPos = Position;

            currentDirection.Normalize();

            currentPos += currentDirection * speed;

            currentPos.Deconstruct(out float x, out float y);

            currentPos = new Vector2((int)x, (int)y);
            
            Position = currentPos;

            Debug.WriteLine(Position.ToString());
            Debug.WriteLine(currentDestination.ToString());
            base.Update(gameTime);
        }

        internal void SetCurrentDestination(List<NectarObject> nectarList)
        {
            NectarObject nectar = GetNearestNectar(nectarList);

            if (nectar != null)
            {
                currentDestination = nectar.Position;
            }
        }

        public void Move(List<NectarObject> nectarList)
        {
            Debug.WriteLine("moving");
            NectarObject nectar = GetNearestNectar(nectarList);

            if (nectar != null)
            {
                currentDestination = nectar.Position;
            }

            currentDirection = currentDestination - Position;

            if (Position == currentDestination)
            {
                Debug.WriteLine("there");

                this.nectar = nectar;

                if (nectarList.Contains(nectar))
                {
                    OnNectarPickUp?.Invoke(this, EventArgs.Empty);

                    SetCurrentDestination(nectarList);

                    currentDirection = currentDestination - Position;
                }
            }
        }
    }
}
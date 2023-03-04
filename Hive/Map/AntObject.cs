using Hive.Drops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Hive.Map
{
    internal class AntObject : HiveMapObject
    {
        private Vector2 currentDestination;
        private Vector2 currentDirection;

        private int speed;


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

            Position += currentDirection * speed;

            currentPos.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

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


        internal void Move(List<NectarObject> nectarList)
        {
            NectarObject nectar = GetNearestNectar(nectarList);

            if (nectar != null)
            {
                currentDestination = nectar.Position;
            }

         
                currentDirection = currentDestination - Position;
           

            if (currentDestination != Position)
            {
                return;
            }

            if (Texture.Bounds.Intersects(nectar.Texture.Bounds))
            {
                nectar.PickUp();

                if (nectarList.Contains(nectar))
                {
                    nectarList.Remove(nectar);

                    nectar.Texture.Dispose();
 
                    SetCurrentDestination(nectarList);
                    
                    currentDirection = currentDestination - Position;

                }
            }
        }
    }
}
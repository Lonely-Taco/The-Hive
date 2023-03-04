using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Hive.Map
{
    internal class AntObject : HiveMapObject
    {
        private Vector2 currentDestination;
        private int speed;

        public AntObject(int speed, Vector2 mapCoordinates, Texture2D texture, Vector2 position) : base(mapCoordinates, texture, position)
        {
            this.speed = speed;
        }

        public NectarObject GetNearestNectar(List<NectarObject> nectarObjects)
        {
            float shortestDistance = float.MaxValue;

            NectarObject nearestNectar = null;
            
            foreach(NectarObject nectarObject in nectarObjects) 
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
            throw new System.NotImplementedException();
        }

        internal void Move()
        {
            throw new NotImplementedException();
        }
    }
}
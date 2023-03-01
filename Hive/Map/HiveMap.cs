using Hive.Common;
using Hive.Drops;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Map
{
    internal class HiveMap : DrawnEntity
    {
        private List<AntObject> ants;
        private List<NectarObject> nectarList;

        private Counter antCounter;
        private Counter nectarCounter;
        private Counter expansionCounter;

        private int width;
        private int height;
        private float dropChance;

        public HiveMap(int width, int height, float dropChance, Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.width = width;
            this.height = height;
            this.dropChance = dropChance;
        }

        public void SpawnNectar()
        {
            Random rnd = new Random();
            float chance = rnd.Next(0, 1);
            if (chance <= dropChance)
            {
                Vector2 nectarCoordinates = new Vector2(rnd.Next(0, width), rnd.Next(0, height));
                NectarObject nectar = new NectarObject(nectarCoordinates, null, nectarCoordinates); //TODO: change to contain actual texture/coordinates
                nectarList.Add(nectar);
            }
        }

        public void MoveAllAnts()
        {
            foreach(AntObject ant in ants)
            {
                ant.Move();
            }
        }

        public void ClaimNectar(NectarObject nectar)
        {

        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            foreach(AntObject ant in ants)
            {
                ant.Draw(gameTime, spriteBatch);
            }

            foreach (NectarObject nectar in nectarList)
            {
                nectar.Draw(gameTime, spriteBatch);
            }
        }
    }
}

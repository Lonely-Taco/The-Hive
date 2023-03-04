using Hive.Common;
using Hive.Drops;
using Hive.Shops;
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
        private ContentLoader content;
        private List<AntObject> ants = new List<AntObject>();
        private List<NectarObject> nectarList = new List<NectarObject>();
        private HiveGame game;
        private AntShop antShop;
        private ExpansionShop expansionShop; 

        private float elapsedDropSpawnTime = 0;
        private float _dropSpawnTimeInterval = 5f;

        private int width;
        private int height;
        private float dropChance;

        private float DropSpawnTimeInterval
        {
            get
            {
                return _dropSpawnTimeInterval;
            }
            set
            {
                _dropSpawnTimeInterval = value;
                elapsedDropSpawnTime = 0;
            }
        }

        public HiveMap(int width, int height, float dropChance, Texture2D texture, Vector2 position, ContentLoader content, HiveGame game) : base(texture, position, 1f)
        {
            this.content = content;
            this.width = width;
            this.height = height;
            this.dropChance = dropChance;
            this.game = game;
            this.antShop = game.AntShop;
            this.antShop.OnBuy += AntOnBuy;
            this.expansionShop = game.ExpansionShop;
        }

        private void AntOnBuy(object sender, EventArgs e)
        {
            Console.WriteLine("Baught");
        }

        public void SpawnNectar()
        {
            Random rnd = new Random();
            float chance = rnd.Next(0, 1);
            if (chance <= dropChance)
            {
                Vector2 nectarCoordinates = new Vector2(rnd.Next((int) position.X, width + (int)position.X), rnd.Next((int) position.Y, height + (int) position.Y));
                NectarObject nectar = new NectarObject(nectarCoordinates, this.content.nectarTexture, nectarCoordinates); 
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
            if (elapsedDropSpawnTime > DropSpawnTimeInterval)
            {
                SpawnNectar();

                elapsedDropSpawnTime = 0;
            }

            elapsedDropSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
     
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (AntObject ant in ants)
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

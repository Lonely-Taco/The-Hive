using Hive.Common;
using Hive.Drops;
using Hive.Shops;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private int antSpeed = 1;
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
            SpawnAnt();
        }

        public void SpawnNectar()
        {
            Random rnd = new Random();
            float chance = rnd.Next(0, 1);
            if (chance <= dropChance)
            {
                Vector2 nectarCoordinates = new Vector2(rnd.Next((int)Position.X, width + (int)Position.X), rnd.Next((int)Position.Y, height + (int)Position.Y));
                NectarObject nectar = new NectarObject(nectarCoordinates, this.content.nectarTexture, nectarCoordinates);
                nectarList.Add(nectar);
            }
        }

        public void SpawnAnt()
        {
            Random rnd = new Random();
            var antCoordinates = new Vector2(rnd.Next((int)Position.X, width + (int)Position.X), rnd.Next((int)Position.Y, height + (int)Position.Y));
            //var antCoordinates = new Vector2(1200,600);
            AntObject antObject = new AntObject(antSpeed, antCoordinates, content.antTexture, antCoordinates);

            antObject.OnNectarPickUp += AntOnNectarPickUp;
            ants.Add(antObject);
        }

        private void AntOnNectarPickUp(object sender, EventArgs e)
        {
            AntObject ant = (AntObject) sender;

            nectarList.Remove(ant.Nectar);
        }

        public void MoveAllAnts(List<NectarObject> nectarList)
        {
            for (int i = ants.Count() - 1; i >= 0; i--)
            {
                ants[i].Move(nectarList);   
            }
        }


        public override void Update(GameTime gameTime)
        {
            if (elapsedDropSpawnTime > DropSpawnTimeInterval)
            {
                SpawnNectar();

                elapsedDropSpawnTime = 0;
            }

            elapsedDropSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            MoveAllAnts(nectarList);

            foreach (var ant in ants)
            {
                ant.Update(gameTime);
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle? rectangle)
        {
            base.Draw(gameTime, spriteBatch, rectangle);

            spriteBatch.DrawString(content.counterFont, nectarList.Count().ToString(), new Vector2(500,500), Color.Black, 0f, Vector2.Zero, scale * 5, SpriteEffects.None, 1);

            foreach (AntObject ant in ants)
            {
                ant.Draw(gameTime, spriteBatch, null);
            }

            foreach (NectarObject nectar in nectarList)
            {
                nectar.Draw(gameTime, spriteBatch, null);
            }
        }
    }
}

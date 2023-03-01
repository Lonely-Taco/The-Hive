using Hive.Map;
using Hive.Shop;
using Hive.Drops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Hive.Common;
using Hive.Utility;

namespace Hive
{
    public class HiveGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<IEntity> entities = new List<IEntity>();

        private ContentLoader contentLoader;
        private Counter antCounter;
        private Counter nectarCounter;
        private AntShop antShop;
        private ExpansionShop expansionShop;
        private HiveMap hiveMap;
        private DropManager dropManager;

        public HiveGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            entities = new List<IEntity>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            contentLoader = new ContentLoader(this);

            this.antCounter = new Counter(contentLoader.counterTexture, Vector2.One);
            entities.Add(antCounter);

            this.nectarCounter = new Counter(contentLoader.counterTexture, Vector2.One);
            entities.Add(nectarCounter);

            this.antShop = new AntShop(contentLoader.antTexture, antCounter, nectarCounter);
            entities.Add(antShop);

            this.expansionShop = new ExpansionShop(contentLoader.nectarTexture, antCounter, nectarCounter);
            entities.Add(expansionShop);

            this.hiveMap = new HiveMap(100, 100, 0.05f, contentLoader.mapTexture, Vector2.One, contentLoader);
            entities.Add(hiveMap);

            this.dropManager = new DropManager(nectarCounter);
            entities.Add(dropManager);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            foreach (IEntity entity in entities)
            {
                entity.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            foreach (IEntity entity in entities)
            {
                entity.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
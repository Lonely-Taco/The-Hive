using Hive.Map;
using Hive.Shops;
using Hive.Drops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Hive.Common;
using Hive.Utility;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

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

        public static int screenSizeX = 720;
        public static int screenSizeY = 480;

        public HiveGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = screenSizeX;
            _graphics.PreferredBackBufferHeight = screenSizeY;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            contentLoader = new ContentLoader(this);

            Shop.Initialize(contentLoader.counterFont, contentLoader.buyButtonTexture, contentLoader.nectarTexture, contentLoader.shopBackgroundTexture);
            Counter.Initialize(contentLoader.backgroundTexture, contentLoader.counterFont);

            this.antCounter = new Counter(new Vector2(10, 100));
            entities.Add(antCounter);

            this.nectarCounter = new Counter(Vector2.One);
            entities.Add(nectarCounter);

            this.antShop = new AntShop(contentLoader.antTexture, antCounter, nectarCounter, new Vector2(50, 50));
            entities.Add(antShop);

            //this.expansionShop = new ExpansionShop(contentLoader.nectarTexture, antCounter, nectarCounter);
            //entities.Add(expansionShop);

            //this.hiveMap = new HiveMap(100, 100, 0.05f, contentLoader.mapTexture, Vector2.One, contentLoader);
            //entities.Add(hiveMap);

            this.dropManager = new DropManager(nectarCounter, contentLoader.nectarTexture);
            entities.Add(dropManager);

            // TODO: use this.Content to load your game content here
        }

        public void Quit()
        {
            this.Exit();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Quit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            InputManager.Update(gameTime);

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
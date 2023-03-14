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
using System.Diagnostics;
using Hive.GameStates;

namespace Hive
{
    public class HiveGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch           _spriteBatch;

        private List<State> states = new();

        private ContentLoader contentLoader;
        private Counter       antCounter;
        private Counter       nectarCounter;
        private Counter       expansionCounter;
        private AntShop       antShop;
        private ExpansionShop expansionShop;
        private HiveMap       hiveMap;
        private DropManager   dropManager;

        private State currentState;
        private State nextState;

        private MenuState menuState;
        private GameState gameState;

        public MenuState MenuState => menuState;

        public GameState GameState => gameState;

        public static int screenSizeX = 1300;
        public static int screenSizeY = 730;

        public ExpansionShop ExpansionShop
        {
            get => expansionShop;
        }

        public AntShop AntShop
        {
            get => antShop;
        }

        public HiveGame()
        {
            _graphics             = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible        = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            _graphics.IsFullScreen              = false;
            _graphics.PreferredBackBufferWidth  = screenSizeX;
            _graphics.PreferredBackBufferHeight = screenSizeY;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch  = new SpriteBatch(GraphicsDevice);
            contentLoader = new ContentLoader(this);

            Shop.Initialize(contentLoader.counterFont,
                            contentLoader.buyButtonTexture,
                            contentLoader.nectarTexture,
                            contentLoader.containerTexture,
                            contentLoader.costBackgroundTexture);

            Counter.Initialize(contentLoader.containerTexture, contentLoader.counterFont);
            EventText.Initialize(contentLoader.counterFont);

            MenuState.Initialize(
                contentLoader.counterFont,
                contentLoader.menuButtonTexture,
                contentLoader.menuBackgroundTexture
            );

            this.antCounter = new Counter(new Vector2(0, 0), contentLoader.antTexture, 1f);
            this.nectarCounter = new Counter(new Vector2(0, 57), contentLoader.nectarTexture, 1f);
            this.expansionCounter = new Counter(new Vector2(976, 175), contentLoader.expansionIconTexture, 1f);
            this.antShop = new AntShop(contentLoader.antTexture, antCounter, nectarCounter, new Vector2(0, 170));
            this.expansionShop = new ExpansionShop(contentLoader.expansionIconTexture, expansionCounter, nectarCounter,
                                                   new Vector2(0, 210));
            this.hiveMap = new HiveMap(512, 512, 0.5f, contentLoader.mapTexture,
                                       new Vector2(788, 218), contentLoader, this);
            this.dropManager = new DropManager(nectarCounter, contentLoader.nectarTexture);

            gameState = new GameState(new Vector2(0, 0), 1f,
                                      new List<IEntity>
                                      {
                                          this.antCounter,
                                          this.nectarCounter,
                                          this.expansionCounter,
                                          this.antShop,
                                          this.expansionShop,
                                          this.hiveMap,
                                          this.dropManager,
                                      },
                                      game: this
            );

            this.menuState = new MenuState(new Vector2(150, 150), 1, new List<IEntity>(), game: this);

            currentState = menuState;
            
            this.states.Add(menuState);
            this.states.Add(gameState);
        }

        private void Quit()
        {
            this.Exit();
        }

        private void UpdateState(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState    = null;
            }

            currentState.Update(gameTime);
            base.Update(gameTime);
        }

       public void ChangeState(State state)
        {
            nextState = state;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Quit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            InputManager.Update(gameTime);
            
            currentState.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            UpdateState(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            currentState.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
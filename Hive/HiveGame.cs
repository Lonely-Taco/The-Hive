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
using System.IO;
using Hive.GameStates;

namespace Hive
{
    public class HiveGame : Game
    {
        #region Fields

        private GraphicsDeviceManager _graphics;
        private SpriteBatch           _spriteBatch;
        private ContentLoader         contentLoader;
        private Counter               antCounter;
        private Counter               nectarCounter;
        private Counter               expansionCounter;
        private AntShop               antShop;
        private ExpansionShop         expansionShop;
        private HiveMap               hiveMap;
        private DropManager           dropManager;
        private State                 currentState;
        private State                 nextState;
        private State                 previousState;
        private MenuState             menuState;
        private GameState             gameState;
        private SettingsState         settingState;
        private SettingData           settings;
        public static int screenSizeX = 1300;
        public static int screenSizeY = 730;

        #endregion

        #region Properties

        public MenuState MenuState => menuState;

        public GameState GameState => gameState;
        public SettingsState SettingState => settingState;

        public int ScreenSizeX => screenSizeX;

        public int ScreenSizeY => screenSizeY;

        public ExpansionShop ExpansionShop => expansionShop;

        public AntShop AntShop => antShop;

        #endregion


        public HiveGame()
        {
            _graphics             = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible        = true;
            settings              = new SettingData(3, Color.Black);
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

            State.Initialize(
                contentLoader.counterFont,
                contentLoader.menuButtonTexture,
                contentLoader.menuBackgroundTexture,
                contentLoader.menuSceneBackgroundTexture
            );

            antCounter       = new Counter(new Vector2(0, 0), contentLoader.antTexture, 1f);
            nectarCounter    = new Counter(new Vector2(0, 57), contentLoader.nectarTexture, 1f);
            expansionCounter = new Counter(new Vector2(976, 175), contentLoader.expansionIconTexture, 1f);
            antShop          = new AntShop(contentLoader.antTexture, antCounter, nectarCounter, new Vector2(0, 170));
            expansionShop = new ExpansionShop(contentLoader.expansionIconTexture, expansionCounter, nectarCounter,
                                              new Vector2(0, 210));
            hiveMap = new HiveMap(512, 512, 0.5f, contentLoader.mapTexture,
                                  new Vector2(788, 218), contentLoader, this, nectarCounter, expansionCounter);
            dropManager = new DropManager(nectarCounter, contentLoader.nectarTexture);

            List<IEntity> gameStateEntityList = 
                new List<IEntity>
                {
                    this.antCounter,
                    this.nectarCounter,
                    this.expansionCounter,
                    this.antShop,
                    this.expansionShop,
                    this.hiveMap,
                    this.dropManager,
                };

            this.gameState = new GameState(new Vector2(0, 0), 1f, gameStateEntityList, this, contentLoader.settingsButtonTexture);
            this.menuState    = new MenuState(new Vector2(150, 150), 1, new List<IEntity>(), game: this);
            this.settingState = new SettingsState(new Vector2(150, 150), 1, new List<IEntity>(), game: this, settings);

            currentState = menuState;
        }

        internal void Quit()
        {
            this.Exit();
        }

        private void Pause()
        {
            if (currentState == gameState)
            {
                ChangeState(menuState);
            }
        }

        private void UpdateState(GameTime gameTime)
        {
            if (nextState == null)
                return;

            currentState = nextState;
            nextState    = null;
        }

        public void ChangeState(State state, SettingData settings)
        {
            ChangeState(state);
            settingState.SetSettings(settings);
            hiveMap.SetSettings(settings);
        }
        public void ChangeState(State state)
        {
            previousState = currentState;
            nextState = state;

            if (previousState == SettingState)
            {
                nextState = menuState;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Pause();

            // TODO: Add your update logic here

            InputManager.Update(gameTime);
            UpdateState(gameTime);
            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            currentState.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
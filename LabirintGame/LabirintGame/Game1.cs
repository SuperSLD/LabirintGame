using Labirint;
using LabirintGame.Classes;
using LabirintGame.Labirint;
using LabirintGame.LabirintClasses;
using LabirintGame.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using GameWindow = LabirintGame.Windows.GameWindow;

namespace LabirintGame {
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TextWriter textWriter;

        List<Window> windows = new List<Window>();
        public static int state = 1;

        TextureManager textureManager;

        public static float SCREEN_WIDTH;
        public static float SCREEN_HEIGHT;
        public static int TILE_SIZE;
        public static int USER_ID = 0;
        public static bool EXIT = false;
        public static bool HOST = false;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Инициализация любых переменных.
        /// </summary>
        protected override void Initialize() {
            SCREEN_WIDTH = Window.ClientBounds.Width;
            SCREEN_HEIGHT = Window.ClientBounds.Height;
            TILE_SIZE = Window.ClientBounds.Height / 20;

            windows.Add(new GameWindow());
            windows.Add(new MenuWindow());
            windows.Add(new RestartWindow());

            foreach (Window window in windows) {
                window.Initialize();
            }
            base.Initialize();

        }

        /// <summary>
        /// Загрузка ресурсов (текстуры, звуки, шрифты)
        /// </summary>
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureManager = new TextureManager();
            textWriter = new TextWriter(textureManager);

            foreach (Window window in windows) {
                window.LoadContent(textureManager, spriteBatch, textWriter);
            }
            textureManager.AddTexture("backgraund1", Content.Load<Texture2D>("standart_backgraund2"));
            textureManager.AddTexture("testu", Content.Load<Texture2D>("testUser")); 
            textureManager.AddTexture("blok_standart", Content.Load<Texture2D>("blok_standart_sprayt3"));
            textureManager.AddTexture("blok_standart4", Content.Load<Texture2D>("blok_standart_sprayt4"));
            textureManager.AddTexture("blok_standart2", Content.Load<Texture2D>("blok_standart_sprayt2"));
            textureManager.AddTexture("user_standart1_1", Content.Load<Texture2D>("user_standart1_1"));
            textureManager.AddTexture("user_standart1_2", Content.Load<Texture2D>("user_standart1_2"));
            textureManager.AddTexture("user_standart2_1", Content.Load<Texture2D>("user_standart2_1"));
            textureManager.AddTexture("user_standart2_2", Content.Load<Texture2D>("user_standart2_2"));
            textureManager.AddTexture("user_standart3_1", Content.Load<Texture2D>("user_standart3_1"));
            textureManager.AddTexture("user_standart3_2", Content.Load<Texture2D>("user_standart3_2"));
            textureManager.AddTexture("user_standart4_1", Content.Load<Texture2D>("user_standart4_1"));
            textureManager.AddTexture("user_standart4_2", Content.Load<Texture2D>("user_standart4_2"));
            textureManager.AddTexture("backgraund2", Content.Load<Texture2D>("standart_backgraund1"));
            textureManager.AddTexture("logo", Content.Load<Texture2D>("labirint_game"));
            textureManager.AddTexture("play", Content.Load<Texture2D>("menu_button_play"));
            textureManager.AddTexture("online", Content.Load<Texture2D>("menu_button_online"));
            textureManager.AddTexture("exit", Content.Load<Texture2D>("menu_button_exit"));
            textureManager.AddTexture("border", Content.Load<Texture2D>("menu_button_r"));
            textureManager.AddTexture("object_exit", Content.Load<Texture2D>("object_exit"));
            textureManager.AddTexture("menu", Content.Load<Texture2D>("menu_button_menu"));
            textureManager.AddTexture("restart", Content.Load<Texture2D>("menu_button_restart"));

            textureManager.AddTexture("num_0", Content.Load<Texture2D>("Numbers/num_0"));
            textureManager.AddTexture("num_1", Content.Load<Texture2D>("Numbers/num_1"));
            textureManager.AddTexture("num_2", Content.Load<Texture2D>("Numbers/num_2"));
            textureManager.AddTexture("num_3", Content.Load<Texture2D>("Numbers/num_3"));
            textureManager.AddTexture("num_4", Content.Load<Texture2D>("Numbers/num_4"));
            textureManager.AddTexture("num_5", Content.Load<Texture2D>("Numbers/num_5"));
            textureManager.AddTexture("num_6", Content.Load<Texture2D>("Numbers/num_6"));
            textureManager.AddTexture("num_7", Content.Load<Texture2D>("Numbers/num_7"));
            textureManager.AddTexture("num_8", Content.Load<Texture2D>("Numbers/num_8"));
            textureManager.AddTexture("num_9", Content.Load<Texture2D>("Numbers/num_9"));

            textureManager.AddTexture("object_flag", Content.Load<Texture2D>("object_flag"));
            textureManager.AddTexture("object_flagbox", Content.Load<Texture2D>("object_flagbox"));

            WebSocketConnection.Connect();
        }

        /// <summary>
        /// Хз зачем он, трогать лучше не стоит.
        /// 
        /// Возможно для отчистки памяти после завершения.
        /// </summary>
        protected override void UnloadContent() { }

        /// <summary>
        /// Обновление всей игровой логики за исключением
        /// обновления экрана
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                && (state == 1 && MenuWindow.b == 2)) {
                EXIT = true;
                WebSocketConnection.Close();
                Exit();
            }

            for (int i = 0; i < windows.Count; i++) {
                if (state == i) windows[i].Update();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Этот метод отвечает за обновление графики.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            for (int i = 0; i < windows.Count; i++) {
                if (state == i) windows[i].Draw();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
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
using System.Threading;

namespace LabirintGame.Windows {

    class GameWindow : Window {

        public const int LABIRINT_SIZE = 400;

        static Map map;
        static User user;
        int SEED = new Random().Next();

        /// <summary>
        /// Самый обычный пустой конструктор.
        /// </summary>
        public GameWindow() 
            : base() {
        }

        /// <summary>
        /// Инициализация.
        /// </summary>
        public override void Initialize() {
            map = new Map(SEED);
            map.LabirintGenerate(LABIRINT_SIZE);
            user = new User(LABIRINT_SIZE);
            map.AddUser(user);
        }

        /// <summary>
        /// Загрузка содержимого.
        /// </summary>
        /// <param name="textureManager"></param>
        /// <param name="batch"></param>
        public override void LoadContent(TextureManager textureManager, SpriteBatch batch) {
            this.spriteBatch = batch;
            this.textureManager = textureManager;
        }

        Thread updateThread = new Thread(UpdateThread);

        /// <summary>
        /// Обновление логики.
        /// </summary>
        public override void Update() {
            if (!updateThread.IsAlive) updateThread.Start();

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A)) user.Move(1, map.GetLabirint());
            if (keyboardState.IsKeyDown(Keys.D)) user.Move(3, map.GetLabirint());
            if (keyboardState.IsKeyDown(Keys.S)) user.Move(4, map.GetLabirint());
            if (keyboardState.IsKeyDown(Keys.W)) user.Move(2, map.GetLabirint());
            if (keyboardState.IsKeyDown(Keys.Escape)) Game1.state = 1;
        }

        int L; double windowK; int windowX; int windowY;

        /// <summary>
        /// Перерисовка окна.
        /// </summary>
        public override void Draw() {
            int[,] labirint = map.GetLabirint();
            L = LABIRINT_SIZE % 2 == 0 ? LABIRINT_SIZE + 1 : LABIRINT_SIZE;
            windowK = (double) (Game1.TILE_SIZE / (double)1000);
            windowX = (int) ((Game1.SCREEN_WIDTH / 2 - user.GetSize()/2 * windowK) - user.GetX() * windowK);
            windowY = (int) ((Game1.SCREEN_HEIGHT / 2 - user.GetSize()/2 * windowK) - (user.GetY()) * windowK);

            for (int x = user.GetTileX() - 30; x < user.GetTileX() + 30; x++) {
                for (int y = user.GetTileY() + 12; y >= user.GetTileY() - 12; y--) {
                    if (x >= 0 && x <= L - 1 && y >= 0 && y <= L - 1) {
                        switch (labirint[x, y]) {
                            case 0:
                                DrawTile("backgraund1", x, y);
                                break;
                            case 1:
                                DrawTile("blok_standart", x, y);
                                break;
                            case 3:
                                DrawTile("blok_standart4", x, y);
                                break;
                            case 5:
                                DrawTile("blok_standart2", x, y);
                                break;
                            case 4:
                                DrawTile("backgraund2", x, y);
                                break;
                        }
                    } else {
                        DrawTile("blok_standart4", x, y);
                    }
                }
            }

            user.Draw(spriteBatch, textureManager, windowK);
        }

        /// <summary>
        /// Отрисовка плитки в заданных координатах относительно игрока.
        /// </summary>
        /// <param name="code">Код текстуры.</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawTile(string code, int x, int y) {
            spriteBatch.Draw(textureManager.GetTexture2D(code),
                                    new Rectangle(
                                        x * Game1.TILE_SIZE + windowX,
                                        y * Game1.TILE_SIZE + windowY,
                                        Game1.TILE_SIZE,
                                        Game1.TILE_SIZE),
                                    Color.AliceBlue);
        }

       
        private static void UpdateThread() {
            while (!Game1.EXIT) {
                map.SendInfo();
                Thread.Sleep(100);
            }
        }
    }
}

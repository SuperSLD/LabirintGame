using Labirint;
using LabirintGame.Classes;
using LabirintGame.Labirint;
using LabirintGame.LabirintClasses;
using LabirintGame.MapClasses;
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
        static Dictionary<string, User> list;

        static int SEED = new Random().Next();
        private static Thread updateThread;
        private static bool objectUpdate = false;

        /// <summary>
        /// Самый обычный пустой конструктор.
        /// </summary>
        public GameWindow() : base() {

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
        public override void LoadContent(TextureManager textureManager, SpriteBatch batch, TextWriter textWriter) {
            this.spriteBatch = batch;
            this.textureManager = textureManager;
            this.textWriter = textWriter;
            updateThread = new Thread(UpdateThread);
            updateThread.Start();
            new Thread(SocketReadThread).Start();
            new Thread(SocketSendThread).Start();

        }

        /// <summary>
        /// Обновление логики.
        /// </summary>
        public override void Update() {

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A))      user.Move(1, map.GetLabirint());
            if (keyboardState.IsKeyDown(Keys.D))      user.Move(3, map.GetLabirint());
            if (keyboardState.IsKeyDown(Keys.S))      user.Move(4, map.GetLabirint());
            if (keyboardState.IsKeyDown(Keys.W))      user.Move(2, map.GetLabirint());
            if (keyboardState.IsKeyDown(Keys.Space))  map.AddFlag();
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
            foreach (MapObject mapObject in map.GetObjects()) {
                if (mapObject.GetTileX() >= user.GetTileX() - 30
                    && mapObject.GetTileX() <= user.GetTileX() + 30
                    && mapObject.GetTileY() >= user.GetTileY() - 12
                    && mapObject.GetTileY() <= user.GetTileY() + 12) {
                    mapObject.Draw(spriteBatch, textureManager, windowK, windowX, windowY);
                }
            }

            foreach (String key in list.Keys) {
                list[key].Draw(spriteBatch, textureManager, windowK , windowX, windowY);
            }
            user.Draw(spriteBatch, textureManager, windowK);

            spriteBatch.Draw(textureManager.GetTexture2D("object_flagbox"),
                                    new Rectangle(
                                        1 * Game1.TILE_SIZE,
                                        1 * Game1.TILE_SIZE,
                                        Game1.TILE_SIZE*2,
                                        Game1.TILE_SIZE*2),
                                    Color.AliceBlue);
            textWriter.DrawText(spriteBatch, user.GetFlags().ToString(), 1 * Game1.TILE_SIZE, 2 * Game1.TILE_SIZE, Game1.TILE_SIZE);
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

       /// <summary>
       /// Поток обновления объектов.
       /// </summary>
        private static void UpdateThread() {
            while (!Game1.EXIT) if (Game1.state == 0) {
                    map.SendInfo();
                    foreach (String key in list.Keys) {
                        list[key].Update(null, null, null);
                    }
                    Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Перезапск на основе случайного значения.
        /// </summary>
        public static void Restart() {
            SEED = new Random().Next();
            map = new Map(SEED);
            map.LabirintGenerate(LABIRINT_SIZE);
            user = new User(LABIRINT_SIZE);
            list = new Dictionary<string, User>();
            map.AddUser(user);
            objectUpdate = false;
        }

        /// <summary>
        /// Перезапуск на основе заданного значения.
        /// </summary>
        /// <param name="seed"></param>
        public static void Restart(int seed) {
            map = new Map(seed);
            map.LabirintGenerate(LABIRINT_SIZE);
            user = new User(LABIRINT_SIZE);
            map.AddUser(user);
            list = new Dictionary<string, User>();

            objectUpdate = false;
        }

        /// <summary>
        /// Поток получения сообщений.
        /// </summary>
        private void SocketSendThread() {
            int x1 = 0; int y1 = 0;
            while (!Game1.EXIT) {
                if (Game1.ONLINE) {
                    Thread.Sleep(10);
                    // TODO: Получить список объектов.
                    if (!objectUpdate) {
                        objectUpdate = true;
                        WebSocketConnection.SendString("sendobjectinfo<!>0");
                    }
                    if (x1 != user.GetX() || y1 != user.GetY()) {
                        WebSocketConnection.SendString("sendxyn<!>" + user.GetX() + "<!>" + user.GetY() + "<!>"
                             + user.GetN() + "<!>");
                    }
                    x1 = user.GetX(); y1 = user.GetY();
                }
            }
        }

        /// <summary>
        /// Прием сообщений с сервера.
        /// </summary>
        private void SocketReadThread() {
            while (!Game1.EXIT) {
                if (Game1.ONLINE) {
                    try {
                        string message = WebSocketConnection.ReceiveMessage().Result;
                        Console.WriteLine("SocketReadThread : " + message);
                        string[] mes = message.Split('&');
                        if (mes[0] == "xyn") {
                            try {
                                list[mes[4]].SetX(Convert.ToInt32(mes[1]));
                                list[mes[4]].SetY(Convert.ToInt32(mes[2]));
                                list[mes[4]].SetN(Convert.ToInt32(mes[3]));
                            } catch (Exception) {
                                list.Add(mes[4], new User(LABIRINT_SIZE));
                                Console.WriteLine("connect user id: " + mes[4]);
                            }
                        } else if (mes[0] == "addflag") {
                            map.AddFlag(Convert.ToInt32(mes[1]), Convert.ToInt32(mes[2]));
                        }
                    } catch (Exception) { }
                }
            }
        }
    }
}

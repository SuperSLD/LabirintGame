using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LabirintGame.Classes;
using LabirintGame.LabirintClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LabirintGame.Windows {
    class MenuWindow : Window {

        public static int b = 0;

        public override void Initialize() {
            
        }

        /// <summary>
        /// Загрузка содержимого.
        /// </summary>
        /// <param name="textureManager"></param>
        /// <param name="batch"></param>
        public override void LoadContent(TextureManager textureManager, SpriteBatch batch, TextWriter textWriter) {
            this.spriteBatch = batch;
            this.textureManager = textureManager;

            new Thread(Timer).Start();
        }

        private bool threadStart = false;
        private static bool touched = false;
        /// <summary>
        /// Обновление логики.
        /// </summary>
        public override void Update() {
            KeyboardState keyboardState = Keyboard.GetState();
            Game1.ONLINE = false;
            if (keyboardState.IsKeyDown(Keys.W) && !touched) {
                b--;
                touched = true;
            }
            if (keyboardState.IsKeyDown(Keys.S) && !touched) {
                b++;
                touched = true;
            }
            if (keyboardState.IsKeyDown(Keys.Enter)) {
                switch (b) {
                    case 0:
                        GameWindow.Restart();
                        Game1.state = 0;
                        break;
                    case 1:
                        if (!threadStart) {
                            threadStart = true;
                            new Thread(SeedThread).Start();
                        }
                        break;
                    case 2:
                        break;
                }
            }
            
            b = b < 0 ? 0 : b;
            b = b > 2 ? 2 : b;
        }

        private static void Timer() {
            while (!Game1.EXIT) {
                if (touched) {
                    Thread.Sleep(100);
                    touched = false;
                }
            }
        }

        /// <summary>
        /// Отрисовка.
        /// </summary>
        public override void Draw() {
            Random rand = new Random(1488);

            for (int x = -30; x < 30; x++) {
                for (int y = -12; y < 12; y++) {
                    if (x <= -9 || x >= 9) {
                        int i = rand.Next(1, 4);
                        switch (i) {
                            case 1:
                                spriteBatch.Draw(
                                    textureManager.GetTexture2D("blok_standart"), new Rectangle(
                                    (int)(Game1.SCREEN_WIDTH / 2 + x * Game1.TILE_SIZE),
                                    (int)(Game1.SCREEN_HEIGHT / 2 + y * Game1.TILE_SIZE),
                                    Game1.TILE_SIZE,
                                    Game1.TILE_SIZE),
                                    Color.AliceBlue);
                                break;
                            case 2:
                                spriteBatch.Draw(
                                    textureManager.GetTexture2D("blok_standart4"), new Rectangle(
                                    (int)(Game1.SCREEN_WIDTH / 2 + x * Game1.TILE_SIZE),
                                    (int)(Game1.SCREEN_HEIGHT / 2 + y * Game1.TILE_SIZE),
                                    Game1.TILE_SIZE,
                                    Game1.TILE_SIZE),
                                    Color.AliceBlue);
                                break;
                            case 3:
                                spriteBatch.Draw(
                                    textureManager.GetTexture2D("blok_standart2"), new Rectangle(
                                    (int)(Game1.SCREEN_WIDTH / 2 + x * Game1.TILE_SIZE),
                                    (int)(Game1.SCREEN_HEIGHT / 2 + y * Game1.TILE_SIZE),
                                    Game1.TILE_SIZE,
                                    Game1.TILE_SIZE),
                                    Color.AliceBlue);
                                break;
                        }
                    } else {
                        int i = rand.Next(1, 3);
                        switch (i) {
                            case 1:
                                spriteBatch.Draw(
                                    textureManager.GetTexture2D("backgraund1"), new Rectangle(
                                    (int)(Game1.SCREEN_WIDTH / 2 + x * Game1.TILE_SIZE),
                                    (int)(Game1.SCREEN_HEIGHT / 2 + y * Game1.TILE_SIZE),
                                    Game1.TILE_SIZE,
                                    Game1.TILE_SIZE),
                                    Color.AliceBlue);
                                break;
                            case 2:
                                spriteBatch.Draw(
                                    textureManager.GetTexture2D("backgraund2"), new Rectangle(
                                    (int)(Game1.SCREEN_WIDTH / 2 + x * Game1.TILE_SIZE),
                                    (int)(Game1.SCREEN_HEIGHT / 2 + y * Game1.TILE_SIZE),
                                    Game1.TILE_SIZE,
                                    Game1.TILE_SIZE),
                                    Color.AliceBlue);
                                break;
                        }
                    }
                }
            }

            
            spriteBatch.Draw(textureManager.GetTexture2D("logo"), new Rectangle(
                                (int)(Game1.SCREEN_WIDTH / 2 - 6 * Game1.TILE_SIZE),
                                (int)(2 * Game1.TILE_SIZE),
                                Game1.TILE_SIZE * 12,
                                Game1.TILE_SIZE * 3),
                                Color.AliceBlue);


            spriteBatch.Draw(textureManager.GetTexture2D("play"), new Rectangle(
                                (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                (int)(6 * Game1.TILE_SIZE),
                                Game1.TILE_SIZE * 4,
                                Game1.TILE_SIZE * 4),
                                Color.AliceBlue);
            if (b == 0)
            spriteBatch.Draw(textureManager.GetTexture2D("border"), new Rectangle(
                                            (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                            (int)(6 * Game1.TILE_SIZE),
                                            Game1.TILE_SIZE * 4,
                                            Game1.TILE_SIZE * 4),
                                            Color.AliceBlue);

            spriteBatch.Draw(textureManager.GetTexture2D("online"), new Rectangle(
                                (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                (int)(11 * Game1.TILE_SIZE),
                                Game1.TILE_SIZE * 4,
                                Game1.TILE_SIZE * 4),
                                Color.AliceBlue);
            if (b == 1)
            spriteBatch.Draw(textureManager.GetTexture2D("border"), new Rectangle(
                            (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                            (int)(11 * Game1.TILE_SIZE),
                            Game1.TILE_SIZE * 4,
                            Game1.TILE_SIZE * 4),
                            Color.AliceBlue);
            
            spriteBatch.Draw(textureManager.GetTexture2D("exit"), new Rectangle(
                                (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                (int)(16 * Game1.TILE_SIZE),
                                Game1.TILE_SIZE * 4,
                                Game1.TILE_SIZE * 2),
                                Color.AliceBlue);
            if (b == 2)
            spriteBatch.Draw(textureManager.GetTexture2D("border"), new Rectangle(
                                (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                (int)(16 * Game1.TILE_SIZE),
                                Game1.TILE_SIZE * 4,
                                Game1.TILE_SIZE * 2),
                                Color.AliceBlue);
        }

        private void SeedThread() {
            WebSocketConnection.SendString("getseed<!>0");
            Console.WriteLine("send: getseed");
            Thread.Sleep(100);
            string message = "0&0";
            while (!Game1.EXIT) {
                message = WebSocketConnection.ReceiveMessage().Result;
                if (message == null) {
                    message = "0&0";
                }
                if (message.Split('&')[0].Equals("seed")) {
                    GameWindow.Restart(Convert.ToInt32(message.Split('&')[1]));
                    Game1.state = 0;
                    Game1.ONLINE = true;
                    threadStart = false;
                    return;
                }
            }
        }
    }
}

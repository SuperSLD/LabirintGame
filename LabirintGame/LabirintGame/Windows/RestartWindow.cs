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
    class RestartWindow : Window {

        private int b = 0;

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
        }

        /// <summary>
        /// Обновление логики.
        /// </summary>
        public override void Update() {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W)) b = 0;
            if (keyboardState.IsKeyDown(Keys.S)) b = 1;

            if (keyboardState.IsKeyDown(Keys.Enter)) {
                switch (b) {
                    case 0:
                        GameWindow.Restart();
                        Game1.state = 0;
                        break;
                    case 1:
                        // TODO: Загнать паузу в отдельный поток.
                        Game1.state = 1;
                        Thread.Sleep(200);
                        break;
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
                    if (x <= -4 || x >= 3 || y <= -4 || y >= 5) {
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
            //ПРОДОЛЖЕНИЕ ОТРИСОВКИ  
            spriteBatch.Draw(textureManager.GetTexture2D("restart"), new Rectangle(
                                (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                (int)(8 * Game1.TILE_SIZE),
                                Game1.TILE_SIZE * 4,
                                Game1.TILE_SIZE * 2),
                                Color.AliceBlue);
            if (b == 0)
                spriteBatch.Draw(textureManager.GetTexture2D("border"), new Rectangle(
                                    (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                    (int)(8 * Game1.TILE_SIZE),
                                    Game1.TILE_SIZE * 4,
                                    Game1.TILE_SIZE * 2),
                                    Color.AliceBlue);

            spriteBatch.Draw(textureManager.GetTexture2D("menu"), new Rectangle(
                                (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                (int)(12 * Game1.TILE_SIZE),
                                Game1.TILE_SIZE * 4,
                                Game1.TILE_SIZE * 2),
                                Color.AliceBlue);
            if (b == 1)
                spriteBatch.Draw(textureManager.GetTexture2D("border"), new Rectangle(
                                    (int)(Game1.SCREEN_WIDTH / 2 - 2 * Game1.TILE_SIZE),
                                    (int)(12 * Game1.TILE_SIZE),
                                    Game1.TILE_SIZE * 4,
                                    Game1.TILE_SIZE * 2),
                                    Color.AliceBlue);
        }
    }
}

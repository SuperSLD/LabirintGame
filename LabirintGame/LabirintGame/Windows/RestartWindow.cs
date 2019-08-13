using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabirintGame.LabirintClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabirintGame.Windows {
    class RestartWindow : Window {
        
        public override void Initialize() {
            
        }

        /// <summary>
        /// Загрузка содержимого.
        /// </summary>
        /// <param name="textureManager"></param>
        /// <param name="batch"></param>
        public override void LoadContent(TextureManager textureManager, SpriteBatch batch) {
            
        }

        /// <summary>
        /// Обновление логики.
        /// </summary>
        public override void Update() {

        }

        /// <summary>
        /// Отрисовка.
        /// </summary>
        public override void Draw() {
            Random rand = new Random(1488);

            for (int x = -30; x < 30; x++) {
                for (int y = -12; y < 12; y++) {
                    if ((x <= -9 || x >= 9) && (y <= -5 || y >= 5)) {
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
            //РОДОЛЖЕНИЕ ОТРИСОВКИ  
        }
    }
}

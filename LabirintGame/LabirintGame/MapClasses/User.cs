using Labirint;
using LabirintGame.Classes;
using LabirintGame.LabirintClasses;
using LabirintGame.MapClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintGame.Labirint {

    public class User : Observer {
        private int x;
        private int y;
        private int tileX;
        private int tileY;

        private int L;

        private int size;
        private int speed;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="L">Размер лабиринта</param>
        public User(int L) {
            this.L = L % 2 == 0 ? L + 1 : L;
            this.tileX = 1;
            this.tileY = L - 1;
            this.x = this.tileX * 1000;
            this.y = this.tileY * 1000;

            this.size = 700;
            this.speed = 80;
        }

        /// <summary>
        /// Обновление пользователя.
        /// </summary>
        public void Update(User user, int[,] labirint, List<MapObject> objects) {
            if (textCode == 2) {
                textCode = 1;
            } else {
                textCode = 2;
            }
        }

        int mov = 4;
        int textCode = 1;

        /// <summary>
        /// Передвижение игрока в зависимости от нажатых клавишь.
        /// 
        /// (Копипаста с java проекта)
        /// </summary>
        /// <param name="n">
        /// Направление движения.
        /// 
        /// 1 - движение влево.
        /// 2 - движение вверх.
        /// 3 - движение вправо.
        /// 4 - движение вниз.
        /// 
        /// </param>
        /// <param name="map">Лабиринт.</param>
        public void Move(int n, int[,] labirint) {

            if (n == 1) {
                x -= speed;
                mov = 1;
            }
            if (n == 2) {
                y -= speed;
                mov = 2;
            }
            if (n == 3) {
                x += speed;
                mov = 3;
            }
            if (n == 4) {
                y += speed;
                mov = 4;
            }

            tileX = x / 1000;
            tileY = y / 1000;

            // Проверка пересечений по Y
            if ((tileY+1)*1000 - y <= speed + 10)
            if ((labirint[tileX, tileY] % 2 == 1)
                || (labirint[tileX + 1, tileY] % 2 == 1
                && (tileX + 1) * 1000 < x + size)) {
                y = (tileY + 1) * 1000;
            }

            if (y + size - (tileY+1)*1000 <= speed + 10)
            if ((labirint[tileX, tileY + 1] % 2 == 1
                    && (tileY + 1) * 1000 < y + size) ||
                    (labirint[tileX + 1, tileY + 1] % 2 == 1
                            && (tileY + 1) * 1000 < y + size && (tileX + 1) * 1000 < x + size
                            )) {
                y = ((tileY + 1) * 1000) - size;
            }

            tileX = x / 1000;
            tileY = y / 1000;

            // Проверка пересечений по X
            if (((labirint[tileX, tileY] % 2 == 1)
                || (labirint[tileX, tileY + 1]  % 2 == 1
                        && (tileY + 1) * 1000 < y + size))) {
                x = (tileX + 1) * 1000 + 1;
            }
            if ((labirint[tileX + 1, tileY] % 2 == 1
                    && (tileX + 1) * 1000 < x + size) ||
                    (labirint[tileX + 1, tileY + 1] % 2 == 1
                            && (tileX + 1) * 1000 < x + size && (tileY + 1) * 1000 < y + size)) {
                x = (tileX + 1) * 1000 - size - 1;
            }

            tileX = x / 1000;
            tileY = y / 1000;

        }

        public void Draw(SpriteBatch spriteBatch, TextureManager textureManager, double windowK) {
            spriteBatch.Draw(textureManager.GetTexture2D("user_standart" + mov.ToString() + "_" + textCode.ToString()),
                                    new Rectangle(
                                        (int)(Game1.SCREEN_WIDTH / 2 - (size * windowK) / 2),
                                        (int)(Game1.SCREEN_HEIGHT / 2 - (size * windowK) / 2),
                                        (int)(size * windowK),
                                        (int)(size * windowK)),
                                    Color.AliceBlue);
        }

        /// <summary>
        /// Возврат координаты плитки по X
        /// </summary>
        /// <returns></returns>
        public int GetTileX() {
            return tileX;
        }

        /// <summary>
        /// Возврат координаты плитки по Y
        /// </summary>
        /// <returns></returns>
        public int GetTileY() {
            return tileY;
        }

        /// <summary>
        /// Возврат координаты по X
        /// </summary>
        /// <returns></returns>
        public int GetX() {
            return this.x;
        }

        /// <summary>
        /// Возврат координаты по Y
        /// </summary>
        /// <returns></returns>
        public int GetY() {
            return this.y;
        }


        /// <summary>
        /// Возврат размеров игрока.
        /// </summary>
        /// <returns></returns>
        public int GetSize() {
            return this.size;
        }
    }
}

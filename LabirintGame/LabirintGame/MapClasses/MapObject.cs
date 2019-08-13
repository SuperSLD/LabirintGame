using LabirintGame.Classes;
using LabirintGame.Labirint;
using LabirintGame.LabirintClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintGame.MapClasses {
    public  class MapObject : Observer{
        protected int x;
        protected int y;
        protected int tileX;
        protected int tileY;
        protected string texture = ""; 

        /// <summary>
        /// Конструктор обхекта
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public MapObject(int x, int y) {
            this.tileX = x;
            this.tileY = y;
            this.x = x * 1000;
            this.y = y * 1000;
        }

        /// <summary>
        /// Отрисовка обхекта.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="textureManager"></param>
        /// <param name="windowK"></param>
        public void Draw(SpriteBatch spriteBatch, 
                        TextureManager textureManager,
                        double windowK,
                        int windowX,
                        int windowY) {
            spriteBatch.Draw(textureManager.GetTexture2D(texture),
                                    new Rectangle(
                                        tileX * Game1.TILE_SIZE + windowX,
                                        tileY * Game1.TILE_SIZE + windowY,
                                        Game1.TILE_SIZE,
                                        Game1.TILE_SIZE),
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
        /// Обновлние объекта.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="labirint"></param>
        /// <param name="objects"></param>
        virtual public void Update(User user, int[,] labirint, List<MapObject> objects) {

        }
    }
}

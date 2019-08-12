using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintGame.LabirintClasses {

    /// <summary>
    /// Класс для упрощения доступа к загруженным текстурам.
    /// </summary>
    public class TextureManager {
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();


        /// <summary>
        /// Пустой конструтор.
        /// </summary>
        public TextureManager() {
        }


        /// <summary>
        /// Метод возвращающий текструру.
        /// </summary>
        /// <param name="code">Код возвращаемой текстуры.</param>
        /// <returns>Текстура.</returns>
        public Texture2D GetTexture2D(string code) {
            return textures[code];
        }

        /// <summary>
        /// Добавление текстуры в список.
        /// </summary>
        /// <param name="code">Код текстуры</param>
        /// <param name="texture">Текстура</param>
        public void AddTexture(string code, Texture2D texture) {
            this.textures.Add(code, texture);
        }
    }
}

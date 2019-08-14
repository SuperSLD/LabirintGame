using LabirintGame.LabirintClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintGame.Classes {
    public class TextWriter {
        TextureManager textureManager;

        public TextWriter(TextureManager textureManager) {
            this.textureManager = textureManager;
        }

        public void DrawText(SpriteBatch batch, string text, int x, int y, int h) {
            int s = 10;
            for (int i = 0; i < text.Length; i++) {
                batch.Draw(textureManager.GetTexture2D("num_" + text[i]), new Rectangle(
                    x + (h/2 + s) * i,
                    y,
                    h/2,
                    h),
                    Color.White);
            } 
        }
    }
}

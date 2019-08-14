using LabirintGame.Classes;
using LabirintGame.LabirintClasses;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintGame.Windows {
    public abstract class Window {
        protected TextureManager textureManager;
        protected SpriteBatch spriteBatch;
        protected TextWriter textWriter;

        public Window() {
            this.spriteBatch = spriteBatch;
            this.textureManager = textureManager;
        }

        public abstract void Initialize();

        public abstract void LoadContent(TextureManager textureManager, SpriteBatch batch, TextWriter textWriter);

        public abstract void Update();
        public abstract void Draw();
    }
}

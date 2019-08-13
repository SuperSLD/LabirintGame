using LabirintGame.Labirint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintGame.MapClasses {
    class Exit : MapObject {
        public Exit(int x, int y) : base(x, y) {
            texture = "object_exit";
        }

        public override void Update(User user, int[,] labirint, List<MapObject> objects) {
            if (user.GetX() >= this.x && user.GetX() + user.GetSize() <= this.x + 1000
                   && user.GetY() >= this.y && user.GetY() + user.GetSize() <= this.y + 1000) {
                Game1.state = 1;
            }
        }
    }
}

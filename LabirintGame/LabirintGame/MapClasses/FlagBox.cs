using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabirintGame.Labirint;

namespace LabirintGame.MapClasses {
    class FlagBox : MapObject {
        public FlagBox(int x, int y) : base(x, y) {
            texture = "object_flagbox";
        }

        private bool isCahge = false;

        public override void Update(User user, int[,] labirint, List<MapObject> objects) {
            if (user.GetX() >= this.x && user.GetX() + user.GetSize() <= this.x + 1000
                   && user.GetY() >= this.y && user.GetY() + user.GetSize() <= this.y + 1000 && ! isCahge) {
                user.SetFlags(user.GetFlags() + new Random().Next(1, 8));
                isCahge = true;
                texture = "backgraund2";
            }
        }
    }
}

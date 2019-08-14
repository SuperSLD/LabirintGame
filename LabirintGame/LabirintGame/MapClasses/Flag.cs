using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintGame.MapClasses {

    class Flag : MapObject {
        public Flag(int x, int y) : base(x, y) {
            texture = "object_flag";
        }
    }
}

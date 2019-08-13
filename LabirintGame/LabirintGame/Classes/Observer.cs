using LabirintGame.Labirint;
using LabirintGame.MapClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirintGame.Classes {
    interface Observer {

        /// <summary>
        /// Прием информации от объекта.
        /// </summary>
        void Update(User user, int[,] labirint, List<MapObject> objects);
    }
}

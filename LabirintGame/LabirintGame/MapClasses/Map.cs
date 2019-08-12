using LabirintGame.Classes;
using LabirintGame.Labirint;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labirint {
    class Map : Subject {
        public const int BRANCH_L = 2;

        private User user;
        private int l;
        private int[,] labirint;
        private Random rand;

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Map() {
            this.rand = new Random();
        }

        /// <summary>
        /// Конструктор на основе изначального значения.
        /// </summary>
        /// <param name="seed">Ключ генерации</param>
        public Map(int seed) {
            this.rand = new Random(seed);
        }

        /// <summary>
        /// Функция для построения лабиринта
        /// </summary>
        /// <param name="L">Размер лабиринта L*L</param>
        /// <returns>Сненерированный лабиритнт</returns>
        public int[,] LabirintGenerate(int L) {
            L = L % 2 == 0 ? L + 1 : L;
            labirint = new int[L, L];
            l = L;
            int k0 = 1; int k1 = 1;

            //while (k0*100 / k0+k1 >= 38) {
            for (int i = 0; i < L; i++) {
                for (int j = 0; j < L; j++) {
                    labirint[i, j] = 1;
                }
            }

            CreateBranch(1, l - 2);

            for (int i = 0; i < l; i++) {
                if (labirint[l - 2, i] == 0) {
                    labirint[l - 1, i] = 0;
                    break;
                }
            }

            for (int i = 0; i < L; i++) {
                for (int j = 0; j < L; j++) {
                    if (labirint[i, j] == 0) k0++;
                    if (labirint[i, j] == 1) {
                        k1++;
                        if (rand.Next(1, 4) == 2) labirint[i, j] = 3;
                        if (rand.Next(1, 4) == 3) labirint[i, j] = 5;
                    }
                }
            }
            //}
            return labirint;
        }

        /// <summary>
        /// Обновление информации слушателей.
        /// </summary>
        public void SendInfo() {
            user.Update();
        }

        /// <summary>
        /// Создание ветки рабиринта.
        /// </summary>
        /// <param name="x">Начало ветки</param>
        /// <param name="y">Начало ветки</param>
        /// <returns>Код завершения</returns>
        private int CreateBranch(int x, int y) {
            int leng = rand.Next(BRANCH_L + 2);
            int n;

            for (int i = 0; i <= leng; i++) {
                List<int> napr = IsClear(x, y);
                n = rand.Next(1, 5);
                while (!napr.Contains(n)) {
                    n = rand.Next(1, 5);
                    if (napr.Count == 0) return -1;
                }
                if (n == 1) {
                    labirint[x, y]     = 0;
                    labirint[x - 1, y] = 0;

                    x -= 2;
                }
                if (n == 2) {
                    labirint[x, y] = 0;
                    labirint[x, y - 1] = 0;

                    y -= 2;
                }
                if (n == 3) {
                    labirint[x, y] = 0;
                    labirint[x + 1, y] = 0;

                    x += 2;
                }
                if (n == 4) {
                    labirint[x, y] = 0;
                    labirint[x, y + 1] = 0;

                    y += 2;
                }
            }
            for (int i = 0; i < IsClear(x, y).Count; i++) {
                CreateBranch(x, y);
            }
           
            return 0;
        }

        /// <summary>
        /// Проверка клетки на отсутствие пересечений с 
        /// другими корридорами
        /// </summary>
        /// <param name="x">Центр обдасти проверки</param>
        /// <param name="y">Центр обдасти проверки</param>
        /// <returns>True если можно ставить корридор на (x;y)</returns>
        private List<int> IsClear(int x, int y) {
            List<int> napr = new List<int>();
            if (x - 2 >= 0 && labirint[x - 2, y] == 1) {
                napr.Add(1);
            }
            if (x + 2 <= l - 2 && labirint[x + 2, y] == 1) {
                napr.Add(3);
            }
            if (y - 2 >= 0 && labirint[x, y - 2] == 1) {
                napr.Add(2);
            }
            if (y + 2 <= l - 2 && labirint[x, y + 2] == 1) {
                napr.Add(4);
            }
            return napr;
        }

        /// <summary>
        /// Вывод лабиритна наружу.
        /// </summary>
        /// <returns></returns>
        public int[,] GetLabirint() {
            return this.labirint;
        }

        /// <summary>
        /// Назначение главного пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public void AddUser(User user) {
            this.user = user;
        }
    }
}
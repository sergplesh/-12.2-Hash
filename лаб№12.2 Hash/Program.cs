using GeometrucShapeCarLibrary;

namespace лаб_12._2_Hash
{
    internal class Program
    {
        /// <summary>
        /// выбор фигуры для добавления/удаления/поиска
        /// </summary>
        /// <param name="obj">фигура</param>
        public static void MenuChoise(ref Shape obj)
        {
            int answer;
            //do
            //{
            Console.WriteLine("1.Неопределённую фигуру");
            Console.WriteLine("2.Прямоугольник");
            Console.WriteLine("3.Параллелепипед");
            Console.WriteLine("4.Окружность");
            //Console.WriteLine("0.Выбор сделан - закрыть меню");
            Console.WriteLine("Выберите фигуру");
            answer = EnterNumber.EnterIntNumber(); // выбираем действие
            switch (answer)
            {
                case 1: // первый выбор
                    {
                        obj = new Shape();
                        break;
                    }
                case 2: // второй выбор
                    {
                        obj = new Rectangle();
                        break;
                    }
                case 3: // третий выбор
                    {
                        obj = new Parallelepiped();
                        break;
                    }
                case 4: // третий выбор
                    {
                        obj = new Circle();
                        break;
                    }
                //case 0: // программа продолжит работу
                //    {
                //        Console.WriteLine("Выбор закрыт");
                //        break;
                //    }
                default: // введённое число не подошло ни к одному пункту
                    {
                        Console.WriteLine("Неправильно задан пункт меню. По умолчанию выбрана - неопределённая фигура.");
                        break;
                    }
            }
            //} while (answer != 0); // не закрываем запросы, пока не введём 0
        }
        /// <summary>
        /// Инициализация фигуры
        /// </summary>
        /// <param name="message">Сообщение о том, для чего инициализируем фигуру</param>
        /// <returns></returns>
        public static Shape InitShape(string message = "Выбор фигуры:")
        {
            // Вводим фигуру
            Console.WriteLine(message);
            Shape shape = new Shape();
            MenuChoise(ref shape); // выбираем фигуру
            Console.WriteLine("Введите данные для объекта:");
            shape.Init(); // задаем параметры для фигуры
            return shape;
        }
        static void Main(string[] args)
        {
            // --------------------------------------------------------------------------------------------------------
            //  MyHashTable
            // --------------------------------------------------------------------------------------------------------
            MyHashTable<Shape> table = new MyHashTable<Shape>();
            int answer;
            do
            {
                Console.WriteLine("1.Сформировать хэш-таблицу вручную");
                Console.WriteLine("2.Сформировать с помощью ДСЧ хэш-таблицу (размерностью 28, содержащая 20 элементов, к-т заполненности = 0,714)");
                Console.WriteLine("3.Распечатать хэш-таблицу");
                Console.WriteLine("4.Добавить в хэш-таблицу элемент");
                Console.WriteLine("5.Удалить заданный элемент");
                Console.WriteLine("6.Поиск заданного элемента");
                Console.WriteLine("0.Закончить работу с хэш-таблицей");
                Console.WriteLine("Выберите пункт меню");
                answer = EnterNumber.EnterIntNumber(); // выбираем действие
                switch (answer)
                {
                    case 1: // первый выбор (Хэш-тааблица вручную)
                        {
                            // Вводим количество элементов, которые хотим ввести
                            Console.WriteLine("Введите количество объектов");
                            int count = EnterNumber.EnterIntNumber(); // вводим количество элементов, которыми будем заполнять хэш-таблицу
                            while (count < 0)
                            {
                                Console.WriteLine("Количество элементов не может быть отрицательным. Введите количество снова.");
                                count = EnterNumber.EnterIntNumber(); // вводим количество элементов, которыми будем заполнять хэш-таблицу
                            }
                            // Высчитываем, опираясь на коэффициент заполняемости(72%), размерность хэш-ьаблицы для count элементов
                            int size = (int)Math.Ceiling(count / 0.72);
                            table = new MyHashTable<Shape>(0); // выделяем память для хэш-таблицы посчитанной размерности 
                            // Заполняем хэш-таблицу элементами
                            Shape added = new Shape();
                            for (int i = 0; i < count; i++)
                            {
                                Console.WriteLine($"Ввод {i + 1} фигуры");
                                // вводим фигуру для добавления
                                added = InitShape();
                                table.AddItem(added); // добавляем в хэш-таблицу
                            }
                            // Вывод результата
                            Console.WriteLine("Сформированная вами хэш-таблица:");
                            table.Print();
                            break;
                        }
                    case 2: // второй выбор (Хэш-тааблица ДСЧ)
                        {
                            // мы выделим память для хэш-таблицы размерностью 28 и добавим 20 элементов
                            // таким образом коэффициент заполненности будет 0,714
                            // при добавлении ещё одного элемента размерность должна увеличиться
                            table = new MyHashTable<Shape>(28);
                            // фигура Shape
                            for (int i = 0; i < 5; i++)
                            {
                                Shape s = new Shape();
                                s.RandomInit();
                                table.AddItem(s); // добавляем в хэш-таблицу
                            }
                            // окружности
                            for (int i = 5; i < 10; i++)
                            {
                                Circle c = new Circle();
                                c.RandomInit();
                                table.AddItem(c); // добавляем в хэш-таблицу
                            }
                            // прямоугольники
                            for (int i = 10; i < 15; i++)
                            {
                                Rectangle r = new Rectangle();
                                r.RandomInit();
                                table.AddItem(r); // добавляем в хэш-таблицу
                            }
                            // параллелепипеды
                            for (int i = 15; i < 20; i++)
                            {
                                Parallelepiped p = new Parallelepiped();
                                p.RandomInit();
                                table.AddItem(p); // добавляем в хэш-таблицу
                            }
                            Console.WriteLine("Сформированная вами хэш-таблица:");
                            table.Print();
                            break;
                        }
                    case 3: // третий выбор (Печать)
                        {
                            table.Print();
                            break;
                        }
                    case 4: // четвертый выбор (Добавление)
                        {
                            Console.WriteLine($"ПЕРЕД ДОБАВЛЕНИЕМ: размерность {table.Capacity}, количество элементов в таблице {table.Count}");
                            // Вводим фигуру, которую хотим добавить
                            Shape added = InitShape("Введите фигуру, которую хотите добавить");
                            // Добавляем заданный элемент
                            bool isAdd = table.AddItem(added);
                            if (isAdd) Console.WriteLine("Элемент добавлен");
                            else Console.WriteLine("Данный элемент уже был добавлен ранее");
                            // Вывод результата
                            Console.WriteLine("Полученная хэш-таблица:");
                            table.Print();
                            Console.WriteLine($"ПОСЛЕ ДОБАВЛЕНИЕМ: размерность {table.Capacity}, количество элементов в таблице {table.Count}");
                            break;
                        }
                    case 5: // пятый выбор (Удаление)
                        {
                            if (table.Count == 0) Console.WriteLine("В хэш-таблице отсутствуют элементы, для удаления добавьте в неё элементы");
                            else
                            {
                                // Вводим фигуру, которую хотим добавить
                                Shape removed = InitShape("Введите фигуру, которую хотите удалить");
                                // Удаляем
                                bool isRemoved = table.RemoveData(removed);
                                if (!isRemoved) Console.WriteLine("Элемента в хэш-таблице не оказалось");
                                else Console.WriteLine("Элемент удалён");
                                Console.WriteLine("Удаление завершено");
                                Console.WriteLine("Полученная хэш-таблица:");
                                table.Print();
                            }
                            break;
                        }
                    case 6: // шестой выбор (Поиск)
                        {
                            if (table.Count == 0) Console.WriteLine("В хэш-таблице отсутствуют элементы, для поиска добавьте в неё элементы");
                            else
                            {
                                // Вводим фигуру, которую хотим найти
                                Shape searched = InitShape("Введите фигуру, которую хотите найти");
                                // Ищем
                                if (table.Contains(searched))
                                {
                                    Console.WriteLine($"Элемент находится на позиции: {table.FindItem(searched) + 1}");
                                }
                                else Console.WriteLine("Элемент не найден в хэш-таблице");
                            }
                            break;
                        }
                    case 0: // программа продолжит работу
                        {
                            Console.WriteLine("Выбор закрыт");
                            break;
                        }
                    default: // введённое число не подошло ни к одному пункту
                        {
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                        }
                }
            } while (answer != 0); // не закрываем запросы, пока не введём 0
        }
    }
}

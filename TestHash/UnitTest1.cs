using GeometrucShapeCarLibrary;
using System.Numerics;
using лаб_12._2_Hash;

namespace TestHash
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddItem_DifferentObjects() // Добавление отличающихся друг от друга объектов
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            int expectedCount = 3;

            // Act
            hashTable.AddItem(new Shape("элемент", 1));
            hashTable.AddItem(new Shape("элемент", 2));
            hashTable.AddItem(new Shape("элемент", 3));

            // Assert
            // все элементы разные - должны все добавиться
            Assert.AreEqual(expectedCount, hashTable.Count);
        }

        [TestMethod]
        public void TestAddItem_SimilarObjects() // Добавление одинаковых объектов
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            int expectedCount = 1;

            // Act
            hashTable.AddItem(new Shape("элемент", 0));
            hashTable.AddItem(new Shape("элемент", 0));
            hashTable.AddItem(new Shape("элемент", 0));

            // Assert
            // все элементы одинаковые - в хэш-таблице должен быть один элемент
            Assert.AreEqual(expectedCount, hashTable.Count);

        }

        [TestMethod]
        public void TestRemoveItem_Existent() // Удаление существующего в хэш-таблице элемента
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("элемент", 1));
            hashTable.AddItem(new Shape("элемент", 2));

            // Act
            bool removed = hashTable.RemoveData(new Shape("элемент", 2));

            // Assert
            Assert.IsTrue(removed);
            Assert.IsFalse(hashTable.Contains(new Shape("элемент", 2))); // убедимся, что элемент удалён
        }

        [TestMethod]
        public void TestRemoveItem_NonExistent() // Удаление НЕ существующего в хэш-таблице элемента
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("элемент", 1));
            hashTable.AddItem(new Shape("элемент", 2));

            // Act
            bool notAdded = hashTable.RemoveData(new Shape("элемент", 100)); // попытка удаления элемента, который не был добавлен

            // Assert
            Assert.IsFalse(notAdded); // удаление не произошло
        }

        [TestMethod]
        public void TestContains_Existent() // Проверка присутствия существующего в хэш-таблице элемента
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("элемент", 1));
            hashTable.AddItem(new Shape("элемент", 2));

            // Act
            bool isExist = hashTable.Contains(new Shape("элемент", 1));

            // Assert
            Assert.IsTrue(isExist); // элемент есть в таблице
        }

        [TestMethod]
        public void TestContains_NonExistent() // Проверка отсутствия НЕ существующего в хэш-таблице элемента
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("элемент", 1));
            hashTable.AddItem(new Shape("элемент", 2));

            // Act
            bool isNotExist = hashTable.Contains(new Shape("элемент", 100)); // попытка найти отсутствующий элемент

            // Assert
            Assert.IsFalse(isNotExist); // элемента нет в таблице
        }

        [TestMethod]
        public void TestFindItem_NonExistent() // Поиск НЕ существующего в хэш-таблице элемента
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("элемент", 1));
            hashTable.AddItem(new Shape("элемент", 2));

            // Act
            int NotExist = hashTable.FindItem(new Shape("элемент", 100)); // попытка найти отсутствующий элемент

            // Assert
            Assert.IsTrue(NotExist == -1); // элемента нет в таблице
        }

        [TestMethod]
        public void TestResize() // Превышение коэффициента заполненности и увеличение размерности хэш-таблицы
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(2); // начальный размер 2
            int expectedSize = 4;

            // Act
            hashTable.AddItem(new Shape("элемент", 1));
            hashTable.AddItem(new Shape("элемент", 2));

            // Assert
            Assert.AreEqual(expectedSize, hashTable.Capacity); // размер должен увеличиться в два раза, до 4
        }

        [TestMethod]
        public void TestAddItem_NullObject() // Добавление null-объекта
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            int expectedCount = 0;

            // Act
            hashTable.AddItem(null);

            // Assert
            Assert.AreEqual(expectedCount, hashTable.Count);
        }

        [TestMethod]
        public void TestAddItem_СollisionAfter() // Коллизия и вставка элемента ПОСЛЕ своего индекса
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // чтобы увеличить возможность коллизии, увеличим коэффициент заполненности таблицы
            int place; // место, куда встанет элемент, претерпевший коллизию
            Shape temp = new Shape(); // с помощью данной переменной запомним этот коллизионный элемент

            // Act
            while (true)
            {
                // создаём новый элемент для добавления
                Shape shape = new Shape();
                shape.RandomInit();
                // произошла коллизия:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ищем место, куда в итоге встанет добавляемый элемент
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // если он встал после предназначенного ему места, то выходим из цикла
                    if (place > hashTable.GetIndex(shape))
                    {
                        temp = shape;
                        break;
                    }
                }
                // добавляем очередной элемент
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.flags[hashTable.GetIndex(temp)] == 1); // назначенное место найденного элемента для нашего тестируемого случая оказалось занято
            Assert.IsTrue(place > hashTable.GetIndex(temp)); // встал после назначенного места
        }

        [TestMethod]
        public void TestAddItem_СollisionBefore() // Коллизия и вставка элемента ДО своего индекса
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // чтобы увеличить возможность коллизии, увеличим коэффициент заполненности таблицы
            int place; // место, куда встанет элемент, претерпевший коллизию
            Shape temp = new Shape(); // с помощью данной переменной запомним этот коллизионный элемент

            // Act
            while (true)
            {
                // создаём новый элемент для добавления
                Shape shape = new Shape();
                shape.RandomInit();
                // произошла коллизия:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ищем место, куда в итоге встанет добавляемый элемент
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // если он встал после предназначенного ему места, то выходим из цикла
                    if (place < hashTable.GetIndex(shape))
                    {
                        temp = shape;
                        break;
                    }
                }
                // добавляем очередной элемент
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.flags[hashTable.GetIndex(temp)] == 1); // назначенное место найденного элемента для нашего тестируемого случая оказалось занято
            Assert.IsTrue(place < hashTable.GetIndex(temp)); // встал после назначенного места
        }

        [TestMethod]
        public void TestAddItem_СollisionBefore_NotBegin() // Коллизия и вставка элемента ДО своего индексаб но не в начало таблицы
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // чтобы увеличить возможность коллизии, увеличим коэффициент заполненности таблицы
            int place; // место, куда встанет элемент, претерпевший коллизию
            Shape temp = new Shape(); // с помощью данной переменной запомним этот коллизионный элемент

            // Act
            while (true)
            {
                // создаём новый элемент для добавления
                Shape shape = new Shape();
                shape.RandomInit();
                // произошла коллизия:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ищем место, куда в итоге встанет добавляемый элемент
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // если он встал после предназначенного ему места, то выходим из цикла
                    if (place < hashTable.GetIndex(shape) && place != 0)
                    {
                        temp = shape;
                        break;
                    }
                }
                // добавляем очередной элемент
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.flags[hashTable.GetIndex(temp)] == 1); // назначенное место найденного элемента для нашего тестируемого случая оказалось занято
            Assert.IsTrue(place < hashTable.GetIndex(temp)); // встал после назначенного места
        }

        [TestMethod]
        public void TestFindExistItem_СollisionAfter() // Коллизия и поиск элемента, вставленного ПОСЛЕ своего индекса
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // чтобы увеличить возможность коллизии, увеличим коэффициент заполненности таблицы
            int place; // место, куда встанет элемент, претерпевший коллизию
            Shape temp = new Shape(); // с помощью данной переменной запомним этот коллизионный элемент

            // Act
            while (true)
            {
                // создаём новый элемент для добавления
                Shape shape = new Shape();
                shape.RandomInit();
                // произошла коллизия:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ищем место, куда в итоге встанет добавляемый элемент
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // если он встал после предназначенного ему места, то выходим из цикла
                    if (place > hashTable.GetIndex(shape))
                    {
                        hashTable.AddItem(shape);
                        temp = shape;
                        break;
                    }
                }
                // добавляем очередной элемент
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.FindItem(temp) > hashTable.GetIndex(temp)); // Поиск выдаст индекс после назначенного места
        }

        [TestMethod]
        public void TestFindExistItem_СollisionBefore() // Коллизия и поиск элемента, вставленного ДО своего индекса
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // чтобы увеличить возможность коллизии, увеличим коэффициент заполненности таблицы
            int place; // место, куда встанет элемент, претерпевший коллизию
            Shape temp = new Shape(); // с помощью данной переменной запомним этот коллизионный элемент

            // Act
            while (true)
            {
                // создаём новый элемент для добавления
                Shape shape = new Shape();
                shape.RandomInit();
                // произошла коллизия:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ищем место, куда в итоге встанет добавляемый элемент
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // если он встал после предназначенного ему места, то выходим из цикла
                    if (place < hashTable.GetIndex(shape))
                    {
                        hashTable.AddItem(shape);
                        temp = shape;
                        break;
                    }
                }
                // добавляем очередной элемент
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.FindItem(temp) < hashTable.GetIndex(temp)); // Поиск выдаст индекс до назначенного места
        }

        [TestMethod]
        public void TestFindExistItem_СollisionBefore_NotZeroAndNotFirstPosition() // Коллизия и поиск элемента, вставленного ДО своего индекса, но не в первые две позиции таблицы
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(1000, 1); // чтобы увеличить возможность коллизии, увеличим коэффициент заполненности таблицы
            int place; // место, куда встанет элемент, претерпевший коллизию
            Shape temp = new Shape(); // с помощью данной переменной запомним этот коллизионный элемент

            // Act
            while (true)
            {
                // создаём новый элемент для добавления
                Shape shape = new Shape();
                shape.RandomInit();
                // произошла коллизия:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ищем место, куда в итоге встанет добавляемый элемент
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // если он встал после предназначенного ему места, то выходим из цикла
                    if (place < hashTable.GetIndex(shape) && place > 1)
                    {
                        hashTable.AddItem(shape);
                        temp = shape;
                        break;
                    }
                }
                // добавляем очередной элемент
                hashTable.AddItem(shape);
            }
            //hashTable.RemoveData(hashTable.table[hashTable.GetIndex(temp) - 5]);

            // Assert
            Assert.IsTrue(hashTable.FindItem(temp) < hashTable.GetIndex(temp) && hashTable.FindItem(temp) > 1); // Поиск выдаст индекс до назначенного места (но не первые две позиции таблицы)
        }

        [TestMethod]
        public void TestFindNotExistItem_Сollision() // Поиск не существующего элемента, претерпевшего коллизию
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(100, 1.5); // увеличим коэффициент заполненности таблицы, чтобы заполнить её полностью, а затем искать в полностью заполненной таблице не существующий элемент
            Shape notExistShape = new Shape("Звёздочка", 777); // создадим не существующий элемент, который будем искать

            // Act
            // ПОЛНОСТЬЮ заполняем таблицу. Добиваемся того, чтобы при поиске несуществующего элемента происходила коллизия в ПОЛНОСТЬЮ заполненной таблице
            while (hashTable.Capacity != hashTable.Count)
            {
                // создаём новый элемент для добавления
                Shape shape = new Shape();
                shape.RandomInit();
                // добавляем
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.FindItem(notExistShape) == -1); // Поиск выдаст -1, так как элемента в таблице нет
        }
    }
}
using GeometrucShapeCarLibrary;
using System.Numerics;
using ���_12._2_Hash;

namespace TestHash
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddItem_DifferentObjects() // ���������� ������������ ���� �� ����� ��������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            int expectedCount = 3;

            // Act
            hashTable.AddItem(new Shape("�������", 1));
            hashTable.AddItem(new Shape("�������", 2));
            hashTable.AddItem(new Shape("�������", 3));

            // Assert
            // ��� �������� ������ - ������ ��� ����������
            Assert.AreEqual(expectedCount, hashTable.Count);
        }

        [TestMethod]
        public void TestAddItem_SimilarObjects() // ���������� ���������� ��������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            int expectedCount = 1;

            // Act
            hashTable.AddItem(new Shape("�������", 0));
            hashTable.AddItem(new Shape("�������", 0));
            hashTable.AddItem(new Shape("�������", 0));

            // Assert
            // ��� �������� ���������� - � ���-������� ������ ���� ���� �������
            Assert.AreEqual(expectedCount, hashTable.Count);

        }

        [TestMethod]
        public void TestRemoveItem_Existent() // �������� ������������� � ���-������� ��������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("�������", 1));
            hashTable.AddItem(new Shape("�������", 2));

            // Act
            bool removed = hashTable.RemoveData(new Shape("�������", 2));

            // Assert
            Assert.IsTrue(removed);
            Assert.IsFalse(hashTable.Contains(new Shape("�������", 2))); // ��������, ��� ������� �����
        }

        [TestMethod]
        public void TestRemoveItem_NonExistent() // �������� �� ������������� � ���-������� ��������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("�������", 1));
            hashTable.AddItem(new Shape("�������", 2));

            // Act
            bool notAdded = hashTable.RemoveData(new Shape("�������", 100)); // ������� �������� ��������, ������� �� ��� ��������

            // Assert
            Assert.IsFalse(notAdded); // �������� �� ���������
        }

        [TestMethod]
        public void TestContains_Existent() // �������� ����������� ������������� � ���-������� ��������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("�������", 1));
            hashTable.AddItem(new Shape("�������", 2));

            // Act
            bool isExist = hashTable.Contains(new Shape("�������", 1));

            // Assert
            Assert.IsTrue(isExist); // ������� ���� � �������
        }

        [TestMethod]
        public void TestContains_NonExistent() // �������� ���������� �� ������������� � ���-������� ��������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("�������", 1));
            hashTable.AddItem(new Shape("�������", 2));

            // Act
            bool isNotExist = hashTable.Contains(new Shape("�������", 100)); // ������� ����� ������������� �������

            // Assert
            Assert.IsFalse(isNotExist); // �������� ��� � �������
        }

        [TestMethod]
        public void TestFindItem_NonExistent() // ����� �� ������������� � ���-������� ��������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>();
            hashTable.AddItem(new Shape("�������", 1));
            hashTable.AddItem(new Shape("�������", 2));

            // Act
            int NotExist = hashTable.FindItem(new Shape("�������", 100)); // ������� ����� ������������� �������

            // Assert
            Assert.IsTrue(NotExist == -1); // �������� ��� � �������
        }

        [TestMethod]
        public void TestResize() // ���������� ������������ ������������� � ���������� ����������� ���-�������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(2); // ��������� ������ 2
            int expectedSize = 4;

            // Act
            hashTable.AddItem(new Shape("�������", 1));
            hashTable.AddItem(new Shape("�������", 2));

            // Assert
            Assert.AreEqual(expectedSize, hashTable.Capacity); // ������ ������ ����������� � ��� ����, �� 4
        }

        [TestMethod]
        public void TestAddItem_NullObject() // ���������� null-�������
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
        public void TestAddItem_�ollisionAfter() // �������� � ������� �������� ����� ������ �������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // ����� ��������� ����������� ��������, �������� ����������� ������������� �������
            int place; // �����, ���� ������� �������, ������������ ��������
            Shape temp = new Shape(); // � ������� ������ ���������� �������� ���� ������������ �������

            // Act
            while (true)
            {
                // ������ ����� ������� ��� ����������
                Shape shape = new Shape();
                shape.RandomInit();
                // ��������� ��������:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ���� �����, ���� � ����� ������� ����������� �������
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // ���� �� ����� ����� ���������������� ��� �����, �� ������� �� �����
                    if (place > hashTable.GetIndex(shape))
                    {
                        temp = shape;
                        break;
                    }
                }
                // ��������� ��������� �������
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.flags[hashTable.GetIndex(temp)] == 1); // ����������� ����� ���������� �������� ��� ������ ������������ ������ ��������� ������
            Assert.IsTrue(place > hashTable.GetIndex(temp)); // ����� ����� ������������ �����
        }

        [TestMethod]
        public void TestAddItem_�ollisionBefore() // �������� � ������� �������� �� ������ �������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // ����� ��������� ����������� ��������, �������� ����������� ������������� �������
            int place; // �����, ���� ������� �������, ������������ ��������
            Shape temp = new Shape(); // � ������� ������ ���������� �������� ���� ������������ �������

            // Act
            while (true)
            {
                // ������ ����� ������� ��� ����������
                Shape shape = new Shape();
                shape.RandomInit();
                // ��������� ��������:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ���� �����, ���� � ����� ������� ����������� �������
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // ���� �� ����� ����� ���������������� ��� �����, �� ������� �� �����
                    if (place < hashTable.GetIndex(shape))
                    {
                        temp = shape;
                        break;
                    }
                }
                // ��������� ��������� �������
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.flags[hashTable.GetIndex(temp)] == 1); // ����������� ����� ���������� �������� ��� ������ ������������ ������ ��������� ������
            Assert.IsTrue(place < hashTable.GetIndex(temp)); // ����� ����� ������������ �����
        }

        [TestMethod]
        public void TestAddItem_�ollisionBefore_NotBegin() // �������� � ������� �������� �� ������ �������� �� �� � ������ �������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // ����� ��������� ����������� ��������, �������� ����������� ������������� �������
            int place; // �����, ���� ������� �������, ������������ ��������
            Shape temp = new Shape(); // � ������� ������ ���������� �������� ���� ������������ �������

            // Act
            while (true)
            {
                // ������ ����� ������� ��� ����������
                Shape shape = new Shape();
                shape.RandomInit();
                // ��������� ��������:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ���� �����, ���� � ����� ������� ����������� �������
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // ���� �� ����� ����� ���������������� ��� �����, �� ������� �� �����
                    if (place < hashTable.GetIndex(shape) && place != 0)
                    {
                        temp = shape;
                        break;
                    }
                }
                // ��������� ��������� �������
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.flags[hashTable.GetIndex(temp)] == 1); // ����������� ����� ���������� �������� ��� ������ ������������ ������ ��������� ������
            Assert.IsTrue(place < hashTable.GetIndex(temp)); // ����� ����� ������������ �����
        }

        [TestMethod]
        public void TestFindExistItem_�ollisionAfter() // �������� � ����� ��������, ������������ ����� ������ �������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // ����� ��������� ����������� ��������, �������� ����������� ������������� �������
            int place; // �����, ���� ������� �������, ������������ ��������
            Shape temp = new Shape(); // � ������� ������ ���������� �������� ���� ������������ �������

            // Act
            while (true)
            {
                // ������ ����� ������� ��� ����������
                Shape shape = new Shape();
                shape.RandomInit();
                // ��������� ��������:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ���� �����, ���� � ����� ������� ����������� �������
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // ���� �� ����� ����� ���������������� ��� �����, �� ������� �� �����
                    if (place > hashTable.GetIndex(shape))
                    {
                        hashTable.AddItem(shape);
                        temp = shape;
                        break;
                    }
                }
                // ��������� ��������� �������
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.FindItem(temp) > hashTable.GetIndex(temp)); // ����� ������ ������ ����� ������������ �����
        }

        [TestMethod]
        public void TestFindExistItem_�ollisionBefore() // �������� � ����� ��������, ������������ �� ������ �������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(10, 1); // ����� ��������� ����������� ��������, �������� ����������� ������������� �������
            int place; // �����, ���� ������� �������, ������������ ��������
            Shape temp = new Shape(); // � ������� ������ ���������� �������� ���� ������������ �������

            // Act
            while (true)
            {
                // ������ ����� ������� ��� ����������
                Shape shape = new Shape();
                shape.RandomInit();
                // ��������� ��������:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ���� �����, ���� � ����� ������� ����������� �������
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // ���� �� ����� ����� ���������������� ��� �����, �� ������� �� �����
                    if (place < hashTable.GetIndex(shape))
                    {
                        hashTable.AddItem(shape);
                        temp = shape;
                        break;
                    }
                }
                // ��������� ��������� �������
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.FindItem(temp) < hashTable.GetIndex(temp)); // ����� ������ ������ �� ������������ �����
        }

        [TestMethod]
        public void TestFindExistItem_�ollisionBefore_NotZeroAndNotFirstPosition() // �������� � ����� ��������, ������������ �� ������ �������, �� �� � ������ ��� ������� �������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(1000, 1); // ����� ��������� ����������� ��������, �������� ����������� ������������� �������
            int place; // �����, ���� ������� �������, ������������ ��������
            Shape temp = new Shape(); // � ������� ������ ���������� �������� ���� ������������ �������

            // Act
            while (true)
            {
                // ������ ����� ������� ��� ����������
                Shape shape = new Shape();
                shape.RandomInit();
                // ��������� ��������:
                if (hashTable.flags[hashTable.GetIndex(shape)] == 1)
                {
                    // ���� �����, ���� � ����� ������� ����������� �������
                    place = hashTable.SearchPlace(hashTable.GetIndex(shape));
                    // ���� �� ����� ����� ���������������� ��� �����, �� ������� �� �����
                    if (place < hashTable.GetIndex(shape) && place > 1)
                    {
                        hashTable.AddItem(shape);
                        temp = shape;
                        break;
                    }
                }
                // ��������� ��������� �������
                hashTable.AddItem(shape);
            }
            //hashTable.RemoveData(hashTable.table[hashTable.GetIndex(temp) - 5]);

            // Assert
            Assert.IsTrue(hashTable.FindItem(temp) < hashTable.GetIndex(temp) && hashTable.FindItem(temp) > 1); // ����� ������ ������ �� ������������ ����� (�� �� ������ ��� ������� �������)
        }

        [TestMethod]
        public void TestFindNotExistItem_�ollision() // ����� �� ������������� ��������, ������������� ��������
        {
            // Arrange
            MyHashTable<Shape> hashTable = new MyHashTable<Shape>(100, 1.5); // �������� ����������� ������������� �������, ����� ��������� � ���������, � ����� ������ � ��������� ����������� ������� �� ������������ �������
            Shape notExistShape = new Shape("��������", 777); // �������� �� ������������ �������, ������� ����� ������

            // Act
            // ��������� ��������� �������. ���������� ����, ����� ��� ������ ��������������� �������� ����������� �������� � ��������� ����������� �������
            while (hashTable.Capacity != hashTable.Count)
            {
                // ������ ����� ������� ��� ����������
                Shape shape = new Shape();
                shape.RandomInit();
                // ���������
                hashTable.AddItem(shape);
            }

            // Assert
            Assert.IsTrue(hashTable.FindItem(notExistShape) == -1); // ����� ������ -1, ��� ��� �������� � ������� ���
        }
    }
}
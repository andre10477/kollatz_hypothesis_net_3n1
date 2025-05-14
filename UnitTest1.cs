using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using _3N_1;

namespace KOLLATZ_Test
{
    [TestClass]
    public class UnitTest1
    {
        // Основная проверка для гипотезы Коллатца
        [TestMethod]
        [DataRow(1, 0)] // Проверка для n = 1, длина последовательности должна быть 0 (без итераций)
        [DataRow(2, 1)] // Для n = 2 последовательность: 2 → 1, итераций 1
        [DataRow(3, 7)] // Для n = 3: 3 → 10 → 5 → 16 → 8 → 4 → 2 → 1, итераций 7
        [DataRow(4, 2)] // Для n = 4: 4 → 2 → 1, итераций 2
        [DataRow(5, 5)] // Для n = 5: 5 → 16 → 8 → 4 → 2 → 1, итераций 5
        [DataRow(6, 8)] // Для n = 6: 6 → 3 → 10 → 5 → 16 → 8 → 4 → 2 → 1, итераций 8
        public void TestCollatzAlgorithm(int input, int expectedIterations)
        {
            Zadacha _z = new Zadacha();
            int actualIterations = _z.algorithm(input); // Метод, который реализует алгоритм гипотезы Коллатца
            Assert.AreEqual(expectedIterations, actualIterations);
        }

        // Проверка для некорректных данных
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(0)]   // Ноль не является допустимым входом для гипотезы
        [DataRow(-1)]  // Отрицательные числа не могут использоваться
        [DataRow(-10)] // Тоже некорректный ввод
        public void TestCollatzAlgorithm_InvalidInput(int input)
        {
            Zadacha _z = new Zadacha();
            _z.algorithm(input); // Ожидается, что выбросится исключение ArgumentException
        }
    }
}


/*
        [DataRow(1, -1)]
        [DataRow(1, 0)]
        [DataRow(1, 1)]
        [DataRow(1, 2)]
        [DataRow(1, 3)]
        [DataRow(1, 4)]
        [DataRow(5, 15)]
        [DataRow(5, 16)]
        [DataRow(5, 17)]
        [DataRow(10, 30)]
        [DataRow(10, 31)]
        [DataRow(10, 32)]
        [DataRow(15, 45)]
        [DataRow(15, 46)]
        [DataRow(15, 47)]
 */
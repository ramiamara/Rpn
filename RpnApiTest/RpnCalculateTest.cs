using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RpnApi.Models;
using RpnApi.Services;
namespace RpnApiTest
{
    [TestFixture]
    public class RpnCalculateTest
    {
        private readonly List<string> operators = new() { "+", "-", "*", "/" };

        [Test]
        public void Calculate_WithoutStacks_shouldReturnsNull()
        {
            var mockStackService = new Mock<IStackService>();

            _ = mockStackService.Setup(x => x.Get())
                .Returns(new Dictionary<int, Stack<Entry>>());

            var sut = new CalculateService(mockStackService.Object);
            // Act
            var result = sut.Calculate(0, GetRandomOperators(operators));

            // Assert
            Assert.Null(result);
        }

        [Test]
        public void Calculate_WithDivisionOperatorAndEntryZero_shouldReturnsException()
        {
            var random = new Random();
            var expectedEntry = new Entry[] {
                new Entry(random.Next(1,100)),
                new Entry(0),
                new Entry(random.Next(1, 100))
            };
            var expectedStack = new Stack<Entry>(expectedEntry);
            var expectedDictionary = new Dictionary<int, Stack<Entry>>() {
                    {0, expectedStack }
                };
            var mockStackService = new Mock<IStackService>();

            _ = mockStackService.Setup(x => x.Get())
                .Returns(expectedDictionary);

            _ = mockStackService.Setup(x => x.Get(0))
                .Returns(expectedStack);

            var sut = new CalculateService(mockStackService.Object);

            // Assert

            var ex = Assert.Throws<DivideByZeroException>(() => sut.Calculate(0, "/"));
            Assert.That(ex.Message, Is.EqualTo("Attempted to divide by zero."));
        }

        [Test]
        [TestCase("+", 15)]
        [TestCase("-", 5)]
        [TestCase("*", 50)]
        [TestCase("/", 2)]
        public void Calculate_HappyPath_ShouldReturnResult(string op, int resultStack)
        {
            var random = new Random();
            var expectedEntry = new Entry[] {
                new Entry(random.Next(1,100)),
                new Entry(5),
                new Entry(10)
            };
            var expectedStack = new Stack<Entry>(expectedEntry);
            var expectedDictionary = new Dictionary<int, Stack<Entry>>() {
                    {0, expectedStack }
                };
            var mockStackService = new Mock<IStackService>();

            _ = mockStackService.Setup(x => x.Get())
                .Returns(expectedDictionary);

            _ = mockStackService.Setup(x => x.Get(0))
                .Returns(expectedStack);

            var sut = new CalculateService(mockStackService.Object);

            // Act
            var result = sut.Calculate(0, op);

            // Assert
            Assert.AreEqual(resultStack, result.Peek().Value);
        }

        private static string GetRandomOperators(List<string> operators)
        {
            var random = new Random();
            int index = random.Next(operators.Count);
            return operators[index];
        }
    }
}
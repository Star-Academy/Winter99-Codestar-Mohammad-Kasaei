using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class QueryEngineTest
    {
        private static readonly Token repair = new("repair");
        private static readonly Token buy = new("buy");
        private static readonly Token car = new("car");
        private static readonly Token bike = new("bike");
        

        private static readonly Document repairCar = new("repair_car");
        private static readonly Document buyCar = new("buy_car");
        private static readonly Document repairBuyCar = new("repair_buy_car");
        private static readonly Document repairBike = new("repair_bike");
        private static readonly Document buyBike = new("buy_bike");

        [Fact]
        public void ConstructorTest()
        {
            InvertedIndex index = new();
            _ = new QueryEngine(index);
        }

        private static InvertedIndex GetMockedInvertedIndex()
        {
            var index = new Mock<InvertedIndex>();

            index
                .Setup(
                    x => x.SearchTokenInDocuments(It.IsAny<Token>())
                ).Returns(
                    new DocumentSet()
                );

            index
                .Setup(
                    x => x.SearchTokenInDocuments(repair)
                ).Returns(
                    new DocumentSet(
                        repairCar ,
                        repairBuyCar,
                        repairBike
                    )
                );
            index
                .Setup(
                    x => x.SearchTokenInDocuments(buy)
                ).Returns(
                    new DocumentSet(
                        buyCar,
                        repairBuyCar,
                        buyBike
                    )
                );
            index
                .Setup(
                    x => x.SearchTokenInDocuments(car)
                ).Returns(
                    new DocumentSet(
                        buyCar,
                        repairBuyCar,
                        repairCar
                    )
                );
            index
                .Setup(
                    x => x.SearchTokenInDocuments(bike)
                ).Returns(
                    new DocumentSet(
                        repairBike,
                        buyBike
                    )
                );
            return index.Object;
        }

        [Fact]
        public void MockTest()
        {
            _ = new QueryEngine(GetMockedInvertedIndex());
        }

        [Fact]
        public void AdvancedQueryAndOneExistingWordTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            engine.AdvancedSearch("car");
        }

        [Fact]
        public void AdvancedQueryAndCarTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet(buyCar, repairBuyCar, repairCar);
            var actual = engine.AdvancedSearch("car");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AdvancedQueryAndBikeTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet(repairBike, buyBike);
            var actual = engine.AdvancedSearch("bike");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AdvancedQueryAndOneNotExistingWordTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet();
            var actual = engine.AdvancedSearch("test");
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void AdvancedQueryAndTwoWordsEmptyResultTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet();
            var actual = engine.AdvancedSearch("test" , "car");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AdvancedQueryAndTwoWordsNonEmptyResultTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet(buyCar,repairBuyCar);
            var actual = engine.AdvancedSearch("buy" , "car");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AdvancedQueryORTwoWordsNonEmptyResultTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet(buyCar, repairBuyCar, repairCar, buyBike);
            var actual = engine.AdvancedSearch("+buy" , "+car");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AdvancedQueryORTwoWordsFullSetTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet(buyCar, repairBuyCar, repairCar, buyBike , repairBike);
            var actual = engine.AdvancedSearch("+bike", "+car");
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void AdvancedQueryNotOneWordTest()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet();
            var actual = engine.AdvancedSearch("-bike");
            Assert.Equal(expected, actual);
        }        
        
        [Fact]
        public void AdvancedQueryComplex1Test()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet();
            var actual = engine.AdvancedSearch("+car", "+bike", "repair", "buy", "-repair");
            Assert.Equal(expected, actual);
        }        

        [Fact]
        public void AdvancedQueryComplex2Test()
        {
            var engine = new QueryEngine(GetMockedInvertedIndex());
            var expected = new DocumentSet(repairCar);
            var actual = engine.AdvancedSearch("+car", "repair" , "-buy");
            Assert.Equal(expected, actual);
        }
    }
}

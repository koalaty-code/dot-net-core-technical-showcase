using Bogus;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechnicalShowcase.Data.Models;

namespace TechnicalShowcase.Tests.Data.Models
{
    [TestClass]
    public class PhotoTest
    {
        private Photo _photo;
        private Randomizer _random;
        private Faker<Photo> _testPhotos;

        [TestInitialize]
        public void BeforeEach()
        {
            _random = new Randomizer();
            _testPhotos = new Faker<Photo>()
                .RuleFor(p => p.Id, _random.Int())
                .RuleFor(p => p.Title, _random.Words());

            _photo = _testPhotos.Generate();
        }

        [TestClass]
        public class ToStringOverrideTest : PhotoTest
        {
            [TestMethod]
            public void ShouldOverrideToStringMethodToReturnStringRepresentationOfPhoto()
            {
                var expectedToStringResult = $"[{_photo.Id}] {_photo.Title}";

                var actualToStringResult = _photo.ToString();

                actualToStringResult.Should().Be(expectedToStringResult);
            }
        }
    }
}

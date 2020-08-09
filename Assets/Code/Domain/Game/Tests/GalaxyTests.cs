using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Code.Presentation.Game.Asteroid;
using Code.Presentation.Game.Saucer;
using NUnit.Framework;
using Zenject;
using Moq;

namespace Code.Domain.Game.Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "CheckNamespace")]
    public sealed class GalaxyTests :  ZenjectUnitTestFixture
    {
        private Mock<IPlayer> _player;
        private Mock<IRandomSaucerHelper> _randomSaucerHelper;

        [SetUp]
        public void InstallBindings()
        {            
            _player = new Mock<IPlayer>();
            _randomSaucerHelper = new Mock<IRandomSaucerHelper>();

            Container.Bind<IPlayer>().FromInstance(_player.Object);
            Container.Bind<IRandomSaucerHelper>().FromInstance(_randomSaucerHelper.Object);
            Container.BindInterfacesAndSelfTo<Galaxy>().AsTransient();
        }

        [Test]
        [TestCase(10)]
        public void NewGalaxy_CreatesAsteroids(int number)
        {
            var sut = Container.Resolve<IGalaxy>();

            AsteroidVo[] asteroidVos = sut.NewGalaxy(number);

            Assert.AreEqual(number, asteroidVos.Length);
        }

        [Test]
        [TestCase(10)]
        public void NewGalaxy_CreatesBigAsteroids(int number)
        {
            var sut = Container.Resolve<IGalaxy>();

            AsteroidVo[] asteroidVos = sut.NewGalaxy(number);
            
            Assert.IsTrue(asteroidVos.All(x=> x.Type == AsteroidType.Big));
        }
        
        [Test]
        public void DestroyAsteroid_CreatesTwoAsteroids_WhenIsABigAsteroid()
        {
            var sut = Container.Resolve<IGalaxy>();

            sut.NewGalaxy(1);
            AsteroidVo[] asteroidDestroyed = sut.AsteroidDestroyed(AsteroidType.Big, 0);
            
            Assert.AreEqual(2, asteroidDestroyed.Length);
        }
        
        [Test]
        public void DestroyAsteroid_CreatesMediumAsteroids_WhenIsABigAsteroid()
        {
            var sut = Container.Resolve<IGalaxy>();

            sut.NewGalaxy(1);
            
            AsteroidVo[] asteroidDestroyed = sut.AsteroidDestroyed(AsteroidType.Big, 0);
            
            Assert.IsTrue(asteroidDestroyed.All(x=> x.Type == AsteroidType.Medium));
        }
        
        [Test]
        public void DestroyAsteroid_CreatesTwoSmallAsteroids_WhenIsAMediumAsteroid()
        {
            var sut = Container.Resolve<IGalaxy>();

            sut.NewGalaxy(1);
            
            sut.AsteroidDestroyed(AsteroidType.Big, 0);
            AsteroidVo[] asteroidDestroyed = sut.AsteroidDestroyed(AsteroidType.Medium, 0);
            
            Assert.AreEqual(2, asteroidDestroyed.Length);
        } 
        
        [Test]
        public void DestroyAsteroid_CreatesSmallAsteroids_WhenIsAMediumAsteroid()
        {
            var sut = Container.Resolve<IGalaxy>();

            sut.NewGalaxy(1);
            
            sut.AsteroidDestroyed(AsteroidType.Big, 0);
            AsteroidVo[] asteroidDestroyed = sut.AsteroidDestroyed(AsteroidType.Medium, 0);
            
            Assert.IsTrue(asteroidDestroyed.All(x=> x.Type == AsteroidType.Small));
        } 
        
        [Test]
        public void DestroyAsteroid_DoesntCreateAsteroids_WhenIsASmallAsteroid()
        {
            var sut = Container.Resolve<IGalaxy>();

            sut.NewGalaxy(1);
            
            sut.AsteroidDestroyed(AsteroidType.Big, 0);
            sut.AsteroidDestroyed(AsteroidType.Medium, 0);
            AsteroidVo[] asteroidDestroyed = sut.AsteroidDestroyed(AsteroidType.Small, 0);
            
            Assert.IsEmpty(asteroidDestroyed);
        } 
        
        [Test]
        public void DestroyAsteroid_IncrementsLevelByOne_WhenNoAsteroidsRemain()
        {
            var sut = Container.Resolve<IGalaxy>();

            sut.NewGalaxy(1);
            sut.AsteroidDestroyed(AsteroidType.Big, 0);
            sut.AsteroidDestroyed(AsteroidType.Medium, 0);
            sut.AsteroidDestroyed(AsteroidType.Medium, 0);
            sut.AsteroidDestroyed(AsteroidType.Small, 0);
            sut.AsteroidDestroyed(AsteroidType.Small, 0);
            sut.AsteroidDestroyed(AsteroidType.Small, 0);
            sut.AsteroidDestroyed(AsteroidType.Small, 0);
            
            Assert.AreEqual(0, sut.NumberOfAsteroids);
            Assert.AreEqual(1, sut.Level);
        }
        
        [Test]
        public void DestroyAsteroid_InvokesAddScoreInPlayer()
        {
            var sut = Container.Resolve<IGalaxy>();

            sut.NewGalaxy(1);
            sut.AsteroidDestroyed(AsteroidType.Big, 100);
            
            _player.Verify(x=> x.AddScore(100));
        }
        
        [Test]
        public void DestroyAsteroid_ThrowsException_IfThereAreNoAsteroidsToDestroy()
        {
            var sut = Container.Resolve<IGalaxy>();

            var ex = Assert.Throws<Exception>(() => sut.AsteroidDestroyed(AsteroidType.Big, 0));
        }
        
        [Test]
        [TestCase(40000)]
        [TestCase(70000)]
        public void NewSaucer_ReturnsSmallSaucer_WhenScoreIsOver40000(int score)
        {
            var sut = Container.Resolve<IGalaxy>();

            _player.Setup(p => p.Score).Returns(score);

            SaucerType type = sut.NewSaucer();
            
            Assert.AreEqual(SaucerType.Small, type);
        }
        
        [Test]
        [TestCase(10)]
        [TestCase(39999)]
        public void NewSaucer_ReturnsRandomSaucer_WhenScoreIsUnder40000(int score)
        {
            var sut = Container.Resolve<IGalaxy>();

            _player.Setup(p => p.Score).Returns(score);
            _randomSaucerHelper.Setup(p => p.GetRandomSaucer()).Returns(SaucerType.Big);

            SaucerType type = sut.NewSaucer();
            
            Assert.AreEqual(SaucerType.Big, type);
        }
    }
}
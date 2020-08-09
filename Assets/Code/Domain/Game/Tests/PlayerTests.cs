using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Zenject;

namespace Code.Domain.Game.Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "CheckNamespace")]
    public sealed class PlayerTests :  ZenjectUnitTestFixture
    {
        [SetUp]
        public void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<IPlayer>().FromInstance(new Player(3));
        }
        
        [Test]
        public void SpaceShipDamaged_ShouldDecreaseOneLife()
        {
            var sut = Container.Resolve<IPlayer>();
            
            sut.SpaceshipDamaged();
            
            Assert.AreEqual(2,sut.Life);
        }

        [Test]
        [TestCase(100, 3)]
        [TestCase(9900, 3)]
        [TestCase(10000, 4)]
        [TestCase(60000, 9)]
        public void AddScore_ShouldAddOneLife_Every10000Score(long extraScore, long expectedLives)
        {
            var sut = Container.Resolve<IPlayer>();
            
            sut.AddScore(extraScore);
            
            Assert.AreEqual(expectedLives,sut.Life);
        }
    }
}

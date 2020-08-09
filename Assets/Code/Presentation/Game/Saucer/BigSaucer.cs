using Zenject;

namespace Code.Presentation.Game.Saucer
{
    public class BigSaucer : Saucer
    {
        [Inject(Id=SaucerType.Big)] private Saucer.Pool _bigPool;

        protected override Pool _pool
        {
            get { return _bigPool; }
        }

        protected override float _velocity
        {
            get { return _gameConfiguration.BigSaucerConfiguration.Velocity; }
        }

        protected override float _shootInterval
        {
            get { return _gameConfiguration.BigSaucerConfiguration.ShootInterval; }
        }

        protected override void SaucerDestroyedCommand()
        {
            _saucersViewModel.SaucerDestroyedCommand(SaucerType.Big, _gameConfiguration.BigSaucerConfiguration.DestroyScore);
        }
    }
}
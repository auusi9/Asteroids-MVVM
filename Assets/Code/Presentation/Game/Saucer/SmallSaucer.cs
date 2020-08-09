using Zenject;

namespace Code.Presentation.Game.Saucer
{
    public class SmallSaucer : Saucer
    {
        [Inject(Id=SaucerType.Small)] private Saucer.Pool _smallSaucer;

        protected override Pool _pool
        {
            get { return _smallSaucer; }
        }

        protected override float _velocity
        {
            get { return _gameConfiguration.SmallSaucerConfiguration.Velocity; }
        }

        protected override float _shootInterval
        {
            get { return _gameConfiguration.SmallSaucerConfiguration.ShootInterval; }
        }
        
        protected override void SaucerDestroyedCommand()
        {
            _saucersViewModel.SaucerDestroyedCommand(SaucerType.Small, _gameConfiguration.SmallSaucerConfiguration.DestroyScore);
        }
    }
}
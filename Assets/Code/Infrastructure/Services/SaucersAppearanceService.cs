using System;

namespace Code.Infrastructure.Services
{
    public class SaucersAppearanceService : ISaucersAppearanceService
    {
        private const int TIME_BETWEEN_SAUCERS = 15000;
        private const int MINIMUM_TIME_BETWEEN_SAUCERS = 5000;
        private const int TIME_DECREASE_BETWEEN_SAUCERS = 1000;

        private int _saucersOnLevel = 0;
        private SaucerRequest _currentSaucerRequest;
        
        public void StartLevel()
        {
            AbortCurrentRequest();
            
            NewSaucerRequest();
        }

        public void EndLevel()
        {
            AbortCurrentRequest();
        }

        public void GameOver()
        {
            AbortCurrentRequest();
        }

        private void InvokeNewSaucer(object sender, EventArgs args)
        {
            _currentSaucerRequest.NewSaucer -= InvokeNewSaucer;
            _saucersOnLevel++;
            
            NewSaucer?.Invoke(this, null);
            NewSaucerRequest();
        }

        private void NewSaucerRequest()
        {
            _currentSaucerRequest = new SaucerRequest(GetTimeBetweenSaucers());
            _currentSaucerRequest.Request();
            _currentSaucerRequest.NewSaucer += InvokeNewSaucer;
        }

        private int GetTimeBetweenSaucers()
        {
            int time = TIME_BETWEEN_SAUCERS - TIME_DECREASE_BETWEEN_SAUCERS * _saucersOnLevel;

            if (time < MINIMUM_TIME_BETWEEN_SAUCERS)
            {
                return MINIMUM_TIME_BETWEEN_SAUCERS;
            }

            return time;
        }

        private void AbortCurrentRequest()
        {
            if (_currentSaucerRequest != null)
            {
                _currentSaucerRequest.NewSaucer -= InvokeNewSaucer;
                _currentSaucerRequest.Abort = true;
                _currentSaucerRequest = null;
            }
        }

        public event EventHandler NewSaucer;
    }
}
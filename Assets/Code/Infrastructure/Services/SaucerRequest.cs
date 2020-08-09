using System;
using System.Threading.Tasks;

namespace Code.Infrastructure.Services
{
    public class SaucerRequest
    {
        public bool Abort = false;

        public event EventHandler NewSaucer;
        
        private int _time;

        public SaucerRequest(int time)
        {
            _time = time;
        }
        
        public async void Request()
        {
            await Task.Delay(_time);
            if (Abort)
            {
                return;
            }

            NewSaucer?.Invoke(this, null);
        }
    }
}
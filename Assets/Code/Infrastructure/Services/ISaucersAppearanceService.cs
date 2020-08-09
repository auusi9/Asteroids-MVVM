using System;

namespace Code.Infrastructure.Services
{
    public interface ISaucersAppearanceService
    {
        void StartLevel();
        void EndLevel();
        void GameOver();

        event EventHandler NewSaucer;
    }
}
using Code.Infrastructure.MainMenu;
using Code.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Code.Presentation.UI.MainMenu
{
    public class HighScoresView : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private IHighScoreService _highScoreService;
        [Inject] private HighScoreElement.Factory _factory;

        private async void Start()
        {
            HighScoreDto[] highScoreDtos = await _highScoreService.GetTopTenScoresSorted();
            HighScores(highScoreDtos);
        } 
        
        private void OnDestroy()
        {
        }

        private void HighScores(HighScoreDto[] highScores)
        {
            for (int i = 0; i < highScores.Length; i++)
            {
                _factory.Create(i+1, highScores[i], transform);
            }
        }
    }
}
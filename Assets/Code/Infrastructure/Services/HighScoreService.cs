using System;
using System.Threading.Tasks;
using Code.Infrastructure.MainMenu;
using Services.Ports;

namespace Code.Infrastructure.Services
{
    public class HighScoreService : IHighScoreService
    {
        private const int TOP_TEN_VALUE = 10;
        private const string SCORE_SAVE_PATH = "scores.data";
        private IPersistenceAdapter<HighScoreDto[]> _persistenceAdapter;

        public HighScoreService()
        {
            _persistenceAdapter = new PersistenceAdapter<HighScoreDto[]>();
        }
        
        public async Task NewScore(HighScoreDto scoreDto)
        {
            HighScoreDto[] scoreDtos = await LoadScoresFromPersistence();
            
            HighScoreDto[] newScores = AddNewScoreToArray(scoreDtos, scoreDto);

            await _persistenceAdapter.SaveData(newScores, SCORE_SAVE_PATH);
        }

        private static HighScoreDto[] AddNewScoreToArray(HighScoreDto[] scoreDtos, HighScoreDto newScore)
        {
            HighScoreDto[] newScores = new HighScoreDto[scoreDtos.Length + 1];
            
            newScores[scoreDtos.Length] = newScore;
            
            for (int i = 0; i < scoreDtos.Length; i++)
            {
                newScores[i] = scoreDtos[i];
            }

            return newScores;
        }

        public async Task<HighScoreDto[]> GetTopTenScoresSorted()
        {
              HighScoreDto[] scores = await LoadScoresFromPersistence();
              
              Array.Sort(scores);

              if (scores.Length <= TOP_TEN_VALUE)
              {
                  return scores;
              }
              
              HighScoreDto[] topTen = new HighScoreDto[TOP_TEN_VALUE];

              for (int i = 0; i < TOP_TEN_VALUE; i++)
              {
                  topTen[i] = scores[i];
              }

              return topTen;
        }

        private async Task<HighScoreDto[]> LoadScoresFromPersistence()
        {
            HighScoreDto[] scores = await _persistenceAdapter.LoadData(SCORE_SAVE_PATH);
            
            if (scores == null)
            {
                return new HighScoreDto[0];
            }

            return scores;
        }
    }
}
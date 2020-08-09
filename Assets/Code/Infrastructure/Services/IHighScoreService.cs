using System.Threading.Tasks;
using Code.Infrastructure.MainMenu;

namespace Code.Infrastructure.Services
{
    public interface IHighScoreService
    {
        Task NewScore(HighScoreDto scoreDto);
        Task<HighScoreDto[]> GetTopTenScoresSorted();
    }
}
namespace Code.Infrastructure.Services
{
    public class AsteroidsPerLevelService : IAsteroidsPerLevelService
    {
        public const int INITIAL_LEVEL_ASTEROIDS = 5;
        
        public int Get(int level)
        {
            return INITIAL_LEVEL_ASTEROIDS + (level - 1);
        }
    }
}
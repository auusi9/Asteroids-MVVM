using Code.Presentation.Game.Saucer;

namespace Code.Domain.Game
{
    public class RandomSaucerHelper : IRandomSaucerHelper
    {
        public SaucerType GetRandomSaucer()
        {
            System.Random rnd = new System.Random();
            if (rnd.Next(2) == 0)
            {
                return SaucerType.Small;
            }

            return SaucerType.Big;
        }
    }

    public interface IRandomSaucerHelper
    {
        SaucerType GetRandomSaucer();
    }
}
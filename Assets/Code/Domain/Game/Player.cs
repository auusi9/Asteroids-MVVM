namespace Code.Domain.Game
{
    public class Player : IPlayer
    {
        public long Score { get; private set; }
        public long Life { get; private set; }
        public string Name { get; private set; }

        public Player(long initialLives)
        {
            Score = 0;
            Life = initialLives;
        }

        public void SetName(string newName)
        {
            Name = newName;
        }

        public void SpaceshipDamaged()
        {
            Life--;
        }

        public void AddScore(long score)
        {
            long newScore = Score + score;

            long extraLivesNewScore = newScore / 10000;
            long extraLivesGiven = Score / 10000;
            if (extraLivesNewScore > extraLivesGiven)
            {
                Life += extraLivesNewScore - extraLivesGiven;
            }

            Score = newScore;
        }
    }
}
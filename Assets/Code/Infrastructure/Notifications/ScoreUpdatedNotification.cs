namespace Code.Infrastructure.Notifications
{
    public class ScoreUpdatedNotification
    {
        public long Score;

        public ScoreUpdatedNotification(long score)
        {
            Score = score;
        }
    }
}
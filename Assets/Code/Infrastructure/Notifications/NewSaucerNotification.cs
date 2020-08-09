using Code.Presentation.Game.Saucer;

namespace Code.Infrastructure.Notifications
{
    public class NewSaucerNotification
    {
        public SaucerType SaucerType;

        public NewSaucerNotification(SaucerType saucerType)
        {
            SaucerType = saucerType;
        }
    }
}
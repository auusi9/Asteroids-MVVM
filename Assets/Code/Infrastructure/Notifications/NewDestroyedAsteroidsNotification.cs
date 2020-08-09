using Code.Infrastructure.Game;

namespace Code.Infrastructure.Notifications
{
    public class NewDestroyedAsteroidsNotification
    {
        public NewDestroyedAsteroidDto NewDestroyedAsteroidDto;

        public NewDestroyedAsteroidsNotification(NewDestroyedAsteroidDto newDestroyedAsteroidDto)
        {
            NewDestroyedAsteroidDto = newDestroyedAsteroidDto;
        }
    }
}
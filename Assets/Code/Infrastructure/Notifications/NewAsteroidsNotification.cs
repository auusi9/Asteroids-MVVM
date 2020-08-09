using Code.Infrastructure.Game;

namespace Code.Infrastructure.Notifications
{
    public class NewAsteroidsNotification
    {
        public NewAsteroidsDto NewAsteroidsDto;

        public NewAsteroidsNotification(NewAsteroidsDto newAsteroidsDto)
        {
            NewAsteroidsDto = newAsteroidsDto;
        }
    }
}

namespace Code.Infrastructure.Game.Interfaces
{
    public interface IGameOverViewModel
    {
        void NameChangedCommand(string newName);
        void SaveNewScoreCommand();
    }
}
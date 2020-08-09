namespace Code.Domain.Game
{
    public interface IPlayer
    {
        long Score { get; }
        long Life { get; }
        
        string Name { get; }

        void SetName(string newName);
        void SpaceshipDamaged();
        void AddScore(long score);
    }
}
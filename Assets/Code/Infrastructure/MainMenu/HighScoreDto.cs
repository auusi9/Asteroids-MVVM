using System;

namespace Code.Infrastructure.MainMenu
{
    [Serializable]
    public class HighScoreDto : IComparable<HighScoreDto>
    {
        public string Name { get; }
        public long Score { get; }

        public HighScoreDto(string name, long score)
        {
            Name = name;
            Score = score;
        }

        public int CompareTo(HighScoreDto highScoreDto)
        {
            return highScoreDto.Score.CompareTo(Score); 
        }

    }
}
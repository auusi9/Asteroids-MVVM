using Code.Infrastructure.MainMenu;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Presentation.UI.MainMenu
{
    public class HighScoreElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _positionText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        [Inject]
        public void Construct(int position, HighScoreDto highScoreDto, Transform parent)
        {
            transform.SetParent(parent, false);
            
            _positionText.SetText(position.ToString());
            _nameText.SetText(highScoreDto.Name);
            _scoreText.SetText(highScoreDto.Score.ToString());
        }
        
        public class Factory : PlaceholderFactory<int, HighScoreDto, Transform, HighScoreElement>
        {
        }
    }
}
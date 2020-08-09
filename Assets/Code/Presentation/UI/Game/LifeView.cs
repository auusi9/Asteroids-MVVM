using Code.Presentation.Game.Projectile;
using UnityEngine;
using Zenject;

namespace Code.Presentation.UI.Game
{
    public class LifeView : MonoBehaviour
    {
        public class Pool : MonoMemoryPool<LifeView>
        {
            protected override void Reinitialize(LifeView projectile)
            {
            }
        }
    }
}
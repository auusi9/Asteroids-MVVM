using UnityEngine;

namespace Code.Configurations
{
    [CreateAssetMenu(fileName = "SpaceShipConfiguration", menuName = "Configurations/SpaceShipConfiguration", order = 1)]
    public class SpaceShipConfiguration : ScriptableObject
    {
        [SerializeField] private float _impulseForce;
        [SerializeField] private float _rotationDegrees;
        [SerializeField] private long _lives;
        [SerializeField] private float _projectileVelocity;
        [SerializeField] private float _projectileLife;
        [SerializeField] private float _maxVelocity;

        public float ImpulseForce => _impulseForce;

        public float RotationDegrees => _rotationDegrees;

        public long Lives => _lives;

        public float ProjectileVelocity => _projectileVelocity;
        public float ProjectileLife => _projectileLife;

        public float MaxVelocity => _maxVelocity;
    }
}
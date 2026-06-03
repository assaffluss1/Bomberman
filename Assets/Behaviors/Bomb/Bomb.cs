using Behaviors.Player;

namespace Behaviors.Bomb
{
    public class Bomb : BombBase
    {
        private PlayerBombManager _playerManager;
        
        private readonly float _explodeTime = 2.9f;

        void Start()
        {
            Invoke(nameof(TriggerExplosion), _explodeTime);
        }
        public void SetOwner(PlayerBombManager manager)
        {
            _playerManager = manager;
        }

        protected override void TriggerExplosion()
        {
            base.TriggerExplosion();
            if (_playerManager != null)
            {
                _playerManager.OnBombExploded();
            }
        }
        public void ChangeExplosionRadius(int newRadius)
        {
            explosionRadius = newRadius;
        }
    }
}
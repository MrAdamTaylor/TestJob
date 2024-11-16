using MyScripts.Infrastructure.Factory;
using UnityEngine;

namespace MyScripts.Logic
{
    public class SpawnerComponent : MonoBehaviour
    {
        [SerializeField] private float _interval;
        [SerializeField] private GameObject _moveTarget;

        private IFactory _factory;
        private float _lastSpawn = -1;

        public void Construct(float interval, IFactory factory)
        {
            _interval = interval;
            _factory = factory;
        }

        void Update()
        {
            if (Time.time > _lastSpawn + _interval)
            {
                Debug.Log("Spawn");
                _lastSpawn = Time.time;
            }
        }
    }
}

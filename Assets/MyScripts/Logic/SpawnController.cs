using MyScripts.Infrastructure.Factory;
using MyScripts.Infrastructure.ServiceLocator;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Logic
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private float _interval;
        [SerializeField] private GameObject _moveTarget;

        private IFactory _factory;
        private float _lastSpawn = -1;
        private NpcCharacteristics _npcCharacteristics;
        private GameObject _parent;
        private bool _isFinish;

        public void Construct(float interval, IFactory factory, NpcCharacteristics npcCharacteristics, bool isFinish)
        {
            _interval = interval;
            _factory = factory;
            _npcCharacteristics = npcCharacteristics;
            _parent = (GameObject)ServiceLocator.Instance.GetData(typeof(GameObject));
            _isFinish = isFinish;
        }

        void Update()
        {
            if (Time.time > _lastSpawn + _interval)
            {
                GameObject npc = _factory.Create(_npcCharacteristics, _parent.transform);
                _lastSpawn = Time.time;
            }
        }
    }
}

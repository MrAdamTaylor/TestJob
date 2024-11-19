using System.Collections;
using UnityEngine;

namespace MyScripts.Logic
{
    public class SelfDestroyByTime : MonoBehaviour
    {
        private const int SECONDS_BY_DESTROYED = 10;

        [SerializeField] private float _seconds;

        public void Construct(float secondsByDestoying)
        {
            _seconds = secondsByDestoying;
        }

        void Start()
        {
            if (_seconds.Equals(0f))
            {
                _seconds = SECONDS_BY_DESTROYED;
            }

            StartCoroutine(SelfDestruct());
        }

        IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(_seconds);
            Destroy(gameObject);
        }
    }
}
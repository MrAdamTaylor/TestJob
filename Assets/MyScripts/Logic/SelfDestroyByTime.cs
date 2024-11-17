using System.Collections;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class SelfDestroyByTime : MonoBehaviour
    {
        private const int SECONDS_BY_DESTROYED = 3;

        [SerializeField] private float _seconds;
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
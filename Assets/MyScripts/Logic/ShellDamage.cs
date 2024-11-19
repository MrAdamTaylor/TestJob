using MyScripts.Logic.Monster;
using UnityEngine;

namespace MyScripts.Logic
{
    public class ShellDamage : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public void Construct(int damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.gameObject.tag == Constants.MONSTER_TAG)
            {
                MonsterHealth enemyHealth = collision.gameObject.GetComponent<MonsterHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.DealDamage(_damage);
                }
            }
        }
    }
}
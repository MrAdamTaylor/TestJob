using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class CannonShootSystem : MonoBehaviour, ISubscribeAction
    {
        private const float DELAY = 1f;
        
        [SerializeField] private Transform _turret;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _shootTarget;

        public bool CanShoot { get; private set; }

        private IFactory _bulletFactory;
        private float _bulletSpeed;
        private float _koef = 1f;
        private bool _canShoot;
        private float _delay;
        private ShellStaticData _bulletStaticData;
        private GameObject _parent;

        public void Construct(IFactory factory, ShellStaticData data, Transform shootPoint, Transform turret)
        {
            _turret = turret;
            _shootPoint = shootPoint;
            _bulletFactory = factory;
            _bulletStaticData = data;
            _parent = (GameObject)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameObject));
            _delay = DELAY;
            _bulletSpeed = data.Speed;
        }

        private void Update()
        {
            CanShoot = _canShoot;
            
            if(_shootTarget == null)
                return;

            if (CanShoot)
            {
                _delay -= Time.deltaTime;
                float? angle = RotateTurret();
                if (angle != null && _delay <= 0.0f) {
                    CreateBullet();
                    _delay = DELAY;
                }
            }
        }

        private void CreateBullet()
        {
            GameObject shell = _bulletFactory.Create(_bulletStaticData, _shootPoint);
        }

        public void EnableAction()
        {
            _canShoot = true;
        }

        public void DisableAction()
        {
            _canShoot = false;
        }

        public void GetTarget(Transform obj)
        {
            _shootTarget = obj;
        }

        private float? RotateTurret()
        {
            float? angle = CalculateAngle(true);

            if (angle != null)
            {
                _turret.localEulerAngles = new Vector3(0.0f + (float)angle, 0.0f, 0.0f);
            }
            else
            {
                _turret.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }

            return angle;
        }

        private float? CalculateAngle(bool isLowAngle)
        {
            Vector3 targetDir = _shootTarget.transform.position - _turret.position;
            float y = targetDir.y;
            if (y < 0)
            {
                _koef = -1f;
            }

            targetDir.y = 0.0f;
            float x = targetDir.magnitude;
            float gravity = 9.8f;
            float sSqr = _bulletSpeed * _bulletSpeed;
            float underTheSqrRoot = (sSqr * sSqr) - _koef*(gravity * (gravity * x * x + 2 * y * sSqr));
            
            
            if (underTheSqrRoot >= 0.0f)
            {
                float root = Mathf.Sqrt(underTheSqrRoot);
                float highAngle = sSqr + root;
                float lowAngle = sSqr - root;

                if (isLowAngle) return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
                else return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            } 
            else
                return null;
        }
    }
}
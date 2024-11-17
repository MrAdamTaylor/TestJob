using UnityEngine;
using UnityEngine.Serialization;

namespace MyScripts.Logic
{
    public class CannonRotate : MonoBehaviour
    {
        private const float DELAY = 1f;
        private const float MAX_DEGREE = 360f;

        [SerializeField] private float _rotateSpeed = 5f;
        [SerializeField] private float _bulletSpeed = 5f;
        [SerializeField] private Transform _turretMain;
        [SerializeField] private Transform _turretCannon;
        [SerializeField] private GameObject _bulletPrefab;
        
        public bool IsCanRotate { get; set; }

        private Transform _shootPosition;
        private Transform _shootTarget;
        
        
        private bool _rotateIsRational;
        private Vector3 _default;
        private float _delay;
        private float _angleShift = 0f;

        public void Construct(Transform turretHead, float rotateSpeed)
        {
            _rotateSpeed = rotateSpeed;
            _turretMain = turretHead;
            _default = _turretMain.forward + new Vector3(0,0,1);
            _delay = DELAY;
            
        }
        
        public void Construct(Transform turretHead, float rotateSpeed, Transform turretCannon, GameObject bullet, Transform shootPosition)
        {
            _rotateSpeed = rotateSpeed;
            _turretMain = turretHead;
            _default = _turretMain.forward + new Vector3(0,0,1);
            _delay = DELAY;

            _bulletPrefab = bullet;
            _shootPosition = shootPosition;
            _turretCannon = turretCannon;
        }

        public void Update()
        {
            IsCanRotate = _rotateIsRational;
            
            if(_shootTarget == null)
                return;
            if (IsCanRotate)
            {
                _delay -= Time.deltaTime;
                Vector3 direction = (_shootTarget.position - _turretMain.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
                _turretMain.rotation = Quaternion.Slerp(_turretMain.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
                float? angle =RotateTurret();
                if (angle != null && _delay <= 0.0f) {
                    CreateBullet();
                    _delay = DELAY;
                }
            }
            else
            {
                Quaternion lookRotation = Quaternion.LookRotation(_default);
                _turretMain.rotation = Quaternion.Slerp(_turretMain.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
            }
        }

        private void CreateBullet()
        {
            GameObject shell = Instantiate(_bulletPrefab, _shootPosition.position, _shootPosition.rotation);
            shell.AddComponent<Rigidbody>().velocity = _bulletSpeed * _turretMain.forward;
        }

        public void DisableRotate()
        {
            _rotateIsRational = false;
        }

        public void EnableRotate()
        {
            _rotateIsRational = true;
        }
        
        private float? RotateTurret()
        {
            float? angle = CalculateAngle(false);

            if (angle != null)
            {
                //_angleShift = (float)angle;
                _turretCannon.localEulerAngles = new Vector3(0.0f + (float)angle, 0.0f, 0.0f);
                //_turretHead.localEulerAngles = new Vector3(0 + (float)angle, _turretHead.rotation.y, _turretHead.rotation.z);

            }
            else
            {
                _turretCannon.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }

            return angle;
        }

        private float? CalculateAngle(bool isLowAngle)
        {
            Vector3 targetDir = _shootTarget.transform.position - _turretMain.transform.position;
            float y = targetDir.y;
            targetDir.y = 0.0f;
            float x = targetDir.magnitude - 1.0f;
            float gravity = 9.8f;
            float sSqr = _bulletSpeed * _bulletSpeed;
            float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);
            
            
            if (underTheSqrRoot >= 0.0f)
            {
                //float absUnderTheSqrRoot = Mathf.Abs(underTheSqrRoot);
                //float root = Mathf.Sqrt(absUnderTheSqrRoot);
                float root = Mathf.Sqrt(underTheSqrRoot);
                float highAngle = sSqr + root;
                float lowAngle = sSqr - root;

                if (isLowAngle) return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
                else return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            } 
            else
                return null;
        }

        public void GetTarget(Transform obj)
        {
            _shootTarget = obj;
        }
    }
}
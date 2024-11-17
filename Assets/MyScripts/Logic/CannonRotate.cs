using UnityEngine;

namespace MyScripts.Logic
{
    public class CannonRotate : MonoBehaviour
    {
        private const float DELAY = 0.2f;
        private const float MAX_DEGREE = 360f;

        [SerializeField] private float _rotateSpeed = 5f;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private Transform _turretHead;

        public bool IsCanRotate { get; set; }

        private Transform _shootTarget;
        private bool _rotateIsRational;
        private Vector3 _default;


        public void Construct(Transform turretHead, float rotateSpeed)
        {
            _rotateSpeed = rotateSpeed;
            _turretHead = turretHead;
            _default = _turretHead.forward + new Vector3(0,0,1);
        }

        public void Update()
        {
            IsCanRotate = _rotateIsRational;
            
            if(_shootTarget == null)
                return;
            if (IsCanRotate)
            {
                Vector3 direction = (_shootTarget.position - _turretHead.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
                _turretHead.rotation = Quaternion.Slerp(_turretHead.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
            }
            else
            {
                Quaternion lookRotation = Quaternion.LookRotation(_default);
                _turretHead.rotation = Quaternion.Slerp(_turretHead.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
            }
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
                _turretHead.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
            }

            return angle;
        }

        private float? CalculateAngle(bool isLowAngle)
        {
            throw new System.NotImplementedException();
        }

        public void GetTarget(Transform obj)
        {
            _shootTarget = obj;
        }
    }
}
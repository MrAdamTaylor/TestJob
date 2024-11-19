using MyScripts.Infrastructure.Factory;
using MyScripts.Logic.Monster;
using UnityEngine;

namespace MyScripts.Logic.Cannon
{
    public class CannonRotate : MonoBehaviour, ISubscribeAction
    {
        private const float DELAY = 1f;
        private const float MAX_DEGREE = 360f;

        [SerializeField] private float _rotateSpeed = 5f;
        [SerializeField] private float _bulletSpeed = 5f;
        [SerializeField] private Transform _turretMain;
        [SerializeField] private Transform _turretCannon;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private bool _isPredicted;

        public bool IsCanRotate { get; set; }

        private Transform _shootPosition;
        private Transform _shootTarget;


        private bool _rotateIsRational;
        private Vector3 _default;
        private float _delay;
        private float _angleShift = 0f;
        private float _koef = 1f;
        private MonsterController _controller;
        private Vector3 PredictedShift = new Vector3(0,0,0);
        private float PredictedMyltiply = 1f;

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
            _isPredicted = true;
        }

        public void Update()
        {
            IsCanRotate = _rotateIsRational;
            
            if(_shootTarget == null)
                return;
            if (IsCanRotate)
            {
                if (_isPredicted && _controller != null)
                {
                    PredictedShift = _controller.PredictedShift;
                    PredictedMyltiply = Constants.PREDICTED_MULTIPLY;
                }
                _delay -= Time.deltaTime;
                Vector3 direction = ((_shootTarget.position + PredictedShift*PredictedMyltiply) - _turretMain.position).normalized;
                //Debug.Log($" New Vector {new Vector3(direction.x, 0.0f, direction.z)} Predicted: {PredictedShift}");
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
                _turretMain.rotation = Quaternion.Slerp(_turretMain.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
            }
            else
            {
                Quaternion lookRotation = Quaternion.LookRotation(_default);
                _turretMain.rotation = Quaternion.Slerp(_turretMain.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
            }
        }

        public void EnableAction()
        {
            _rotateIsRational = true;
        }

        public void DisableAction()
        {
            _rotateIsRational = false;
        }

        public void GetTarget(Transform obj)
        {
            _shootTarget = obj;
            _controller = _shootTarget.GetComponent<MonsterController>();
        }
    }
}
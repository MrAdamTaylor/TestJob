using System;
using UnityEditor;
using UnityEngine;

namespace MyScripts.EnterpriceLogic
{
    public class ReactionTrigger : MonoBehaviour
    {
        public Action TriggerAction;
        public Action TriggerEndAction;
        
        private Transform _goalTransform;
        [SerializeField] private float _radius;
        private bool _isTriggered;


        public void Construct(float radius, Transform goalTransform = null)
        {
            _radius = radius;
            _goalTransform = goalTransform;
        }

        private void OnDrawGizmos()
        {
            if (_goalTransform is null)
            {
                Handles.color = Color.blue;
                Handles.DrawWireDisc(this.transform.position, Vector3.up, _radius);    
                return;
            }
            else
            {
                Handles.color = CheckTrigger(_goalTransform.position)? Color.green: Color.red;
                Handles.DrawWireDisc(this.transform.position, Vector3.up, _radius);
            }
        }

        public void ResetTrigger()
        {
            _goalTransform = null;
        }

        private void FixedUpdate()
        {
            if (_goalTransform is null)
                return;
        
            _isTriggered = CheckTrigger(_goalTransform.position);
            if (_isTriggered)
                TriggerAction?.Invoke();
            else
            {
                TriggerEndAction?.Invoke();
            }
        }

        private bool CheckTrigger(Vector3 goalTransformPosition)
        {
       
        
            Vector3 dirToTargetWorld = (goalTransformPosition - transform.position);
            Vector3 flatDirToTarget = transform.InverseTransformVector(dirToTargetWorld);
            flatDirToTarget.y = 0;
            float flatDistance = flatDirToTarget.magnitude;
            if (flatDistance > _radius)
                return false;
            else
            {
                return true;
            }
        }
    }
}

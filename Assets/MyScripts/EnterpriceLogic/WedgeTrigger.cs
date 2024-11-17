using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


public class WedgeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _height = 1;

    public Action TriggerAction;
    public Action TriggerEndAction;
    
    [FormerlySerializedAs("Angle Thresh")]
    [Range(0, 1)] 
    [SerializeField] private float _angThresh = 0.5f;

    private bool _isTriggered;

    public void Construct(float radius, float height, float angThresh, Transform provoceuter=null)
    {
        _radius = radius;
        _height = height;
        _angThresh = angThresh;
        _target = provoceuter;
    }

    private void OnDrawGizmos()
     { 
         Gizmos.color = Handles.color = Contains(_target.position) ? Color.red : Color.white;
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
        Vector3 top = new Vector3(0, this._height, 0);
        
        Handles.DrawWireDisc(default, Vector3.up, _radius);
        Handles.DrawWireDisc(top, Vector3.up, _radius);
        
        float p = _angThresh;
        float x = Mathf.Sqrt(1 - p * p);

        Vector3 vRight = new Vector3(-x, 0, p) * _radius;
        Vector3 vLeft = new Vector3(x, 0, p) * _radius;
        
        Gizmos.DrawRay(default, vLeft);
        Gizmos.DrawRay(default, vRight);
        
        Gizmos.DrawRay(top, vLeft);
        Gizmos.DrawRay(top, vRight);
        
        Gizmos.DrawLine(default, top);
        Gizmos.DrawLine(vLeft, top + vLeft);
        Gizmos.DrawLine(vRight, top + vRight);
     }

    private void FixedUpdate()
    {
        _isTriggered = Contains(_target.position);
        if(_isTriggered)
            TriggerAction?.Invoke();
        else
        {
            TriggerEndAction?.Invoke();
        }
    }

    public void SetTarget(Transform provoceuter)
    {
        _target = provoceuter;
    }


    private bool Contains(Vector3 position)
   {
       //NOTE - трансформация из глобального в локальное пространство
       Vector3 dirToTargetWorld = (position - transform.position);
       Vector3 vecToTarget = transform.InverseTransformVector(dirToTargetWorld);
       Vector3 dirToTarget = vecToTarget.normalized;

       //NOTE - проверка высоты
       if (vecToTarget.y < 0 || vecToTarget.y > _height)
           return false;

       //NOTE - проверка находимся ли мы внутри угла
       Vector3 flatDirToTarget = vecToTarget;
       flatDirToTarget.y = 0;
       float flatDistance = flatDirToTarget.magnitude;
       flatDirToTarget /= flatDistance;
       if (flatDirToTarget.z < _angThresh)
           return false;

       //NOTE - проверка на то, находимся ли мы в цилиндре
       if (flatDistance > _radius)
           return false;
       
       return true;
   }
}

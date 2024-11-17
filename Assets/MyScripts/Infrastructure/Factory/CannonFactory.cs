using System;
using MyScripts.Data;
using MyScripts.Data.Blackboard;
using MyScripts.Infrastructure.AssertService;
using MyScripts.Logic;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class CannonFactory : IFactory
    {
        private IAssert _assert;
        private ObjectData _objectData;
        private Blackboard _blackboard;
    
        public CannonFactory(IAssert assert, ObjectData data)
        {
            _assert = assert;
            _objectData = data;
        }

        public GameObject Create(ScriptableObject configs,  Transform parent)
        {
            if (configs.GetType() != typeof(CannonCharacteristics))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }
        
            CannonCharacteristics cannonCharacteristics = (CannonCharacteristics)configs;
        
            Debug.Log("Cannon Position: "+_objectData.PositionData);
            GameObject obj = _assert.Assert(_objectData.ModelData, _objectData.PositionData, parent);
            WedgeTrigger wedgeTrigger = obj.AddComponent<WedgeTrigger>();

            //GameObject provoceuter = (GameObject)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameObject));
            wedgeTrigger.Construct(
                cannonCharacteristics.TriggerConfigs.Radius, 
                cannonCharacteristics.TriggerConfigs.High ,
                cannonCharacteristics.TriggerConfigs.AngThresh
                );
            CannonController cannonController = obj.AddComponent<CannonController>();
            //cannonController.Construct(cannonCharacteristics.RotateSpeed);

            Transform cannonTower = obj.transform.Find(Constants.CANNON_TOWER_NAME);
            Transform cannonMoving = cannonTower.Find(Constants.CANNON_MOVING_NAME);
            Transform shootPoint = cannonMoving.Find(cannonCharacteristics.ShootPoinName);
            CannonRotate cannonRotate = obj.AddComponent<CannonRotate>();

            //Transform shootPoint = cannonTower.transform.Find(cannonCharacteristics.ShootPoinName);
            cannonRotate.Construct(cannonTower,cannonCharacteristics.RotateSpeed, cannonMoving, cannonCharacteristics.ProjectTilePrefab, shootPoint);
            wedgeTrigger.TriggerAction += cannonRotate.EnableRotate;
            wedgeTrigger.TriggerEndAction += cannonRotate.DisableRotate;
            
            cannonController.Construct(wedgeTrigger, cannonRotate);
            return obj;
        }

        
    }
}
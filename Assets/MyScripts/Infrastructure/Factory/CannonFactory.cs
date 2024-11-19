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
        
            GameObject obj = _assert.Assert(_objectData.ModelData, _objectData.PositionData, parent);
            WedgeTrigger wedgeTrigger = obj.AddComponent<WedgeTrigger>();

            wedgeTrigger.Construct(
                cannonCharacteristics.TriggerConfigs.Radius, 
                cannonCharacteristics.TriggerConfigs.High ,
                cannonCharacteristics.TriggerConfigs.AngThresh
                );
            CannonController cannonController = obj.AddComponent<CannonController>();

            Transform cannonTower = obj.transform.Find(Constants.CANNON_TOWER_NAME);
            Transform cannonMoving = cannonTower.Find(Constants.CANNON_MOVING_NAME);
            Transform shootPoint = cannonMoving.Find(cannonCharacteristics.ShootPoinName);
            CannonRotate cannonRotate = obj.AddComponent<CannonRotate>();
            CannonShootSystem cannonShootSystem = obj.AddComponent<CannonShootSystem>();
            cannonRotate.Construct(cannonTower,cannonCharacteristics.RotateSpeed, cannonMoving, cannonCharacteristics.ProjectTilePrefab, shootPoint);
            wedgeTrigger.TriggerAction += cannonRotate.EnableAction;
            wedgeTrigger.TriggerEndAction += cannonRotate.DisableAction;
            wedgeTrigger.TriggerAction += cannonShootSystem.EnableAction;
            wedgeTrigger.TriggerEndAction += cannonShootSystem.DisableAction;

            IAssert assert = (IAssert)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IAssert));
            IDataProvider dataProvider = (IDataProvider)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IDataProvider));
            ShellStaticData shellCharacteristics = Resources.Load<ShellStaticData>("StaticData/Shell");
            
            shellCharacteristics.Turret = shootPoint;
            shellCharacteristics.Position = shootPoint.position;
            ObjectData data = dataProvider.CreateData(shellCharacteristics, false);
            IFactory factory = new ShellFactory(assert, data);
            
            cannonShootSystem.Construct(factory, shellCharacteristics, shootPoint, cannonMoving);
            
            cannonController.Construct(wedgeTrigger, cannonRotate, cannonShootSystem);
            return obj;
        }

        
    }
}
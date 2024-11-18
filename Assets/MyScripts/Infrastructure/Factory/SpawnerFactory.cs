using System;
using MyScripts.Data;
using MyScripts.Infrastructure.AssertService;
using MyScripts.Logic;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class SpawnerFactory : IFactory
    {
        private IAssert _assert;
        private ObjectData _objectData;

        public SpawnerFactory(IAssert assert, ObjectData objectData)
        {
            _assert = assert;
            _objectData = objectData;
        }

        public GameObject Create(ScriptableObject configs,  Transform parent)
        {
            if (configs.GetType() != typeof(SpawnerCharacteristics))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }

            GameObject finish = null;
            ReactionTrigger reactionTrigger = null;
            bool isMoveGoal = false;
            
            SpawnerCharacteristics characteristics = (SpawnerCharacteristics)configs;
            GameObject obj = _assert.Assert(_objectData.ModelData, _objectData.PositionData, parent);

            if (characteristics.IsFinish)
            {
                finish = GameObject.Find(characteristics.FinishPointName);
                reactionTrigger = finish.AddComponent<ReactionTrigger>();
                reactionTrigger.Construct(characteristics.FinishRadius);
                isMoveGoal = true;
                ServiceLocator.ServiceLocator.Instance.BindData(typeof(ReactionTrigger), reactionTrigger);
            }

            if (characteristics.IsEnemy)
            {
                SpawnController spawner = obj.AddComponent<SpawnController>();
                IAssert assert = (IAssert)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IAssert));
                IDataProvider dataProvider = (IDataProvider)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IDataProvider));
                NpcCharacteristics npcCharacteristics = Resources.Load<NpcCharacteristics>("StaticData/NPC");
                npcCharacteristics.IsMoveGoalExist = isMoveGoal;
                ObjectData data = dataProvider.CreateData(npcCharacteristics);
                IFactory factory = new NpcFactory(assert, data);
                spawner.Construct(characteristics.Interval, factory, npcCharacteristics, characteristics.IsFinish);
            }

            return obj;
        }
    }
}
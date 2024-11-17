using System;
using MyScripts.Data;
using MyScripts.Infrastructure.AssertService;
using MyScripts.Logic;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class NpcFactory : IFactory
    {
        private const float TRIGGER_END_RADIUS = 2f;
        
        
        private IAssert _assert;
        private ObjectData _data;
        

        public NpcFactory(IAssert assert, ObjectData data)
        {
            _assert = assert;
            _data = data;
        }

        public GameObject Create(ScriptableObject configs,  Transform parent)
        {
            if (configs.GetType() != typeof(NpcCharacteristics))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }

            NpcCharacteristics npcConfigs = (NpcCharacteristics)configs;
            GameObject obj = _assert.Assert(_data.ModelData, _data.PositionData);
            Rigidbody rigidbody = obj.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
            
            MonsterController controller = obj.AddComponent<MonsterController>();
            

            if (npcConfigs.IsMoveGoalExist)
            {
                ReactionTrigger reactionTrigger =
                    (ReactionTrigger)ServiceLocator.ServiceLocator.Instance.GetData(typeof(ReactionTrigger));
                //reactionTrigger.TriggerAction += 
                reactionTrigger.Construct(TRIGGER_END_RADIUS, obj.transform);
                MoveTo moveTo = obj.AddComponent<MoveTo>();
                moveTo.Construct(npcConfigs.Speed, reactionTrigger.transform);

                DestroyerNPC destroyerNpc = obj.AddComponent<DestroyerNPC>();
                reactionTrigger.TriggerAction += destroyerNpc.WinDestroy;
                
                controller.Construct(moveTo, reactionTrigger, destroyerNpc);
                
                Debug.Log("NPC have a Goal");
            }
            else
            {
                MoveDirection moveTo = obj.AddComponent<MoveDirection>();
                moveTo.Construct(npcConfigs.Speed);
                controller.Construct(moveTo);
                obj.AddComponent<SelfDestroyByTime>();

            }
            
            //controller.Construct();

            return obj;
        }
        
        
    }
}


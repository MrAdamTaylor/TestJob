using MyScripts.Data.Blackboard;
using MyScripts.EnterpriceLogic;
using MyScripts.Infrastructure.ServiceLocator;
using UnityEngine;

namespace MyScripts.Logic.Monster
{
    public class MonsterController : MonoBehaviour, IBlackboard
    {
        public Vector3 PredictedShift { get; private set; }

        private Blackboard _blackboard;
        private GameObject _parent;
        private IMoveComponent _moveComponent;
        private DestroyerNPC _destroyerNpc;
        private ReactionTrigger _trigger;
        private MonsterHealth _monsterHealth;
    
        public void Construct(IMoveComponent moveComponent, MonsterHealth monsterHealth, DestroyerNPC destroyerNpc)
        {
            _parent = (GameObject)ServiceLocator.Instance.GetData(typeof(GameObject));
            gameObject.transform.SetParent(_parent.transform);
            CreateAIData();
            _moveComponent = moveComponent;
            _moveComponent.DestinationAction += Finish;
            _moveComponent.Move();
            _destroyerNpc = destroyerNpc;
            _monsterHealth = monsterHealth;
        }

        public void Construct(IMoveComponent moveComponent, ReactionTrigger trigger, DestroyerNPC destroyerNpc, MonsterHealth health)
        {
            _parent = (GameObject)ServiceLocator.Instance.GetData(typeof(GameObject));
            gameObject.transform.SetParent(_parent.transform);
            CreateAIData();
            _moveComponent = moveComponent;
            _moveComponent.DestinationAction += Finish;
            _moveComponent.Move();
            _destroyerNpc = destroyerNpc;
            _trigger = trigger;
            _trigger.TriggerAction += _destroyerNpc.WinDestroy;
            _destroyerNpc.IsDestroying += Finish;
            _monsterHealth = health;
        }

        private void OnDestroy()
        {
            _blackboard.Set(EBlackboardKey.CannonFocus, _parent);
            if(_blackboard.WedgeTrigger != null)
                _blackboard.WedgeTrigger.TriggerAction -= IsTriggered;
        }

        private void Update()
        {
            PredictedShift = (_moveComponent.GetDirection() * _moveComponent.Speed);
        }

        public void CreateAIData()
        {
            if (ServiceLocator.Instance.IsGetData(typeof(Blackboard)))
            {
                _blackboard = (Blackboard)ServiceLocator.Instance.GetData(typeof(Blackboard));
                if (_blackboard.WedgeTrigger != null)
                {
                    _blackboard.WedgeTrigger.TriggerAction += IsTriggered;
                    _blackboard.Set(EBlackboardKey.CannonFocus, gameObject);
                }
            }
            else
            {
                _blackboard = new Blackboard();
                ServiceLocator.Instance.BindData(typeof(Blackboard), _blackboard);
            }
        }

        private void IsTriggered()
        {
            //Debug.Log("Cannon Territory");
        }

        private void Finish()
        {
            Debug.Log("<color=yellow>Finish</color>");
            _moveComponent.StopMove();
            _blackboard.RemoveByKey(EBlackboardKey.CannonFocus);
            if (_trigger != null)
            {
                _trigger.TriggerAction -= _destroyerNpc.WinDestroy;
                _trigger.ResetTrigger();
            }
            _monsterHealth.HealthLessZero -= _destroyerNpc.DeathDestroy;
            Destroy(gameObject);
        }
    }
}
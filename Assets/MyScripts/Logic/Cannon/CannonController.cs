using MyScripts.Data.Blackboard;
using MyScripts.EnterpriceLogic;
using MyScripts.Infrastructure.ServiceLocator;
using UnityEngine;

namespace MyScripts.Logic.Cannon
{
    public class CannonController : MonoBehaviour, IBlackboard
    {
        private Blackboard _blackboard;
        private WedgeTrigger _wedgeTrigger;
        private CannonRotate _cannonRotate;
        private CannonShootSystem _cannonShootSystem;
        
        public void Construct(WedgeTrigger wedgeTrigger, CannonRotate cannonRotate, CannonShootSystem shootSystem)
        {
            _wedgeTrigger = wedgeTrigger;
            CreateAIData();
            _cannonRotate = cannonRotate;
            _cannonShootSystem = shootSystem;
            _blackboard.ReadyForShoot += _cannonRotate.GetTarget;
            _blackboard.ReadyForShoot += _cannonShootSystem.GetTarget;
        }

        public void CreateAIData()
        {
            if (ServiceLocator.Instance.IsGetData(typeof(Blackboard)))
            {
                _blackboard = (Blackboard)ServiceLocator.Instance.GetData(typeof(Blackboard));
            }
            else
            {
                _blackboard = new Blackboard(_wedgeTrigger);
                ServiceLocator.Instance.BindData(typeof(Blackboard), _blackboard);
            }
            
        }

        public void OnDestroy()
        {
            _wedgeTrigger.TriggerAction -= _cannonRotate.EnableAction;
            _wedgeTrigger.TriggerEndAction -= _cannonRotate.DisableAction;

            _wedgeTrigger.TriggerAction -= _cannonShootSystem.EnableAction;
            _wedgeTrigger.TriggerEndAction -= _cannonShootSystem.DisableAction;

            _blackboard.ReadyForShoot -= _cannonRotate.GetTarget;
            _blackboard.ReadyForShoot -= _cannonShootSystem.GetTarget;
        }
    }
}
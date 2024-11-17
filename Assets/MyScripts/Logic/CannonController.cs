using MyScripts.Data.Blackboard;
using MyScripts.Infrastructure.ServiceLocator;
using UnityEngine;

namespace MyScripts.Logic
{
    public class CannonController : MonoBehaviour, IBlackboard
    {
        private Blackboard _blackboard;
        private WedgeTrigger _wedgeTrigger;
        private CannonRotate _cannonRotate;
        
        public void Construct(WedgeTrigger wedgeTrigger, CannonRotate cannonRotate)
        {
            _wedgeTrigger = wedgeTrigger;
            CreateAIData();
            _cannonRotate = cannonRotate;
            _blackboard.ReadyForShoot += _cannonRotate.GetTarget;
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
    }
}
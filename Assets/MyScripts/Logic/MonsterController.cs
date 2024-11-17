using MyScripts.Data.Blackboard;
using MyScripts.Infrastructure.Factory;
using MyScripts.Infrastructure.ServiceLocator;
using MyScripts.Logic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IBlackboard
{
    private Blackboard _blackboard;
    private GameObject _parent;
    private IMoveComponent _moveComponent;
    private DestroyerNPC _destroyerNpc;
    private ReactionTrigger _trigger;
    
    public void Construct(IMoveComponent moveComponent)
    {
        _parent = (GameObject)ServiceLocator.Instance.GetData(typeof(GameObject));
        gameObject.transform.SetParent(_parent.transform);
        CreateAIData();
        _moveComponent = moveComponent;
        _moveComponent.DestinationAction += Finish;
        _moveComponent.Move();
    }

    public void Construct(IMoveComponent moveComponent, ReactionTrigger trigger, DestroyerNPC destroyerNpc)
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
    }

    private void OnDestroy()
    {
        _blackboard.Set(EBlackboardKey.CannonFocus, _parent);
        _blackboard.WedgeTrigger.TriggerAction -= IsTriggered;
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
        _trigger.TriggerAction -= _destroyerNpc.WinDestroy;
        _trigger.ResetTrigger();
        Destroy(gameObject);
    }
}

public interface IBlackboard
{
    void CreateAIData();
}

using MyScripts.Data.Blackboard;
using MyScripts.Infrastructure.ServiceLocator;
using UnityEngine;

public class MonsterLogic : MonoBehaviour, IBlackboard
{
    private Blackboard _blackboard;
    private GameObject _parent;

    public void Construct()
    {
        _parent = (GameObject)ServiceLocator.Instance.GetData(typeof(GameObject));
        gameObject.transform.SetParent(_parent.transform);
        CreateAIData();
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
        Debug.Log("Cannon Territory");
    }
}

public interface IBlackboard
{
    void CreateAIData();
}

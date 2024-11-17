using System.Collections.Generic;
using UnityEngine;

namespace MyScripts.Data.Blackboard
{
    public class Blackboard
    {
        public WedgeTrigger WedgeTrigger { get; private set; }

        private Dictionary<EBlackboardKey, GameObject> _gameObjValue = new Dictionary<EBlackboardKey, GameObject>();

        public Blackboard()
        {
            
        }

        public Blackboard(WedgeTrigger wedgeTrigger)
        {
            WedgeTrigger = wedgeTrigger;
            Debug.Log($"<color=green>Trigger is add</color>");
        }

        public bool TryGet(EBlackboardKey blackboardKey, out GameObject value, GameObject defaultValue = null)
        {
            if (_gameObjValue.ContainsKey(blackboardKey))
            {
                value = _gameObjValue[blackboardKey];
                return true;
            }

            value = defaultValue;
            return false;
        }

        public GameObject GetGameObject(EBlackboardKey blackboardKey)
        {
            if (!_gameObjValue.ContainsKey(blackboardKey))
                throw new System.ArgumentException($"Key not found for {blackboardKey} in GameObjects");
            return _gameObjValue[blackboardKey];
        }

        public void Set(EBlackboardKey blackboardKey, GameObject value)
        {
            _gameObjValue[blackboardKey] = value;
            if (blackboardKey == EBlackboardKey.CannonFocus)
            {
                WedgeTrigger.SetTarget(value.transform);
            }
        }
    }
}

public enum EBlackboardKey
{
    CannonFocus,
    Unknown
}
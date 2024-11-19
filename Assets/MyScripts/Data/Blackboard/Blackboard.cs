using System;
using System.Collections.Generic;
using MyScripts.EnterpriceLogic;
using UnityEngine;

namespace MyScripts.Data.Blackboard
{
    public class Blackboard
    {
        
        public WedgeTrigger WedgeTrigger { get; private set; }

        public Action<Transform> ReadyForShoot; 
        
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
            if (blackboardKey == EBlackboardKey.CannonFocus && WedgeTrigger != null)
            {
                WedgeTrigger.SetTarget(value.transform);
                ReadyForShoot?.Invoke(value.transform);
            }
        }

        public void RemoveByKey(EBlackboardKey cannonFocus)
        {
            if (_gameObjValue.ContainsKey(cannonFocus))
            {
                _gameObjValue.Remove(cannonFocus);
            }

        }
    }
}
using System;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
     [SerializeField]private int _currentHealt;

     public Action HealthLessZero;
     
     private int _maxHealt;

     public void Construct(int health)
     {
          _maxHealt = health;
          _currentHealt = health;
     }

     public void DealDamage(int value)
     {
          _currentHealt -= value;
          if (_currentHealt <= 0)
          {
               Debug.Log($"<color=red>I am hited!</color>");
               HealthLessZero?.Invoke();
          }
     }
}

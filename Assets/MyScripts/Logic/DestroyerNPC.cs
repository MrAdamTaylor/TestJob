using System;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class DestroyerNPC : MonoBehaviour
    {
        public Action IsDestroying { get; set; }

        public void WinDestroy()
        {
            IsDestroying?.Invoke();
            Destroy(gameObject);
        }
        
        public void DeathDestroy()
        {
            Debug.Log($"<color=red>NPC death: </color>");
            IsDestroying?.Invoke();
            Destroy(gameObject);
        }
    }
}
using System;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class DestroyerNPC : MonoBehaviour
    {
        public Action IsDestroying { get; set; }

        public void WinDestroy()
        {
            Debug.Log($"NPC to moving in end: ");
            IsDestroying?.Invoke();
            Destroy(gameObject);
        }
    }
}
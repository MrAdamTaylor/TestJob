using System;
using MyScripts.Data;
using MyScripts.Infrastructure.AssertService;
using MyScripts.Logic;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class ShellFactory : IFactory
    {
        private IAssert _assert;
        private ObjectData _objectData;
        
        public ShellFactory(IAssert assert, ObjectData data)
        {
            _assert = assert;
            _objectData = data;
        }

        public GameObject Create(ScriptableObject configs, Transform parent)
        {
            if (configs.GetType() != typeof(ShellStaticData))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }
            
            ShellStaticData shellCharacteristics = (ShellStaticData)configs;
            GameObject obj = _assert.Assert(_objectData.ModelData, parent.transform.position);
            obj.AddComponent<Rigidbody>().velocity = shellCharacteristics.Speed * shellCharacteristics.Turret.forward;
            ShellDamage damage = obj.AddComponent<ShellDamage>();
            damage.Construct(shellCharacteristics.Damage);

            if (shellCharacteristics.IsSelfDestory)
            {
                SelfDestroyByTime selfDestroy = obj.AddComponent<SelfDestroyByTime>();
                selfDestroy.Construct(shellCharacteristics.SecondsByDestoying);
            }

            return obj;
        }
    }
}
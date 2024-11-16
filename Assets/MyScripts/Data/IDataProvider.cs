using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Data
{
    public interface IDataProvider
    {
        public ObjectData CreateData(ObjectStaticData characteristics);

        public Vector3 GetPositionByData(ObjectStaticData characteristics);
    }
}
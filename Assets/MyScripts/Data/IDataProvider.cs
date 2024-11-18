using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Data
{
    public interface IDataProvider
    {
        public ObjectData CreateData(ObjectStaticData characteristics, bool cordinates = true);

        public Vector3 GetPositionByData(ObjectStaticData characteristics);
    }
}
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Data
{
    public class StringDataProvider : IDataProvider
    {
        public ObjectData CreateData(ObjectStaticData objectCharacteristics)
        {
            string gameObject = objectCharacteristics.PrefabPath;
            Vector3 position = GetPositionByData(objectCharacteristics);
            ObjectData data = new ObjectData(gameObject, position);
            return data;
        }

        public Vector3 GetPositionByData(ObjectStaticData data)
        {
            if (data.IsUseVector)
            {
                Vector3 position = data.Position;
                return position;
            }
            else
            {
                GameObject gameObject = GameObject.Find(data.ScenePointName);
                Vector3 position = gameObject.transform.position;
                return position;
            }
        }
    }
}
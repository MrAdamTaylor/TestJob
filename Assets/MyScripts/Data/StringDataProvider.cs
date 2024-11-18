using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Data
{
    public class StringDataProvider : IDataProvider
    {
        public ObjectData CreateData(ObjectStaticData objectCharacteristics, bool coordinates = true)
        {
            string gameObject = objectCharacteristics.PrefabPath;
            Vector3 position;
            position = coordinates ? GetPositionByData(objectCharacteristics) : objectCharacteristics.Position;
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
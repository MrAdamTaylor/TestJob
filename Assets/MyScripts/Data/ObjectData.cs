using UnityEngine;

namespace MyScripts.Data
{
    public class ObjectData
    {
        public object ModelData { get; private set; }

        public Vector3 PositionData { get; private set; }

        public ObjectData(object modelData, Vector3 positionData)
        {
            ModelData = modelData;
            PositionData = positionData;
        }
    }
}
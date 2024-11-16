using System.Collections.Generic;
using MyScripts.Data;
using MyScripts.EnterpriceLogic;
using MyScripts.Infrastructure;
using MyScripts.Infrastructure.AssertService;
using MyScripts.Infrastructure.Factory;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Logic
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private List<GameEntityStaticData> _entityStatic;
    
        private IAssert _assert;
        private IDataProvider _dataProvider;
        private SpawnerFactory _spawnerFactory;

        private List<IFactory> _factories = new List<IFactory>(); 

        private EntityServiceCreater _entityServiceCreater;
    
    
        public void Start()
        {
            CommonConfigs commonConfigs = Resources.Load<CommonConfigs>("StaticData/CommonConfigs");
            _entityServiceCreater = new EntityServiceCreater();
            if (commonConfigs.IsLoadByName)
            {
                _assert = new AssertByString();
                _dataProvider = new StringDataProvider();
            }
            else
            {
                _assert = new AssertByObject();
                _dataProvider = new ObjectDataProvider();
            }

            GameObject parent = GameObject.Find(SceneConstants.GAME_OBJECTS);
        
            for (int i = 0; i < _entityStatic.Count; i++)
            {
                IFactory factory = _entityServiceCreater.Create(_entityStatic[i], _assert, _dataProvider);
                factory.Create(_entityStatic[i], parent.transform);
            }
        }
    }
}
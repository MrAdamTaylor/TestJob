using System;
using MyScripts.Data;
using MyScripts.Infrastructure.AssertService;
using MyScripts.Infrastructure.Factory;
using MyScripts.StaticData;

namespace MyScripts.Infrastructure
{
    public class EntityServiceCreater
    {
        public IFactory Create(GameEntityStaticData gameEntityStaticData, IAssert assert, IDataProvider dataProvider)
        {
            IFactory factory = null;
            switch (gameEntityStaticData)
            {
                case CannonCharacteristics cannonCharacteristics:
                    ObjectData cannonData = dataProvider.CreateData(cannonCharacteristics);
                    factory = new CannonFactory(assert, cannonData);
                    break;
                case SpawnerCharacteristics spawnerCharacteristics:
                    ObjectData spawnerData = dataProvider.CreateData(spawnerCharacteristics);
                    factory = new SpawnerFactory(assert, spawnerData);
                    break;
                case null:
                    throw new Exception("Entity Data is null");
            }

            if (factory is null)
                throw new Exception("Factory is null");
            return factory;
        }
    }
}
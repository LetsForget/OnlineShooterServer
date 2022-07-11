using GameLogic;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class ServerSpawnSystem : IEcsSystem
    {
        private readonly EcsWorld _world = null;

        public void Spawn(int clientId)
        {
            ref var spawnComponent = ref _world.NewEntity().Get<SpawnComponent>();
            spawnComponent.clientId = clientId;
            spawnComponent.position = Vector3.zero;
        }
    }
}
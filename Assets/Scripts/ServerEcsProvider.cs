using GameLogic;
using RiptideNetworking;
using UnityEngine;

public class ServerEcsProvider : BaseEcsProvider
{
    private PlayerUpdateSystem<ServerMovementUpdate, ServerPlayerComponent> playerInputUpdateSystem;
    private SpawnSystem<ServerPlayerComponent> spawnSystem;
    
    public Server Server
    {
        set
        {
            systems.Inject(value);
            systems.Init();
            inited = true;
        }
    }

    public void SpawnPlayer(ushort clientId)
    {
        spawnSystem.ServerSpawn(clientId, Vector3.zero);
    }

    public void DestroyPlayer(ushort clientId)
    {
        spawnSystem.Destroy(clientId);
        playerInputUpdateSystem.OnDestroyPlayer(clientId);
    }

    public void AddUpdate(ServerMovementUpdate update)
    {
        playerInputUpdateSystem.AddUpdate(update);
    }

    protected override void AddSystems()
    {
        base.AddSystems();
        systems.Add(spawnSystem = new SpawnSystem<ServerPlayerComponent>())
            .Add(playerInputUpdateSystem = new PlayerUpdateSystem<ServerMovementUpdate, ServerPlayerComponent>())
            .Add(new ServerMovementSystem())
            .Add(new ServerSendSystem())
            .Add(new ClientIdSetterSystem<ServerPlayerComponent>());
    }
}
using GameLogic;
using Leopotam.Ecs;
using RiptideNetworking;
using Systems;

public class ServerEcsProvider : BaseEcsProvider
{
    private ServerSpawnSystem serverSpawnSystem;
    
    public Server Server
    {
        set
        {
            systems.Inject(value);
            systems.Init();
            inited = true;
        }
    }

    protected override void AddSystems()
    {
        serverSpawnSystem = new ServerSpawnSystem();
        
        systems.Add(serverSpawnSystem);
        
        base.AddSystems();
    }

    public void SpawnPlayer(int clientId)
    {
        serverSpawnSystem.Spawn(clientId);
    }
}
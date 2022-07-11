using GameLogic;
using Leopotam.Ecs;
using RiptideNetworking;
using UnityEngine;


public class ServerEcsProvider : BaseEcsProvider
{
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
        SpawnSystem.Spawn(clientId, Vector3.zero);
    }

    public void DestroyPlayer(ushort clientId)
    {
        SpawnSystem.Destroy(clientId);
    }

    public void AddUpdate(CharacterMovementUpdate update)
    {
        UpdateReceiveSystem.AddUpdate(update);
    }
}
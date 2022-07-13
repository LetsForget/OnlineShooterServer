using System.Linq;
using GameLogic;
using RiptideNetworking;
using UnityEngine;

namespace Network
{
    public class ServerHandler : MonoBehaviour
    {
        [SerializeField] private ServerEcsProvider ecsProvider;
        
        public Server Server { get; private set; }
 
        private void Start()
        {
            Server = new Server();
            ecsProvider.Server = Server;
            
            Server.ClientConnected += OnClientConnected;
            Server.ClientDisconnected += OnClientDisconnected;
            Server.MessageReceived += OnMessageReceived;
        }
        
        private void Update()
        {
            Server.Tick();
        }

        private void OnDestroy()
        {
            if (Server.IsRunning)
            {
                Server.Stop();
            }
        }
        
        private void OnClientConnected(object sender, ServerClientConnectedEventArgs e)
        {
            foreach (var player in ecsProvider.PlayersList.list)
            {
                var playerKey = player.Key;
                var spawnMessage = PlayerSpawnMessage.Create(ref playerKey);
                
                Server.Send(spawnMessage, e.Client.Id);
            }
            
            var clientId = e.Client.Id;
            
            Server.SendToAll(PlayerSpawnMessage.Create(ref clientId));
            ecsProvider.SpawnPlayer(clientId);
        }
        
        private void OnClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            var clientId = e.Id;
            
            Server.SendToAll(PlayerDestroyMessage.Create(ref clientId));
            ecsProvider.DestroyPlayer(e.Id);
        }

        public void OnServerStop()
        {
            var keys = ecsProvider.PlayersList.list.Keys.ToList();
            foreach (var player in keys)
            {
                ecsProvider.DestroyPlayer(player);
            }
        }
        
        private void OnMessageReceived(object sender, ServerMessageReceivedEventArgs e)
        {
            switch (e.MessageId)
            {
                case 1:
                {
                    var playerInputUpdate = ServerMovementUpdateMessage.Convert(e.Message);                    
                    ecsProvider.AddUpdate(playerInputUpdate);
                    break;
                }
            }
        }
    }
}
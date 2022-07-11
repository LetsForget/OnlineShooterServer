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
            Server.ClientDisconnected += ServerOnClientDisconnected;
            Server.MessageReceived += OnMessageReceived;
        }
        
        private void FixedUpdate()
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
            var clientId = e.Client.Id;
            
            Server.SendToAll(PlayerSpawnMessage.Create(ref clientId));
            ecsProvider.SpawnPlayer(clientId);
        }
        
        private void ServerOnClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            var clientId = e.Id;
            
            Server.SendToAll(PlayerDestroyMessage.Create(ref clientId));
            ecsProvider.DestroyPlayer(e.Id);
        }
        
        private void OnMessageReceived(object sender, ServerMessageReceivedEventArgs e)
        {
            switch (e.MessageId)
            {
                case 1:
                {
                    var characterMovementUpdate = CharacterMovementUpdateMessage.Convert(e.Message);
                    ecsProvider.AddUpdate(characterMovementUpdate);
                    Server.SendToAll(e.Message);
                    break;
                }
            }
        }
    }
}
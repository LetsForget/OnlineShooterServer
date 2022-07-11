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
            ecsProvider.SpawnPlayer(e.Client.Id);
        }
        
        private void OnMessageReceived(object sender, ServerMessageReceivedEventArgs e)
        {

        }
    }
}
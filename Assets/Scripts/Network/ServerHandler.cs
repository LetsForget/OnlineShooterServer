using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
namespace Network
{
    public class ServerHandler : MonoBehaviour
    {
        [SerializeField] private ServerEcsProvider ecsProvider;
        
        public Server Server { get; private set; }
 
        private void Start()
        {
            Server = new Server();
            
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

        }
        
        private void OnMessageReceived(object sender, ServerMessageReceivedEventArgs e)
        {

        }
    }
}
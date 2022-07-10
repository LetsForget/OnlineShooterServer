using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
namespace Network
{
    public class ServerLauncher : MonoBehaviour
    {
        private const int MAX_PLAYERS = 8;
        
        [SerializeField] private TMP_InputField portField;
        
        [SerializeField] private Button RunStopButton;
        [SerializeField] private TMP_Text RunStopText;
        
        public Server Server { get; private set; }
 
        private void Start()
        {
            Server = new Server();
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
            
            RunStopButton.onClick.AddListener(RunStopServer);
        }

        private void RunStopServer()
        {
            if (Server.IsRunning)
            {
                Server.Stop();
                
                portField.interactable = true;
                RunStopText.text = "Run";
            }
            else
            {
                if (portField.text.Length == 0)
                {
                    return;
                }
                
                var port = Convert.ToUInt16(portField.text);
                Server.Start(port, MAX_PLAYERS);
                
                portField.interactable = false;
                RunStopText.text = "Stop";
            }
        }

        private void OnDestroy()
        {
            if (Server.IsRunning)
            {
                Server.Stop();
            }
        }
    }
}
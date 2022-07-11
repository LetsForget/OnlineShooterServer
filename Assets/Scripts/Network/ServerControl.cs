using System;
using RiptideNetworking.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class ServerControl : MonoBehaviour
    {
        private const int MAX_PLAYERS = 8;
        
        [SerializeField] private ServerHandler serverHandler;
        
        [SerializeField] private TMP_InputField portField;

        [SerializeField] private Button RunStopButton;
        [SerializeField] private TMP_Text RunStopText;

        private void Start()
        {
            RunStopButton.onClick.AddListener(RunStopServer);
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        }
        
        private void RunStopServer()
        {
            if (serverHandler.Server.IsRunning)
            {
                serverHandler.Server.Stop();
                
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
                serverHandler.Server.Start(port, MAX_PLAYERS);

                portField.interactable = false;
                RunStopText.text = "Stop";
            }
        }
    }
}
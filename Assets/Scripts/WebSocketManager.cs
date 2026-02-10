using UnityEngine;
using NativeWebSocket;
using UnityEngine.Events;
using System;

public class WebSocketManager : MonoBehaviour
{
     private WebSocket websocket;
    public string serverIP = "XXX.XXX.XXX.XXX"; // Replace with your server's IP address
    public int serverPort = 8081; // Replace with your server's port number (8081 is the default)

    [Range(0, 255)]
    public int ledIntensity = 0;

    async void Start()
    {
        websocket = new WebSocket("ws://" + serverIP + ":" + serverPort);

        //Runs when connected to the server
        websocket.OnOpen += async () =>
        {
            Debug.Log("Connected to WebSocket server");
            string UUID = SystemInfo.deviceUniqueIdentifier; // Certain devices block MAC address access for privacy reasons so we send a UUID instead

            await websocket.SendText("Device (Unity):" + SystemInfo.deviceName + " ... Device's Unique Identifier: " + UUID);
        };

        //Runs when a message is received from the server
        websocket.OnMessage += (bytes) =>
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received: " + message);

            IncomingMessageParser(message);

        };

        //Runs when disconnected from the server
        websocket.OnClose += (code) =>
        {
            Debug.Log("WebSocket closed");
        };

        await websocket.Connect();
    }

    void Update()
    {
            websocket.DispatchMessageQueue();
    }

    public void IncomingMessageParser(String msg)
    {
        string valueParsed = msg.Substring(msg.IndexOf(":") + 1);

        if(msg.Contains("button")) {
            if(valueParsed == "1") 
            {
                //do something if button pressed
                Debug.Log("ESP32 Button Pressed");
            }
            if(valueParsed == "0") 
            {
                //do something if button released
                Debug.Log("ESP32 Button Released");
            }

        }

    }

    public async void SendHello()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("Hello from Unity");
            Debug.Log("Sent: Hello from Unity");
        }
        else
        {
            Debug.LogWarning("WebSocket not connected");
        }
    }

}

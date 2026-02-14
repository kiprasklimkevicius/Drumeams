using UnityEngine;
using NativeWebSocket;
using UnityEngine.Events;
using System;

public class WebSocketManager : MonoBehaviour
{
     private WebSocket websocket;
    public string serverIP = "XXX.XXX.XXX.XXX"; // Replace with your server's IP address
    public int serverPort = 8081; // Replace with your server's port number (8081 is the default)

    IObserver audioManager = null;
    IObserver particleManager = null;


    [Range(0, 255)]
    public int ledIntensity = 0;

    async void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        particleManager = GameObject.Find("ParticleSystemManager").GetComponent<ParticleSystemManager>();

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

        if(msg.Contains("fan")) {
            int blowForce = int.Parse(valueParsed);
            Debug.Log("fan, value: " + blowForce);
            if(blowForce >= 1) 
            {
                audioManager.OnBlowDetect(blowForce);
                particleManager.OnBlowDetect(blowForce);
            }
            if(blowForce == 0)
            {
                audioManager.OnBlowFinished();
                particleManager.OnBlowFinished();
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

    public async void DrumHit(int drumVelocity)
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {

            await websocket.SendText("DRUM_HIT:" + drumVelocity); //TODO: add remap/ dynamic
            Debug.Log("Sent: DRUM_HIT:255");
        }
        else
        {
            Debug.LogWarning("WebSocket not connected");
        }
    }

}

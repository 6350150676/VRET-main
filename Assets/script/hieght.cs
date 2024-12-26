using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
public class HeightSender : MonoBehaviour
{
    // public float forceAmount = 11.0f; \
    // private UdpClient udpClient;
    // private IPEndPoint targetEndpoint;
    private float playerHeight;

    // Replace with the actual IP address of your ESP8266 device
    // private string esp8266IpAddress = "192.168.122.161";  // Adjust IP address
    // private int port = 2000; // Replace with your desired port number

    void Start()
    {
        // udpClient = new UdpClient();
        // targetEndpoint = new IPEndPoint(IPAddress.Parse(esp8266IpAddress), port);

    }

    void Update()
    {
        // Get player height from your game logic (replace with your method)
        playerHeight = GetPlayerHeight()+20.0f;

         StartCoroutine(Result());
        
            
        
            
        
    }

    private  float GetPlayerHeight()
    {
        // Implement your logic to get the player's height from your game
        // This example assumes you have a variable or method to retrieve it
        return transform.position.y/10.0f; // Replace with your actual height retrieval
    }
    private  IEnumerator Result(){
        yield return new WaitForSeconds(4);
        Debug.Log(" height data:"+ playerHeight+"m");

    }


}

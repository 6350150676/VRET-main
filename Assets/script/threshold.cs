using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using  UnityEngine.SceneManagement;
using System.Collections;
public class UDPListener : MonoBehaviour
{
      public float forceMagnitude = 10f;

    private Rigidbody rb;

  
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint = null; // Initialize to null
    private float heartbeatThreshold=85.0f; // Set your desired threshold value here
    public string nextSceneName = "END 1"; // Name of the scene to switch to (optional)
    public Camera mainCamera; // Reference to the main camera
    public Camera secondaryCamera; // Reference to the secondary camera
   // Adjust the force strength as needed
//    public usicontroller UI;


    private Queue<Action> cameraSwitchQueue = new Queue<Action>();

    void Awake()
    {
        // GetComponent<Rigidbody>().AddForce(Vector3.up * forceAmount, ForceMode.Force);
  
     // Example threshold (adjust as needed)

        // Create UDP client with appropriate port number
         rb = GetComponent<Rigidbody>();
        udpClient = new UdpClient(2000); // Change port if different from ESP8266

        // Start listening for incoming data asynchronously
        Thread thread = new Thread(ReceiveCallback);
        thread.Start();
    }

    void ReceiveCallback()
    {
        try
        {
            while (true)
            {
                // Receive data asynchronously
                byte[] receivedBytes = null;
                try
                {
                    receivedBytes = udpClient.Receive(ref remoteEndPoint);
                }
                catch (SocketException ex)
                {
                    Debug.LogError("UDP communication error: " + ex.Message);
                    continue; // Skip to next iteration on error
                }

                if (receivedBytes != null)
                {
                    string receivedData = Encoding.ASCII.GetString(receivedBytes);

                    // Process data
                    Debug.Log("Received Heartbeat: " + receivedData);

                    // Extract and parse heartbeat value
                    float heartbeatValue = float.Parse(receivedData);

                    // Check if heartbeat exceeds threshold
                    if (heartbeatValue > heartbeatThreshold)
                    {
                        Debug.LogWarning("Heartbeat exceeded threshold!");

                        // Queue the camera switch action for execution on main thread
                        cameraSwitchQueue.Enqueue(delegate()
                        {
                            if (mainCamera != null)
                            {
                                mainCamera.enabled = !mainCamera.enabled;
                                if (secondaryCamera != null)
                                {
                                    secondaryCamera.enabled = !secondaryCamera.enabled;
                                }
                            }
                        });

                        // Optional scene switching (if nextSceneName is set)
                        if (nextSceneName != null && nextSceneName != "")
                        {
                            cameraSwitchQueue.Enqueue(delegate()
                            {
                                try
                                {
                                    StartCoroutine(Switch());
                                }
                                catch (System.Exception ex)
                                {
                                    Debug.LogError("Scene loading error: " + ex.Message);
                                }
                            });
                        }
                        else
                        {
                            // Stop simulation logic (replace with your implementation)
                            cameraSwitchQueue.Enqueue(delegate()
                            {
                                Time.timeScale = 0.0f; // Example: Pause simulation time
                            });
                        }

                        break; // Exit the loop to prevent further processing
                    }
                }
            }
        }
        finally
        {
            // Close the UDP client when the thread exits
            udpClient.Close();
        }
    }
     
    void Update()
    {
       rb.AddForce(Vector3.up * forceMagnitude, ForceMode.Impulse);
        // Process camera switch requests queued from background thread
        while (cameraSwitchQueue.Count > 0)
        {
            Action cameraSwitchAction = cameraSwitchQueue.Dequeue();
            cameraSwitchAction.Invoke();
        }
    }
    private IEnumerator Switch(){
    // waits for 2 seconds
        yield return new WaitForSeconds(3);
      SceneManager.LoadScene(nextSceneName);

     }
    void OnDestroy()
    {
        // Close UDP client when script is destroyed
        udpClient.Close();
    }
}
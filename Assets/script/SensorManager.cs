using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class SensorManager : MonoBehaviour
{
    public float sensorValue; // Stores the current sensor value
    public float thresholdValue = 70.0f; // Adjust the threshold based on your sensor's output
    public string nextSceneName = "Scene2"; // Name of the scene to switch to

    void Update()
    {
        // Update sensorValue based on your sensor reading method (replace this)
        sensorValue = GetSensorValue(); // Replace with your sensor reading logic

        // Check if sensor value exceeds threshold
        if (sensorValue > thresholdValue)
        {
            SwitchToNextScene();
        }
    }

    float GetSensorValue()
    {
        // Implement sensor reading logic here (e.g., using UDPListener or other methods)
        // This example placeholder assumes a value between 0 and 100
      return Random.Range(0.0f, 100.0f);
    }

   public void SwitchToNextScene()
    {
        try
        {
            SceneManager.LoadScene(nextSceneName);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Scene loading error: " + ex.Message);
            // Handle scene loading error (optional, e.g., display an error message)
        }
    }
}
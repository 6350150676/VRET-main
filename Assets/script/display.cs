using UnityEngine;
using UnityEngine.UI;

public class HeartRateDisplay : MonoBehaviour
{
    Text heartRateText;

    void Start()
    {
        // Find the Text component
        heartRateText = GetComponent<Text>();
    }

    // Method to update the displayed heart rate value
    public void UpdateHeartRate(float heartRate)
    {
        heartRateText.text = "Heart Rate: " + heartRate.ToString() + " bpm";
    }
}

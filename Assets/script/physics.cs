using UnityEngine;

public class physics : MonoBehaviour
{
    public float forceAmount = 7.0f; // Adjust the force strength as needed
    

    void Update()
    {
        // Option 1: Apply force in world Y direction
        GetComponent<Rigidbody>().AddForce(Vector3.up * forceAmount, ForceMode.Force);


        // Option 2: Apply force in object's local Y direction (relative to object's rotation)
        // This might be useful if your object has its own orientation
        // GetComponent<Rigidbody>().AddForce(transform.up * forceAmount, ForceMode.Force);
    }
}
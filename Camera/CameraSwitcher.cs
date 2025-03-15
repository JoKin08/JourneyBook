using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera cameraMap; // Reference to the first camera
    public Camera cameraTrip; // Reference to the second camera

    private bool isCameraMapActive = true; // Track which camera is currently active

    void Start()
    {
        // Ensure only one camera is active at the start
        cameraMap.enabled = true;
        cameraTrip.enabled = false;
    }

    void Update()
    {
        // Check if the "M" key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Toggle between the two cameras
            isCameraMapActive = !isCameraMapActive;

            // Enable/disable cameras based on the toggle state
            cameraMap.enabled = isCameraMapActive;
            cameraTrip.enabled = !isCameraMapActive;
        }
    }
}
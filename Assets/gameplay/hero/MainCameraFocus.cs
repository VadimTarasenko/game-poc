using UnityEngine;

public class MainCameraFocus : MonoBehaviour
{
    public Transform target; // Assign the spectre_econ GameObject's Transform here
    public Vector3 offset = new Vector3(0, 10, 0); // Adjust the offset as needed

    void LateUpdate()
    {
        if (target != null)
        {
            // Set the camera position to be above the target with the specified offset
            transform.position = target.position + offset;

            // Make the camera look at the target
            transform.LookAt(target);
        }
    }
}
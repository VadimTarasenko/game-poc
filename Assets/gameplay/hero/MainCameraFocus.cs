using UnityEngine;

public class MainCameraFocus : MonoBehaviour
{
    public Transform target; // Assign the spectre_econ GameObject's Transform here
    public Vector3 offset = new Vector3(0, 10, 0); // Adjust the offset as needed
    public float rotationSpeed = 5f; // Adjust this value to control the speed of the rotation
    
    private float currentYRotation;

    void LateUpdate()
    {
        if (target != null)
        {
            // Set the camera position to be above the target with the specified offset
            transform.position = target.position + offset;

            // Smoothly interpolate the Y rotation
            float targetYRotation = target.eulerAngles.y;
            currentYRotation = Mathf.LerpAngle(currentYRotation, targetYRotation, Time.deltaTime * rotationSpeed);

            // Set the camera rotation
            transform.rotation = Quaternion.Euler(70, currentYRotation, 0);
        }
    }
}
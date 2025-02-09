using UnityEngine;

public class SpinDisplay : MonoBehaviour
{
    [SerializeField] private float rotationSpeed; // Speed of the rotation in degrees per second

    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
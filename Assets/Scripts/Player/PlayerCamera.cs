using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float xMovement, yMovement;
    public float sensitivity;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        UpdateCameraRotation();
    }


    private void UpdateCameraRotation()
    {
        var mouse = InputEvents.Instance.LookDelta;
        float mouseX = mouse.x * sensitivity * Time.fixedDeltaTime;
        float mouseY = mouse.y * sensitivity / 1.5f * Time.fixedDeltaTime;
        //float mouseX = mouse.x * 80 * Time.fixedDeltaTime;
        //float mouseY = mouse.y * 80 * Time.fixedDeltaTime;
        Vector3 rot = transform.localRotation.eulerAngles;
        xMovement = rot.y + mouseX;
        yMovement -= mouseY;
        yMovement = Mathf.Clamp(yMovement, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yMovement, xMovement, 0);
    }
}

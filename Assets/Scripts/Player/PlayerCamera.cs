using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float xMovement, yMovement;
    public float sensitivity;

    [Tooltip("How far can the player look up? Make a negative value!")]
    [SerializeField] float topClamp;

    [Tooltip("How far can the player look down? Make a positive value!")]
    [SerializeField] float bottomClamp;

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
        yMovement = Mathf.Clamp(yMovement, topClamp, bottomClamp);
        transform.localRotation = Quaternion.Euler(yMovement, xMovement, 0);
    }
}

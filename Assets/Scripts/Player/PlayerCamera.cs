using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float xMovement, yMovement;
    private void FixedUpdate()
    {
        UpdateCameraRotation();
    }


    private void UpdateCameraRotation()
    {
        var mouse = InputEvents.Instance.LookDelta;
        //float mouseX = mouse.x * OptionInstance.sensitivity * Time.fixedDeltaTime;
        //float mouseY = mouse.y * OptionInstance.sensitivity * Time.fixedDeltaTime;
        float mouseX = mouse.x * 80 * Time.fixedDeltaTime;
        float mouseY = mouse.y * 80 * Time.fixedDeltaTime;
        Vector3 rot = transform.localRotation.eulerAngles;
        xMovement = rot.y + mouseX;
        yMovement -= mouseY;
        yMovement = Mathf.Clamp(yMovement, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yMovement, xMovement, 0);
    }
}

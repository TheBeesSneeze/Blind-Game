/*
 * A script that anchors a gameobject to another gameobject 
 * but independent of position.y 
 * 
 * Eli
 */
using UnityEngine;

public class AnchorScript : MonoBehaviour
{
    [SerializeField] private Transform AnchorToRotate;
    [SerializeField] private GameObject CameraAnchor;

    private void Start()
    {
        if (CameraAnchor == null)
            CameraAnchor = Camera.main.gameObject;
    }

    void Update()
    {
        Quaternion newRotation = CameraAnchor.transform.rotation;
        AnchorToRotate.rotation = Quaternion.Euler(AnchorToRotate.rotation.eulerAngles.x, newRotation.eulerAngles.y, AnchorToRotate.rotation.eulerAngles.z);

    }
}

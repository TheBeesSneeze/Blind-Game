using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractble : MonoBehaviour, IInteractable
{

    void IInteractable.Interact(GameObject player)
    {
        print("interact");
    }
}

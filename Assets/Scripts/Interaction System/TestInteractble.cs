using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractble : MonoBehaviour, IInteractable
{
    void IInteractable.DisplayInteractUI()
    {
        print("Interact!");
    }

    void IInteractable.HideInteractUI()
    {
        print("Stop Interact");
    }

    void IInteractable.Interact(GameObject player)
    {
        print("am interacting");
    }
}

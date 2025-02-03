using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingSoundInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private ConstantSoundInterval soundObject;


    // Start is called before the first frame update
    void Start()
    {
        if(soundObject == null)
            soundObject = GetComponent<ConstantSoundInterval>(); // if you still get errors past this point its your fault
    }

    public void Interact(GameObject player)
    {
        soundObject.Toggle();
    }

    public void CancelInteract() { }

    public void DisplayInteractUI() {
        CanvasInteractionBehavior.ShowInteractUI.Invoke();
    }

    public void HideInteractUI() 
    {
        CanvasInteractionBehavior.HideInteractUI.Invoke();
    }
}

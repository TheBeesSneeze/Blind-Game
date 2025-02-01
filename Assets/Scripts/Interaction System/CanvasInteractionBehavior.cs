using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInteractionBehavior : MonoBehaviour
{
    [SerializeField] private GameObject interactPrompt;
    public static Action ShowInteractUI;
    public static Action HideInteractUI;

    private void Awake()
    {
        ShowInteractUI += EnableInteractUI;
        HideInteractUI += DisableInteractUI;
    }

    private void EnableInteractUI()
    {
        interactPrompt.SetActive(true);
    }

    private void DisableInteractUI()
    {
        interactPrompt.SetActive(false);
    }
}

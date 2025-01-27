/*
 * Utility script to be used across many different scripts.
 * This script is a middleman for scripts to send information between each other.
 * 
 * ie. 
 * PlayerManager.cs checks if the player is grounded every frame, it invokes events like "WhilePlayerGrounded" accordingly
 * FootstepPlayer.cs waits for the appropriate events to be called to play sounds
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager //:MonoBehaviour
{
    public static UnityEvent OnPlayerDegrounded = new UnityEvent(); // oh my god pls rename this variable wtf is the word for degrounded
    public static UnityEvent OnPlayerGrounded = new UnityEvent();
    public static UnityEvent WhilePlayerGrounded = new UnityEvent();
    public static UnityEvent WhilePlayerNotGrounded = new UnityEvent();
}

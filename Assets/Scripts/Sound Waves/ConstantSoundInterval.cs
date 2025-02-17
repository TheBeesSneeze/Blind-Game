/*
 * Creates sound waves over intervals.
 * Can start activated/ deactivated. 
 * Can be toggled by other scripts via Activate(), Deactivate(), and Toggle()
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class ConstantSoundInterval : MonoBehaviour
{
    [SerializeField] private bool ActiveAtStart;
    [SerializeField] private float secondsBetweenSoundWaves;
    [SerializeField] private SoundWaveProperties wave;

    [ReadOnly] private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        if(ActiveAtStart)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (active)
            return;

        active = true;

        wave.PlayAtPosition(transform.position);
    }

    public void Deactivate()
    {
        if (!active)
            return;

        // this stops the coroutine
        active = false;
    }

    public void Toggle()
    {
        if (active)
            Deactivate();
        else
            Activate();
    }

    private IEnumerator playSoundWaveOverInterval()
    {
        while(active)
        {
            wave.PlayAtPosition(transform.position);

            yield return new WaitForSeconds(secondsBetweenSoundWaves);
        }
    }

    private void OnDrawGizmos()
    {
        SoundWaveManager.DrawSoundWaveGizmos(transform.position, wave);
    }
}

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
using System.Linq;
using System.IO;

public class ConstantSoundInterval : MonoBehaviour
{
    [SerializeField] private bool ActiveAtStart;
    [SerializeField] private float secondsBetweenSoundWaves;
    [SerializeField] private SoundWaveProperties wave;

    [ReadOnly] private bool active = false;

    private List<PermanentOutline> outlines = new List<PermanentOutline>();

    [Tooltip("Will activating this end the game?")]
    public bool FinalActivation;

    [Tooltip("Put the game's ending here! Or something. I don't know.")]
    public GameObject Ending;

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

        if(FinalActivation)
        {

            //end the game here
            Ending.SetActive(true);
            Time.timeScale = 0;

        }

        //outlines = GameObject.FindObjectsOfType<PermanentOutline>().ToList();

        //for (int i = 0; i < outlines.Count; i++)
        //{

        //    GetComponent<PermanentOutline>().EnableOutline();

        //}
    }

    public void Deactivate()
    {
        if (!active)
            return;

        // this stops the coroutine
        active = false;

        outlines = GameObject.FindObjectsOfType<PermanentOutline>().ToList();

        for(int i = 0; i < outlines.Count; i++)
        {

            GetComponent<PermanentOutline>().DisableOutline();

        }

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

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

    [Tooltip("Will activating this end the game?")]
    public bool FinalActivation;

    [ShowIf("FinalActivation")]
    [Tooltip("Put the game's ending here! Or something. I don't know.")]
    public GameObject Ending;

    //insert all of the objects that you want a constant sound source to light up here. i'm so so sorry
    public List<GameObject> outlinedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if(ActiveAtStart)
        {
            Activate();
        }
    }

    private void Update()
    {

        if (active && FinalActivation)
        {

            //end the game here
            if (Ending != null)
                Ending.SetActive(true);
            else
                Debug.LogError("Ending canvas not in " + gameObject.name);
            //Time.timeScale = 0;

        }

    }

    public void Activate()
    {
        if (active)
            return;

        active = true;

        StartCoroutine(playSoundWaveOverInterval());

        //wave.PlayAtPosition(transform.position);

        for(int i = 0; i < outlinedObjects.Count; i++)
        {

            if (outlinedObjects[i] != null)
            {

                if (outlinedObjects[i].GetComponent<PermanentOutline>() != null)
                {
                    outlinedObjects[i].GetComponent<PermanentOutline>().EnabledOutline();
                }

                if (outlinedObjects[i].GetComponent<Outline>() != null)
                {

                    outlinedObjects[i].GetComponent<Outline>().OutlineColor = Color.yellow;

                    outlinedObjects[i].GetComponent<Outline>().enabled = true;
                }

            }
        
        }

    }

    public void Deactivate()
    {
        if (!active)
            return;

        // this stops the coroutine
        active = false;

        wave.PlayAtPosition(transform.position);

        for (int i = 0; i < outlinedObjects.Count; i++)
        {
            if(outlinedObjects[i].GetComponent<PermanentOutline>() != null)
                outlinedObjects[i].GetComponent<PermanentOutline>().DisableOutline();

            if(outlinedObjects[i].GetComponent<Outline>() != null)
                outlinedObjects[i].GetComponent<Outline>().enabled = false;

        }

        //outlines = GameObject.FindObjectsOfType<PermanentOutline>().ToList();

        //for(int i = 0; i < outlines.Count; i++)
        //{
        //    if(TryGetComponent<PermanentOutline>(out PermanentOutline outline))
        //    {
        //        outline.DisableOutline();
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Theres no outline");
        //    }


        //}

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

            yield return new WaitForSeconds(secondsBetweenSoundWaves * 4);
        }
    }

    private void OnDrawGizmos()
    {
        SoundWaveManager.DrawSoundWaveGizmos(transform.position, wave);
    }
}

/*
 * Handles regular footsteps for (WASD)
 * 
 * Eli
 */
using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] SoundWaveProperties soundWave;
    [SerializeField] Transform footstepOriginLeft;
    [SerializeField] Transform footstepOriginRight;
    public int MaxStepsPerSecond;
    private bool leftStep;
    private Rigidbody rb;
    private PlayerManager playerManager;
    private void Start()
    {
        leftStep = false;
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        EventManager.OnPlayerDegrounded.AddListener(EndFootsteps);
        EventManager.OnPlayerGrounded.AddListener(StartFootsteps);
    }
    public void StartFootsteps()
    {
        StartCoroutine(FootStepRoutine());
    }
    public void EndFootsteps()
    {
        StopAllCoroutines();
    }
    private IEnumerator FootStepRoutine()
    {
        while (true)
        {
            //if the player is moving any slower than this, i dont think they moving enough to call footsteps
            if (rb.velocity.magnitude > 1)
            {
                //This number will divide 1 to make a decimal. High maxsteps will result in more FREQUENT steps
                var steps = Mathf.Clamp(rb.velocity.magnitude, 0, MaxStepsPerSecond);
                CallFootstep();
                //print("next step in " + Mathf.Clamp(1f / steps, 0.05f, 0.5f));
                yield return new WaitForSeconds(Mathf.Clamp(1f / steps, 0.05f, 0.5f));
            }
            else
            {
                //print("WAITING");
                yield return new WaitForEndOfFrame();
            }

        }
    }
    public void CallFootstep()
    {
        if(leftStep)
        {
            SoundWaveManager.Instance.CreateSoundWaveAtPosition(footstepOriginLeft.position, soundWave);
            leftStep = false;
        }
        else
        {
            SoundWaveManager.Instance.CreateSoundWaveAtPosition(footstepOriginRight.position, soundWave);
            leftStep= true;
        }
       
    }
}

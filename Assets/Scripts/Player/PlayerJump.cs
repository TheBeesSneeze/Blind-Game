/*
 * Handles regular player jumping (SPACE)
 * 
 * Toby, Eli
 */
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Tooltip("A scaler to help with MJC. If it feels like its not doing anything, increase the other 2")][SerializeField] float jumpPower;
    [Tooltip("How long the player can charge by holding space (in seconds)")][SerializeField] float maxJumpCharge;
    [SerializeField] SoundWaveProperties soundWave;

    private Rigidbody rb;
    private PlayerManager playerManager;
    public AnimationCurve jumpCurve;

    [ReadOnly] public bool chargingJump;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        InputEvents.JumpStarted.AddListener(ChargeJump);
        InputEvents.JumpCanceled.AddListener(ReleaseJump);
        EventManager.OnPlayerGrounded.AddListener(OnPlayerLand);
    }

    private void ChargeJump()
    {
        //if (GrapplingHook.isGrappling){return;}

        if (!chargingJump)
        {
            chargingJump = true;
            StartCoroutine(JumpingCounter());
        }
    }
    IEnumerator JumpingCounter()
    {
        while (chargingJump) 
        {
            time += Time.deltaTime;
            yield return null; 
        }
    }

    private void ReleaseJump()
    {
        float normalizedTime = time / maxJumpCharge;
        float output = jumpCurve.Evaluate(normalizedTime);

        if (playerManager.isGrounded)
        {
            rb.AddForce(Vector3.up * output * jumpPower, ForceMode.Impulse);
        }



        //float localTime = time;
        //float clampedJump = Mathf.Clamp(localTime, 0.75f, maxJumpCharge) * 2;

        //if (playerManager.isGrounded)
        //{
        //    rb.AddForce(Vector3.up * clampedJump * jumpPower, ForceMode.Impulse);
        //}

        StopCoroutine(JumpingCounter());
        time = 0f;
        chargingJump = false;
    }

    private void OnPlayerLand()
    {
        //SoundWaveManager.Instance.CreateSoundWaveAtPosition(rb.position, soundWave);
    }
}

/*
 * Utility script that spawns sound waves at positions. 
 * 
 * -Toby
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveManager : Singleton<SoundWaveManager>
{
    [SerializeField] private GameObject SoundWavePrefab;

    [SerializeField] private Gradient DefaultGradient;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Instantiates a new sound wave at specified position and destroys it after lifetime elapses
    /// </summary>
    public void CreateSoundWaveAtPosition(Vector3 position, Gradient colorOverLifetime, float maxRadius = 1, float lifetime = 5)
    {
        ParticleSystem particle = InstantiateSoundWave(position, colorOverLifetime, maxRadius, lifetime);
        particle.Play(false);

        if(particle.transform.parent != null)
            Destroy(particle.transform.parent.gameObject, lifetime);
        else
            Destroy(particle.gameObject);
    }

    /// <summary>
    /// Instantiates a new sound wave at specified position and destroys it after lifetime elapses
    /// </summary>
    public void CreateSoundWaveAtPosition(Vector3 position, SoundWaveProperties wave)
    {
        CreateSoundWaveAtPosition(position, wave.ColorOverLifetime, wave.MaxRadius, wave.Lifetime);
    }

    /// <summary>
    /// Instantiates a new sound wave at specified position and destroys it after lifetime elapses
    /// </summary>
    public void CreateSoundWaveAtPosition(Vector3 position, float maxRadius = 1, float lifetime = 5)
    {
        CreateSoundWaveAtPosition(position, DefaultGradient, maxRadius, lifetime);
    }

    #region Private Methods
    private ParticleSystem InstantiateSoundWave(Vector3 position, Gradient colorOverLifetime, float maxRadius, float lifetime)
    {
        GameObject wave = Instantiate(SoundWavePrefab, position, Quaternion.identity);
        ParticleSystem particle = wave.GetComponentInChildren<ParticleSystem>();

        // guys particle systems are so annoying

        ParticleSystem.ColorOverLifetimeModule particleColorOverLifetime = particle.colorOverLifetime; // you need to make new variables or else it doesnt work
        particleColorOverLifetime.enabled = true;
        particleColorOverLifetime.color = colorOverLifetime;

        var main = particle.main;
        main.startSize = maxRadius;
        main.startLifetime = lifetime;

        return particle;

    }
    #endregion
}

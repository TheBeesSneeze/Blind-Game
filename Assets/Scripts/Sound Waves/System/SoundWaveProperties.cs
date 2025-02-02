/*
 * Serialized object that represents the settings for a sound wave particle.
 * 
 * Has a method to create a new sound wave from SoundWaveManager
 * 
 * -Toby
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[System.Serializable]
public class SoundWaveProperties
{
    [Min(0)]
    [Tooltip("How far the sound wave will travel")]
    public float MaxRadius = 1;

    [Min(0)]
    [Tooltip("How long it takes the sound wave to reach the max radius")]
    public float Lifetime = 5;

    public Gradient ColorOverLifetime = new Gradient()
    {
        // default example gradient because its important that the color becomes less visible:

        colorKeys = new GradientColorKey[2] {
            new GradientColorKey(new Color(1, 1, 1), 0),
            new GradientColorKey(new Color(1, 1, 1), 1f)
        },
        
        alphaKeys = new GradientAlphaKey[2] {
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(0, 1)
        }
    };

    public void PlayAtPosition(Vector3 position)
    {
        SoundWaveManager.Instance.CreateSoundWaveAtPosition(position, this);
    }
}

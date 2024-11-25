using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    public SoundsArrays[] randomSounds;

    private AudioSource _audioSource => GetComponent<AudioSource>();

    public void PlaySound(int i, float volume = 1f, bool random = false, bool isDestroyed = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        AudioClip clip = random ? randomSounds[i].ClipsSoundArray[Random.Range(0, randomSounds[i].ClipsSoundArray.Length)] : sounds[i];
        _audioSource.pitch = Random.Range(p1, p2);
        if (isDestroyed)
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        else
            _audioSource.PlayOneShot(clip, volume);
    }

    [System.Serializable]

    public class SoundsArrays
    {
        public AudioClip[] ClipsSoundArray;
    }

}

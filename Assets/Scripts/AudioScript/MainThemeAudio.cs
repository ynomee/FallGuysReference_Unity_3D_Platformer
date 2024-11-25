using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThemeAudio : AudioSounds
{
    private void Start()
    {
        PlayRandomTrack();
    }
    private void PlayRandomTrack()
    {
        PlaySound(0, volume: 0.15f, random: true, p1: 1, p2: 1);
    }
}

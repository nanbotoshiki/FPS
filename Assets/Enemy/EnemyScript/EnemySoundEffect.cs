using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class EnemySoundEffect : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float pitchRange = 0.1f;
    AudioSource source;

    public static EnemySoundEffect instance;

    void Awake()
    {
        source = GetComponents<AudioSource>()[0];
    }

    public void PlaySE()
    {
        source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}

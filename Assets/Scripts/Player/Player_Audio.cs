using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [Header("Audio")]

    [SerializeField]
    private AudioSource footstepAudioSource;

    [SerializeField]
    private AudioSource healthAudioSource;

    [SerializeField]
    private AudioSource interactionsAudioSource;

    [SerializeField]
    private AudioClip damagedAudioClip;

    [SerializeField]
    private AudioClip invulnerableAudioClip;

    [SerializeField]
    private AudioClip digAudioClip;

    [SerializeField]
    private AudioClip deniedAudioClip;
}

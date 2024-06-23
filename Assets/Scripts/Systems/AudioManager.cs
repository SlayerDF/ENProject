using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public struct AudioClipStruct
    {
        public string name;
        public AudioClip audioClip;
    }

    [Serializable]
    public struct AudioSourceStruct
    {
        public string name;
        public AudioSource audioSource;
        public AudioClipStruct[] audioClipStructs;
    }

    [SerializeField]
    private AudioSourceStruct[] audioSourceStructs;

    private readonly Dictionary<string, AudioClip> audioClipsByName = new();
    private readonly Dictionary<string, AudioSource> audioSourcesByClipName = new();
    private readonly Dictionary<string, AudioSource> audioSourcesByChannelName = new();

    private void Awake()
    {
        foreach (var audioSourceStruct in audioSourceStructs)
        {
            audioSourcesByChannelName.Add(audioSourceStruct.name, audioSourceStruct.audioSource);

            foreach (var audioClipStruct in audioSourceStruct.audioClipStructs)
            {
                audioClipsByName.Add(audioClipStruct.name, audioClipStruct.audioClip);
                audioSourcesByClipName.Add(audioClipStruct.name, audioSourceStruct.audioSource);
            }
        }
    }

    public void Play(string clipName, bool interrupt = true)
    {
        var audioSource = audioSourcesByClipName[clipName];

        if (!interrupt && audioSource.isPlaying) return;

        audioSource.clip = audioClipsByName[clipName];
        audioSource.Play();
    }

    public async UniTask WaitToFinishByChannelName(string channel, bool ignoreTimeScale = false)
    {
        var audioSource = audioSourcesByChannelName[channel];

        if (!audioSource.isPlaying) return;

        var secondsToWait = audioSource.clip.length - audioSource.time;

        await UniTask.Delay(TimeSpan.FromSeconds(secondsToWait), ignoreTimeScale);
    }

    public async UniTask WaitToFinishAll(bool ignoreTimeScale = false)
    {
        float maxSecondsToWait = -1;

        for (int i = 0; i < audioSourceStructs.Length; i++)
        {
            var audioSource = audioSourceStructs[i].audioSource;

            if (!audioSource.isPlaying) continue;

            var secondsToWait = audioSource.clip.length - audioSource.time;

            if (secondsToWait > maxSecondsToWait)
            {
                maxSecondsToWait = secondsToWait;
            }
        }

        if (maxSecondsToWait > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(maxSecondsToWait), ignoreTimeScale);
        }
    }
}

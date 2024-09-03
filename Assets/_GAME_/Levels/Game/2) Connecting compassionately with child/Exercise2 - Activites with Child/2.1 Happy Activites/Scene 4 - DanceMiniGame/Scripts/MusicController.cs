using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("List of Tracks")]
    [SerializeField] private Track[] audioTracks;
    private int trackIndex;
    [Header("Text UI")]
    [SerializeField] private TMP_Text trackTextUI;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        trackIndex = 0;
        audioSource.clip = audioTracks[trackIndex].trackAudioClip;
        trackTextUI.text = audioTracks[trackIndex].name;
    }

    public void SkipForwardButton()
    {
        trackIndex = (trackIndex + 1) % audioTracks.Length;
        UpdateTrack(trackIndex);
    }

    public void SkipBackwardButton()
    {
        trackIndex = (trackIndex - 1) % audioTracks.Length;
        UpdateTrack(trackIndex);
    }

    void UpdateTrack(int index)
    {
        audioSource.clip = audioTracks[index].trackAudioClip;
        trackTextUI.text = audioTracks[index].name;
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void PauseAudio()
    {
        audioSource.Pause();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}

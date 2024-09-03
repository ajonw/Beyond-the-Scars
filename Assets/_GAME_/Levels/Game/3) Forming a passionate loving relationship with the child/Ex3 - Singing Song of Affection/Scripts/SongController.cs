using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongController : MonoBehaviour
{
    [SerializeField] GameObject songSelectorCanvas;
    [SerializeField] GameObject endButton;
    [Header("List of Songs")]
    [SerializeField] private SO_SongEntry[] songs;
    private int songIndex;
    [Header("Text UI")]
    [SerializeField] private TMP_Text songTextUI;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        songIndex = 0;
        audioSource.clip = songs[songIndex].song;
        songTextUI.text = songs[songIndex].name;
        songSelectorCanvas.SetActive(false);
        endButton.SetActive(false);
    }

    public void StartSongSession()
    {
        songSelectorCanvas.SetActive(true);
        songTextUI.text = songs[songIndex].name;
        endButton.SetActive(true);
    }

    public void CloseSongCanvas()
    {
        songSelectorCanvas.SetActive(false);
    }

    public void StopSession()
    {
        StopAudio();
        songSelectorCanvas.SetActive(true);
    }

    public void SkipForwardButton()
    {
        songIndex = (songIndex + 1) % songs.Length;
        UpdateTrack(songIndex);
    }

    public void SkipBackwardButton()
    {
        songIndex = (songIndex - 1) % songs.Length;
        UpdateTrack(songIndex);
    }

    void UpdateTrack(int index)
    {
        audioSource.clip = songs[index].song;
        songTextUI.text = songs[index].name;
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

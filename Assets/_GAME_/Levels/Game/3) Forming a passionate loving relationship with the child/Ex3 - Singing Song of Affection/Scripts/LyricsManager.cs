using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LyricsManager : MonoBehaviour
{
    public GameObject lyricsCanvas;
    public TMP_Text lyricsText;

    public Animator adultAnimator;
    public Animator childAnimator;

    public SO_SongEntry songEntry;

    [Header("Lyrics Manager")]
    [SerializeField] private SongController songController;

    public LevelLoader levelLoader;

    public SceneField nextScene;

    private int currentLine = 0;

    // Start is called before the first frame update
    void Start()
    {
        lyricsCanvas.SetActive(false);
    }

    public void StartLyrics()
    {
        songController.PlayAudio();
        songController.CloseSongCanvas();
        currentLine = 0;
        lyricsCanvas.SetActive(true);
        adultAnimator.SetBool("isDancing", true);
        childAnimator.SetBool("isDancing", true);
        StartCoroutine(DisplayLyrics());
    }

    private IEnumerator DisplayLyrics()
    {
        while (currentLine < songEntry.lyrics.Length)
        {
            lyricsText.text = songEntry.lyrics[currentLine];
            // StartCoroutine(KaraokeHighlight(songEntry.timestamps[currentLine]));
            yield return new WaitForSeconds(songEntry.timestamps[currentLine]);
            currentLine++;
        }
        lyricsText.text = "";
        Finish();
    }

    public void Finish()
    {
        StopAllCoroutines();
        adultAnimator.SetBool("isDancing", false);
        childAnimator.SetBool("isDancing", false);
        lyricsText.text = "";
        songController.StopAudio();
        songController.StartSongSession();
        lyricsCanvas.SetActive(false);

    }

    public void Quit()
    {
        lyricsCanvas.SetActive(false);
        songController.CloseSongCanvas();
        levelLoader.LoadNextLevel(nextScene);
    }
    IEnumerator KaraokeHighlight(float duration)
    {
        float elapsedTime = 0;
        int totalCharacters = lyricsText.text.Length;
        int visibleCount = 0;

        while (elapsedTime < duration)
        {
            // Calculate the number of characters that should be visible
            visibleCount = Mathf.FloorToInt((elapsedTime / duration) * totalCharacters);
            lyricsText.maxVisibleCharacters = visibleCount;  // Reveal characters over time

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure all characters are visible at the end
        lyricsText.maxVisibleCharacters = totalCharacters;
    }

}

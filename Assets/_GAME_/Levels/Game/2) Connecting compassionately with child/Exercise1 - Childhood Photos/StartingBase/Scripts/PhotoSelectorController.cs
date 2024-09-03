using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSelectorController : MonoBehaviour
{
    [Header("Photos")]
    [SerializeField] private Sprite[] photos;
    private int photoIndex;
    [Header("Image UI")]
    [SerializeField] private Image currentPhoto;

    [Header("Title Text UI")]
    [SerializeField] private TMP_Text currentTitleText;

    [Header("Button Text UI")]
    [SerializeField] private TMP_Text currentButtonText;

    [Header("Scenes")]
    [SerializeField] private SceneField positiveScene;
    [SerializeField] private SceneField negativeScene;

    [Header("Level Loader")]
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private GameObject checkMark;

    [SerializeField] private bool memoryLevel = true;


    private void Start()
    {
        photoIndex = 0;
        currentPhoto.sprite = photos[photoIndex];
        currentTitleText.text = "Happy Photo";
        if (memoryLevel)
        {
            currentButtonText.text = "Recall Positive Memory";
            if (CompletionData.completedHappyMemory)
            {
                checkMark.SetActive(true);
            }
            else
            {
                checkMark.SetActive(false);
            }
        }
        else
        {
            currentButtonText.text = "Start Happy Activities";

            if (Ex2CompletionData.completedHappyActivity)
            {
                checkMark.SetActive(true);
            }
            else
            {
                checkMark.SetActive(false);
            }
        }

    }

    public void NextButton()
    {
        photoIndex = (photoIndex + 1) % photos.Length;
        UpdatePhoto(photoIndex);
    }

    public void BackButton()
    {
        photoIndex = Mathf.Abs((photoIndex - 1) % photos.Length);
        UpdatePhoto(photoIndex);
    }

    void UpdatePhoto(int index)
    {
        currentPhoto.sprite = photos[index];
        if (photoIndex == 0)
        {
            currentTitleText.text = "Happy Photo";
            if (memoryLevel)
            {
                currentButtonText.text = "Recall Positive Memory";

                if (CompletionData.completedHappyMemory)
                {
                    checkMark.SetActive(true);
                }
                else
                {
                    checkMark.SetActive(false);
                }
            }
            else
            {
                currentButtonText.text = "Start Happy Activities";

                if (Ex2CompletionData.completedHappyActivity)
                {
                    checkMark.SetActive(true);
                }
                else
                {
                    checkMark.SetActive(false);
                }
            }

        }
        else if (photoIndex == 1)
        {
            currentTitleText.text = "Unhappy Photo";
            if (memoryLevel)
            {
                currentButtonText.text = "Recall Negative Memory";
                if (CompletionData.completedUnhappyMemory)
                {
                    checkMark.SetActive(true);
                }
                else
                {
                    checkMark.SetActive(false);
                }
            }
            else
            {
                currentButtonText.text = "Start Unhappy Activities";
                if (Ex2CompletionData.completedUnhappyActivity)
                {
                    checkMark.SetActive(true);
                }
                else
                {
                    checkMark.SetActive(false);
                }
            }
        }
    }

    public void RecallMemory()
    {
        if (photoIndex == 0 && !CompletionData.completedHappyMemory)
        {
            levelLoader.LoadNextLevel(positiveScene);
            CompletionData.completedHappyMemory = true;
        }
        else if (photoIndex == 1 && !CompletionData.completedUnhappyMemory)
        {
            levelLoader.LoadNextLevel(negativeScene);
            CompletionData.completedUnhappyMemory = true;
        }
    }

    public void PlayActivity()
    {
        if (photoIndex == 0 && !Ex2CompletionData.completedHappyActivity)
        {
            levelLoader.LoadNextLevel(positiveScene);
            Ex2CompletionData.completedHappyActivity = true;
        }
        else if (photoIndex == 1 && !Ex2CompletionData.completedUnhappyActivity)
        {
            levelLoader.LoadNextLevel(negativeScene);
            Ex2CompletionData.completedUnhappyActivity = true;
        }
    }
}

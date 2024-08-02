using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public RewardSystem rewardSystem;
    public Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float fillValue = (float)rewardSystem.secureAttachmentLevel / (float)rewardSystem.maxSecureAttachment;
        slider.value = fillValue;
    }
}

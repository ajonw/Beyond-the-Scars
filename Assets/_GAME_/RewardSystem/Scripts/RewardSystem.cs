using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    public int secureAttachmentLevel;
    public int maxSecureAttachment = 10;
    // Start is called before the first frame update
    void Start()
    {
        secureAttachmentLevel = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updatePoints(int amount)
    {
        secureAttachmentLevel += amount;

        if (secureAttachmentLevel <= 0)
        {
            secureAttachmentLevel = 0;
        }
    }
}

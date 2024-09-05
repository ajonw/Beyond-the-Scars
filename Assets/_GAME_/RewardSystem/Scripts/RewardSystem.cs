using UnityEngine;

public static class RewardSystem 
{
    public static int secureAttachmentLevel;
    public static int maxSecureAttachment = 10;
    // Start is called before the first frame update
    static RewardSystem()
    {
        secureAttachmentLevel = 0;
        maxSecureAttachment = 3;
    }
    public static void updatePoints(int amount)
    {
        secureAttachmentLevel += amount;

        if (secureAttachmentLevel <= 0)
        {
            secureAttachmentLevel = 0;
        }
    }
}

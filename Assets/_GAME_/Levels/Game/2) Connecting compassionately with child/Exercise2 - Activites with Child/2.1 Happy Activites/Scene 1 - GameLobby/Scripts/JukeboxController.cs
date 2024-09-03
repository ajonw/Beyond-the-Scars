using TMPro;
using UnityEngine;

public class JukeboxController : MonoBehaviour
{
    [SerializeField] public TMP_Text startInstruction;
    [SerializeField] public LevelLoader levelLoader;
    [SerializeField] public SceneField nextScene;

    private bool changeSceneEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        startInstruction.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeSceneEnabled && Input.GetKeyDown(KeyCode.Z))
        {
            //Change scene to game
            levelLoader.LoadNextLevel(nextScene);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startInstruction.enabled = true;
            changeSceneEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startInstruction.enabled = false;
            changeSceneEnabled = false;
        }
    }
}

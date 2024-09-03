using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private SpriteRenderer spriteRenderer;

    public KeyCode keyToPress;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                // gameObject.SetActive(false);
                spriteRenderer.enabled = false;
                MiniGameManager.instance.NoteHit();
            }
        }
        if (MiniGameManager.instance.restart)
        {
            spriteRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;

            MiniGameManager.instance.NoteMissed();
        }
    }
}

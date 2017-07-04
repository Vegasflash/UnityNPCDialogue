using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour
{
    public TextAsset script;

    public int startLine;
    public int endLine;

    public TextBoxManager tBoxManager;
    public GameObject speechBubble;

    public bool destroyWhenActivated;
    public bool requireButtonPress;
    private bool waitForPress;

    void Start()
    {
        tBoxManager = FindObjectOfType<TextBoxManager>();
    }

    void Update()
    {
        if(waitForPress && Input.GetKeyDown(KeyCode.F))
        {
            speechBubble.SetActive(true);
            tBoxManager.ReloadScript(script);
            tBoxManager.currentLine = startLine;
            tBoxManager.endAtLine = endLine;
            tBoxManager.EnableTextBox();
        }
        if (destroyWhenActivated)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (requireButtonPress)
            {
                waitForPress = true;
                return;
            }
            tBoxManager.ReloadScript(script);
            tBoxManager.currentLine = startLine;
            tBoxManager.endAtLine = endLine;
            tBoxManager.EnableTextBox();

        }
        if (destroyWhenActivated)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            waitForPress = false;
        }
    }
}

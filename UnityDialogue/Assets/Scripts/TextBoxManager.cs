using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public PlayerMotor playerMotor;
    public GameObject textBox;
    public GameObject player;
    public GameObject speechBubble;
    public GameObject firstChoiceButton;
    public GameObject secondChoiceButton;
    public Animator animator;

    public Text script;
    public Text firstChoiceText;
    public Text secondChoiceText;

    public int currentLine;
    public int endAtLine = 8;

    private float delay = 0.1f;
    private float slowDown = 5f;

    public TextAsset textFile;
    public string[] textLines;

    public bool isActive;
    public bool stopPlayerMovement;
    public bool canTalk;
    bool firstChoice = false;
    bool secondChoice = false;
    bool isChoosing = false;

    void Start()
    {
        animator = FindObjectOfType<Animator>();
        script.text = "";
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
        if(isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        delay = 0.1f;

        if (canTalk == true)
        {

        }
        if (Input.GetMouseButton(0))
        {
            delay /= slowDown;
        }
        if (Input.GetMouseButtonDown(0) && canTalk == true)
        {
            canTalk = false;
            if (!isChoosing)
            {
                currentLine++;
                EnableTextBox();
                playerMotor.canMove = false;
            }
        }
        switch (currentLine)
        {
            case 1:
                TextBoxUpdate(2, 3, currentLine);
                break;
            case 2:
                TextBoxUpdate(4, 5, currentLine);
                break;
            case 3:
                TextBoxUpdate(6, 7, currentLine);
                break;
            case 4:
                TextBoxUpdate(8, 8, currentLine);
                break;
            case 5:
                TextBoxUpdate(8, 8, currentLine);
                break;
            case 6:
                TextBoxUpdate(8, 8, currentLine);
                break;
            case 7:
                TextBoxUpdate(8, 8, currentLine);
                break;
            default:
                break;
        }
    }

    public void OnButtonHover()
    {
        animator.SetBool("isOver", true);
    }

    public void OnButtonExit()
    {
        animator.SetBool("isOver", false);
    }

    void TextBoxUpdate(int intFirstChoice, int intSecondChoice, int presentLine)
    {
        canTalk = false;
        isChoosing = true;
        UpdatePlayerChoices(presentLine);
        if (firstChoice == true)
        {
            UpdateCurrentLine(intFirstChoice);
        }
        else if (secondChoice == true)
        {
            UpdateCurrentLine(intSecondChoice);
        }
    }

    void UpdatePlayerChoices(int presentLine)
    {
        switch (presentLine)
        {
            case 1:
                firstChoiceText.text = "Hell Yeh!";
                secondChoiceText.text = "Ew, no thanks...";
                break;
            case 2:
                firstChoiceText.text = "Give me some greens!";
                secondChoiceText.text = "Crack, definetly crack!";
                break;
            case 3:
                firstChoiceText.text = "I just don't do drugs...";
                secondChoiceText.text = "Whatever, you suck!";
                break;
            case 4:
                firstChoiceText.text = "Thanks!";
                secondChoiceText.text = "Thanks... Loser!";
                break;
            case 5:
                firstChoiceText.text = "Aaaw yeeh, that's good!";
                secondChoiceText.text = "I just don't care...";
                break;

            case 6:
                firstChoiceText.text = "Thanks Mam";
                secondChoiceText.text = "Er, right.. ciao!";
                break;
            case 7:
                firstChoiceText.text = "Rude...";
                secondChoiceText.text = "Fuck you asshole!";               
                break;
            default:
                break;
        }
    }

    public void EnableButtons()
    {
        animator.SetBool("isTalking", true);
        firstChoiceButton.SetActive(true);
        secondChoiceButton.SetActive(true);
    }

    public void UpdateCurrentLine(int lineToChange)
    {
        currentLine = lineToChange;
        firstChoice = false;
        secondChoice = false;

        if (currentLine != 8)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
            speechBubble.SetActive(false);
        }
    }

    public void FirstButtonPressed()
    {
        AudioManager.Instance.ButtonAudio();
        firstChoice = true;
        firstChoiceButton.SetActive(false);
        secondChoiceButton.SetActive(false);
        canTalk = false;
    }

    public void SecondButtonPressed()
    {
        AudioManager.Instance.ButtonAudio();
        secondChoice = true;
        secondChoiceButton.SetActive(false);
        firstChoiceButton.SetActive(false);
        canTalk = false;
    }

    IEnumerator ShowText(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;
        script.text = "";
        AudioManager.Instance.TypeWriterAudio();
        while (currentCharacterIndex < stringLength)
        {           
            script.text += stringToDisplay[currentCharacterIndex];           
            currentCharacterIndex++;
            if (currentCharacterIndex < stringLength)
            {
                yield return new WaitForSeconds(delay);
            }
            else
            {
                canTalk = true;
                if (currentLine != 0)
                {
                    EnableButtons();
                }
                break;
            }
        }
        AudioManager.Instance.StopTypeWriter();
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        StartCoroutine(ShowText(textLines[currentLine]));
        if(stopPlayerMovement)
        {
            playerMotor.canMove = false;
        }
    }

    public void DisableTextBox()
    {
        animator.SetBool("isTalking", false);
        textBox.SetActive(false);
        isActive = false;
        playerMotor.canMove = true;
    }

    public void ReloadScript(TextAsset script)
    {
        if(script != null)
        {
            textLines = new string[1];
            textLines = (script.text.Split('\n'));
        }
    }
}

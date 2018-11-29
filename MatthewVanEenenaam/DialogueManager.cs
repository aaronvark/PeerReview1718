using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dBox;

    public Text dText;

    public bool dialogActive;

    public string[] dialogLines;

    public int currentLine;

    private PlayerMovement thePlayer;

    public int dialoguetime;

    // Use this for initialization
    void Start ()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (dialoguetime >= 10)
        {
            dialoguetime = 10;
        }
        else
        {
            dialoguetime++;
        }

        if (dialogActive && Input.GetKeyUp(KeyCode.E) && dialoguetime >= 10)
        {
            currentLine++;
        }

        if (currentLine >= dialogLines.Length && dialoguetime >= 10)
        {
            dBox.SetActive(false);
            dialogActive = false;
            thePlayer.canMove = true;
            currentLine = 0;
        }

        dText.text = dialogLines[currentLine];
               
	}

    public void ShowDialogue()
    {
        dialoguetime = 0;
        dialogActive = true;
        dBox.SetActive(true);
        thePlayer.canMove = false;
    }
}

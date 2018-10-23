using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public string Dialogue;
    public GameObject DialogueDisplay;
    public GameObject ThoughtTrail;
    public Text DialogueTextField;

	// Use this for initialization
	void Start ()
    {
        DialogueDisplay = GameObject.Find("ThoughtBubble");
        DialogueTextField = GameObject.Find("ThoughtBubble").GetComponentInChildren<Text>();
        ThoughtTrail = GameObject.Find("ThoughtTrail");
        DialogueDisplay.SetActive(false);
        ThoughtTrail.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.name == "Player")
        {
            DialogueTextField.text = Dialogue;
            DialogueDisplay.SetActive(true);
            ThoughtTrail.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D Other)
    {
        DialogueDisplay.SetActive(false);
        ThoughtTrail.SetActive(false);
    }
}

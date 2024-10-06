using HammerFallInteractive.EcoGame.Dialogues;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalSceneScript : MonoBehaviour
{
    public Button planetButton;
    public Dialogue startDialogue;

    void Start()
    {
        if (!SystemState.SnakePlayed)
        {
            planetButton.interactable = false;
            DialogueController ctrl = FindObjectOfType<DialogueController>();
            ctrl.onDialogueEnded.AddListener(OnDialogueEnded);
            ctrl.ShowDialogueBox();
            ctrl.StartDialogue(startDialogue);
        }
    }
    public void OnDialogueEnded(Dialogue dlg)
    {
        FindObjectOfType<DialogueController>().HideDialogueBox();
        planetButton.interactable = true;
    }
}

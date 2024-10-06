using HammerFallInteractive.EcoGame.Dialogues;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalSceneScript : MonoBehaviour
{
    public Button planetButton;
    public Button planet2Button;
    public Dialogue startDialogue;

    void Start()
    {
        if (!SystemState.PortalsVisited)
        {
            planetButton.interactable = false;
            planet2Button.interactable = false;
            DialogueController ctrl = FindObjectOfType<DialogueController>();
            ctrl.onDialogueEnded.AddListener(OnDialogueEnded);
            ctrl.ShowDialogueBox();
            ctrl.StartDialogue(startDialogue);
            SystemState.PortalsVisited = true;
        }
        else if (SystemState.SnakePlayed)
        {
            planet2Button.interactable = true;
            planetButton.interactable = true;
        }
    }
    public void OnDialogueEnded(Dialogue dlg)
    {
        FindObjectOfType<DialogueController>().HideDialogueBox();
        planetButton.interactable = true;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }   
}

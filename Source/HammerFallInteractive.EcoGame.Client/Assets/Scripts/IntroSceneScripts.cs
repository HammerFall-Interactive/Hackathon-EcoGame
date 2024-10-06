using HammerFallInteractive.EcoGame.Dialogues;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneScripts : MonoBehaviour
{
    void StartIntroDlg(Dialogue dlg)
    {
        FindObjectOfType<DialogueController>().StartDialogue(dlg);
        FindObjectOfType<DialogueController>().ShowDialogueBox();
        FindObjectOfType<DialogueController>().onDialogueEnded.AddListener(OnDialogueEnded);
    }

    void OnDialogueEnded(Dialogue dlg)
    {
        FindObjectOfType<DialogueController>().HideDialogueBox();
        GetComponent<Animator>().SetTrigger("Intro2");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using HammerFallInteractive.EcoGame.Dialogues;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneScripts : MonoBehaviour
{
    public TMP_Text wateringCanText;
    public Button wateringCanButton;
    public Image planetImage;
    public Sprite planetHealthySprite;
    public Toggle taskToggle;
    public Image taskToggleBg;
    public Color toggleDoneColor;
    public Color toggleUndoneColor;

    private void Start()
    {
        if (SystemState.SnakePlayed)
        {
            GetComponent<Animator>().SetTrigger("Intro2");
        }

        if (SystemState.ToolReady)
            OnWateringCanAvailable();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            GetComponent<Animator>().speed = 5f;
        if (Input.GetMouseButtonUp(0))
            GetComponent<Animator>().speed = 1f;
    }

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

    public void OnWateringCanAvailable()
    {
        wateringCanButton.interactable = true;
        wateringCanText.text = "1";
    }

    public void OnWateringCanUsed()
    {
        planetImage.sprite = planetHealthySprite;
        taskToggle.isOn = true;
        taskToggleBg.color = toggleDoneColor;
        wateringCanButton.interactable = true;
        wateringCanText.text = "0";
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

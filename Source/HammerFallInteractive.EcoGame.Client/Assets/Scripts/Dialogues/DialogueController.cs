/* Copyright © 2024 - HammerFall Interactive. All Rights Reserved
 * 
 * Dialogue System Controller - Lite Edition
 * This file is a part of in house Dialogue System - DialogueForge
 * Copying, Distribuuting or Using without explicit author permission is prohibited!
 * 
 * Author: craftersmine @ HammerFall Interactive 
 */
using TMPro;

using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;

namespace HammerFallInteractive.EcoGame.Dialogues
{
    public class DialogueController : MonoBehaviour
    {
        private Dialogue currentDialogue;
        private int currentDialogueTextIndex;
        private bool isTypewriterAnimationEnded = false;
        private bool isAnswerRequired = false;
        private bool newDIaloguePending = false;
        private bool isEndingPending = false;
        private bool isGamePaused = false;

        public TMP_Text dialogueTextContainer;

        public string emptyDialogueText;

        public UnityEvent<Dialogue> onDialogueEnded;
        public UnityEvent<DialogueEntry> onDialogueEntryStarted;
        public UnityEvent<DialogueEntry> onDialogueEntryEnded;

        public Animator dialogueContainerAnimator;

        public bool canSkipDialogue = true;
        public bool afterPauseFlag = false;

        public void Start()
        {
            dialogueContainerAnimator = GetComponent<Animator>();

            //if (currentDialogue == null)
            //    OnDialogueEnded();
            //else
            //    StartDialogue(currentDialogue);
        }

        private void Update()
        {
            if (!isAnswerRequired && (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)) && !isGamePaused)
            {
                PrintNextDialogueText();
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (dialogue == null)
                return;

            if (isEndingPending)
                newDIaloguePending = true;

            canSkipDialogue = true;
            afterPauseFlag = false;
            isTypewriterAnimationEnded = true;
            currentDialogue = dialogue;
            currentDialogueTextIndex = 0;

            Debug.Log("Beginning dialogue: " + dialogue.dialogueId);
            PrintNextDialogueText();

        }

        public void PrintNextDialogueText()
        {
            if (currentDialogue == null)
                return;

            if (!CanPrintNext())
            {
                if (afterPauseFlag)
                    afterPauseFlag = false;
                return;
            }

            if (isTypewriterAnimationEnded && !isAnswerRequired)
            {
                isTypewriterAnimationEnded = false;
                Debug.Log("Typewriter animation is ended, starting next dialogue entry...");

                if (currentDialogueTextIndex >= currentDialogue.entries.Count)
                {
                    isEndingPending = true;
                    OnDialogueEnded();
                    return;
                }

                DialogueEntry currentEntry = currentDialogue.entries[currentDialogueTextIndex];
                Debug.Log("Writing dialogue entry: " + currentEntry.id);
                onDialogueEntryStarted?.Invoke(currentEntry);
                dialogueTextContainer.text = currentEntry.text;
                currentDialogueTextIndex++;
                isTypewriterAnimationEnded = true;
            }
            else
            {
                Debug.Log("Typewriter animation is not ended, skipping animation...");

                onDialogueEntryEnded?.Invoke(currentDialogue.entries[currentDialogueTextIndex]);

            }
        }

        private bool CanPrintNext()
        {
            bool isPaused = isGamePaused;
            return !isPaused && canSkipDialogue && !afterPauseFlag;
        }

        public void HideDialogueBox()
        {
            if (dialogueTextContainer == null)
                dialogueContainerAnimator = GetComponent<Animator>();
            dialogueContainerAnimator.SetBool("IsHidden", true);
        }

        public void ShowDialogueBox()
        {
            if (dialogueTextContainer == null)
                dialogueContainerAnimator = GetComponent<Animator>();
            dialogueContainerAnimator.SetBool("IsHidden", false);
        }

        public Dialogue GetCurrentDialogue()
        {
            return currentDialogue;
        }

        public void BreakDialogue()
        {
            if (currentDialogue != null)
            {
                OnDialogueEnded();
            }
        }

        private void OnDialogueEnded()
        {
            Dialogue temp = currentDialogue;

            currentDialogue = null;
            currentDialogueTextIndex = 0;
            onDialogueEnded?.Invoke(temp);

            if (isEndingPending && !newDIaloguePending)
            {
                canSkipDialogue = false;

                dialogueTextContainer.text = emptyDialogueText;
            }
            isEndingPending = false;
            newDIaloguePending = false;
        }
    }
}

using System.Collections;
using UnityEngine;

public enum STATE {
    DISABLED,
    WAITING,
    TYPING
}

public class DialogueSystem : MonoBehaviour 
{
    public DialogueData dialogueData;
    
    public int currentText = 0;
    public bool finished = false;
    private bool isEnding = false;
    private TypeTextAnimation typeText;
    private DialogueUI dialogueUI;
    private STATE state;

    private void Awake() 
    {
        typeText = FindAnyObjectByType<TypeTextAnimation>();
        dialogueUI = FindAnyObjectByType<DialogueUI>();

        if (typeText != null)
        {
            typeText.TypeFinished += OnTypeFinished;
        }
    }

    private void Start() 
    {
        state = STATE.DISABLED;
    }

    public bool IsDisabled()
    {
        return state == STATE.DISABLED && !isEnding;
    }

    private void Update() 
    {
        if (state == STATE.DISABLED) return;

        switch (state) 
        {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.TYPING:
                Typing();
                break;
        }
    }

    public void Next() 
    {
        if (isEnding) return;

        if (finished)
        {
            StartCoroutine(EndDialogueRoutine());
            return;
        }

        if (dialogueData == null || dialogueData.talkScript == null || dialogueData.talkScript.Count == 0)
        {
            return;
        }

        if (currentText == 0) 
        {
            if (dialogueUI != null) dialogueUI.Enable();
        }

        if (dialogueUI != null) 
        {
            dialogueUI.SetName(dialogueData.talkScript[currentText].name);
            dialogueUI.SetPortrait(dialogueData.talkScript[currentText].profile);
        }
        
        if (typeText != null)
        {
            typeText.fullText = dialogueData.talkScript[currentText].text;
            typeText.StartTyping();
        }

        currentText++;

        if (currentText >= dialogueData.talkScript.Count) 
        {
            finished = true;
        }

        state = STATE.TYPING;
    }

    private void OnTypeFinished() 
    {
        state = STATE.WAITING;
    }

    private void Waiting() 
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E)) 
        {
            if (!finished) 
            {
                Next();
            }
            else  
            {
                StartCoroutine(EndDialogueRoutine());
            }
        }
    }

    private void Typing() 
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E)) 
        {
            if (typeText != null) typeText.Skip();
            state = STATE.WAITING;
        }
    }

    private IEnumerator EndDialogueRoutine()
    {
        isEnding = true;
        if (dialogueUI != null) dialogueUI.Disable();
        state = STATE.DISABLED;
        currentText = 0;
        finished = false;

        yield return new WaitForSeconds(0.2f);
        isEnding = false;
    }
}
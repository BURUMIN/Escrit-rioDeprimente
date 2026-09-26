using UnityEngine;

public enum STATE
{
    DISABLED,
    WAITING,
    TYPING
}
public class DialogueSystem : MonoBehaviour
{
    public DialogueData dialogueData;
    STATE state;
    int currentText = 0;
    bool finished = false;
    TypeTextAnimation typeText;
    DialogueUI dialogueUI;

    void Awake()
    {
        typeText = GetComponent<TypeTextAnimation>();
        dialogueUI = GetComponent<DialogueUI>();
        typeText.TypeFinished = onTypeFinished;
    }

    void Start()
    {
        state = STATE.DISABLED;
    }

    void Update()
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
        if (currentText == 0)
        {
            dialogueUI.enable();
        }
        
        dialogueUI.setName(dialogueData.talkScript[currentText].name);
        typeText.fullText = dialogueData.talkScript[currentText++].text;
        if (currentText == dialogueData.talkScript.Count) finished = true;
        typeText.StartTyping();
        state = STATE.TYPING;
    }
    void onTypeFinished()
    {

        state = STATE.WAITING;

    }
    void Waiting()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
                Next();
            if (!finished)
            {
                Next();
            } else
            {
                dialogueUI.disable();
                state = STATE.DISABLED;
                currentText = 0;
                finished = false;
            }
        }
    }

    void Typing()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            typeText.Skip();
            state = STATE.WAITING;
        }
    }
    

}

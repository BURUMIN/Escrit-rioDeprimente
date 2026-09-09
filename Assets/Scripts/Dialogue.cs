using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite portrait;
    public string spreechTxt;
    public string NPCName;

    private DialogueController dc;

    private void Start()
    {
        dc = FindAnyObjectByType<DialogueController>();
    }


    
}

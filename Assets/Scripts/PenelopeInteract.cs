using UnityEngine;

public class PenelopeInteract : MonoBehaviour, IInteractable
{
    private bool PlayerIn = false;
    DialogueSystem dialogueSystem;

    [System.Obsolete]
    public void Awake()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
    public void Interact()
    {
        dialogueSystem.Next();
    }

    public void Update()
    {
        if (PlayerIn && Input.GetKeyDown(KeyCode.E)) 
        {
            Interact();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIn = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIn = false;
        }
    }
}

using UnityEngine;

public class AntonioInteract : MonoBehaviour, IInteractable
{
    private bool PlayerIn = false;
    private DialogueSystem dialogueSystem;

    private void Awake()
    {
        dialogueSystem = FindAnyObjectByType<DialogueSystem>();
    }

    public void Interact()
    {
        if (dialogueSystem != null && dialogueSystem.IsDisabled())
        {
            dialogueSystem.Next();
        }
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
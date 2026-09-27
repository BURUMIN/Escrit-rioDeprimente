using UnityEngine;

public class InteractionController : MonoBehaviour, IInteractable
{
    private bool PlayerIn = false;
    private DialogueSystem dialogueSystem;
    public GameObject destroyOnFinish; // Referência ao objeto que será destruído

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
        if (PlayerIn || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            Destroy(destroyOnFinish); // Destroi o objeto após a interação
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

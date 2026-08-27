using UnityEngine;

public class InteractionController : MonoBehaviour, IInteractable
{
    private bool PlayerIn = false;
    public void Interact()
    {
            Destroy(gameObject);
    }

    public void Update()
    {
        if (PlayerIn && Input.GetKeyDown(KeyCode.E)) 
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIn = false;
        }
    }
}

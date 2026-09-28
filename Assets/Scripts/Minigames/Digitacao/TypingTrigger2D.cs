using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider2D))]
public class TypingTrigger2D : MonoBehaviour
{
    [Header("Referências")]
    public TypingManager typingManager;
    public KeyCode interactKey = KeyCode.E;

    // Suporta TextMeshPro (Canvas UI) ou TextMeshPro (World 3D)
    public TextMeshProUGUI promptTextCanvas;
    public TextMeshPro promptTextWorld;

    [Header("Controle do Jogador")]
    public PlayerMovement playerScript;

    private bool isPlayerInRange = false;
    private bool isMiniGameRunning = false;

    void Start()
    {
        SetPromptActive(false);
    }

    void OnEnable()
    {
        TypingManager.OnMiniGameCompleted += HandleSuccess;
        TypingManager.OnMiniGameFailed += HandleFailure;
    }

    void OnDisable()
    {
        TypingManager.OnMiniGameCompleted -= HandleSuccess;
        TypingManager.OnMiniGameFailed -= HandleFailure;
    }

    void Update()
    {
        if (isPlayerInRange && !isMiniGameRunning && Input.GetKeyDown(interactKey))
        {
            StartMiniGame();
        }
    }

    void StartMiniGame()
    {
        if (typingManager == null)
        {
            Debug.LogError("[TypingTrigger2D] TypingManager não está atribuído no Inspector!");
            return;
        }

        isMiniGameRunning = true;
        SetPromptActive(false);

        if (playerScript != null)
        {
            playerScript.SetCanMove(false);
        }

        typingManager.StartMiniGame();
    }

    void EndMiniGame()
    {
        isMiniGameRunning = false;

        if (playerScript != null)
        {
            playerScript.SetCanMove(true);
        }

        if (isPlayerInRange)
        {
            SetPromptActive(true);
        }
    }

    void HandleSuccess()
    {
        EndMiniGame();
    }

    void HandleFailure()
    {
        EndMiniGame();
    }

    private void SetPromptActive(bool active)
    {
        if (promptTextCanvas != null) promptTextCanvas.gameObject.SetActive(active);
        if (promptTextWorld != null) promptTextWorld.gameObject.SetActive(active);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Tenta pegar o PlayerMovement automaticamente caso esqueça de arrastar no Inspector
            if (playerScript == null)
                playerScript = collision.GetComponent<PlayerMovement>();

            isPlayerInRange = true;
            if (!isMiniGameRunning)
                SetPromptActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            SetPromptActive(false);
        }
    }
}
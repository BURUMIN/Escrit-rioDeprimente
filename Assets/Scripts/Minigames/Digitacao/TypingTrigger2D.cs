using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider2D))]
public class TypingTrigger2D : MonoBehaviour
{
    [Header("Referências")]
    public TypingManager typingManager;
    public KeyCode interactKey = KeyCode.E;
    public TextMeshPro promptText;

    [Header("Controle do Jogador")]
    public PlayerMovement playerScript;

    private bool isPlayerInRange = false;
    private bool isMiniGameRunning = false;

    void Start()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false);
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
        isMiniGameRunning = true;

        if (promptText != null)
            promptText.gameObject.SetActive(false);

        if (playerScript != null)
            playerScript.SetCanMove(false);

        typingManager.StartMiniGame();
    }

    void EndMiniGame()
    {
        isMiniGameRunning = false;

        if (playerScript != null)
            playerScript.SetCanMove(true);

        if (isPlayerInRange && promptText != null)
            promptText.gameObject.SetActive(true);
    }

    void HandleSuccess()
    {
        Debug.Log("Sucesso! Mini-game concluído.");
        EndMiniGame();
    }

    void HandleFailure()
    {
        Debug.Log("Falha! Tempo esgotado.");
        EndMiniGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (promptText != null && !isMiniGameRunning)
                promptText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (promptText != null)
                promptText.gameObject.SetActive(false);
        }
    }
}
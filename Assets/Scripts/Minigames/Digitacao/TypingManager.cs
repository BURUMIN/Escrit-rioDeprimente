using System;
using UnityEngine;
using TMPro;

public class TypingManager : MonoBehaviour
{
    public static event Action OnMiniGameCompleted;
    public static event Action OnMiniGameFailed;

    [Header("UI References")]
    public GameObject miniGameCanvas;
    public TextMeshProUGUI wordDisplay;
    public TextMeshProUGUI inputDisplay;

    [Header("Word Settings")]
    public TextAsset wordsFile;
    private string[] wordList;

    private string targetWord = "";
    private string currentInput = "";
    private bool isGameActive = false;

    void Start()
    {
        LoadWords();
        if (miniGameCanvas != null)
            miniGameCanvas.SetActive(false);
    }

    void LoadWords()
    {
        if (wordsFile != null)
        {
            wordList = wordsFile.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < wordList.Length; i++)
            {
                wordList[i] = wordList[i].Trim();
            }
        }
        else
        {
            Debug.LogError("[TypingManager] Arquivo words.txt não foi atribuído!");
            wordList = new string[] { "teste" };
        }
    }

    public void StartMiniGame()
    {
        if (wordList == null || wordList.Length == 0) LoadWords();

        targetWord = wordList[UnityEngine.Random.Range(0, wordList.Length)];
        currentInput = "";
        isGameActive = true;

        if (miniGameCanvas != null) miniGameCanvas.SetActive(true);
        UpdateUI();
    }

    void Update()
    {
        if (!isGameActive) return;

        // Captura caracteres digitados pelo jogador de forma segura
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // Backspace
            {
                if (currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    UpdateUI();
                }
            }
            else if (c == '\n' || c == '\r') // Enter
            {
                // Opcional: Submeter com Enter
            }
            else
            {
                // Adiciona o caractere digitado
                currentInput += c;
                ValidateInput();
            }
        }
    }

    void ValidateInput()
    {
        UpdateUI();

        // 1. Checa se o texto digitado até agora está correto
        if (!targetWord.StartsWith(currentInput, StringComparison.OrdinalIgnoreCase))
        {
            // O jogador ERROU a digitação!
            Debug.LogWarning("[TypingManager] Erro de digitação detectado! Reiniciando entrada/Falhando...");

            // Opção A: Limpar a palavra ao errar
            currentInput = "";
            UpdateUI();

            // Descomente a linha abaixo se quiser que um ERRO feche o minigame imediatamente:
            // FailMiniGame(); 
            return;
        }

        // 2. Checa se completou a palavra perfeitamente
        if (currentInput.Equals(targetWord, StringComparison.OrdinalIgnoreCase))
        {
            CompleteMiniGame();
        }
    }

    void UpdateUI()
    {
        if (wordDisplay != null) wordDisplay.text = targetWord;
        if (inputDisplay != null) inputDisplay.text = currentInput;
    }

    private void CompleteMiniGame()
    {
        isGameActive = false;
        if (miniGameCanvas != null) miniGameCanvas.SetActive(false);
        OnMiniGameCompleted?.Invoke();
    }

    public void FailMiniGame()
    {
        isGameActive = false;
        if (miniGameCanvas != null) miniGameCanvas.SetActive(false);
        OnMiniGameFailed?.Invoke();
    }
}
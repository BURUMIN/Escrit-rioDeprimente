using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class TypingManager : MonoBehaviour
{
    public static event Action OnMiniGameCompleted;
    public static event Action OnMiniGameFailed;

    [Header("UI References")]
    public GameObject miniGameCanvas;
    public TextMeshProUGUI wordDisplay; // Apenas um TextMeshProUGUI é necessário!
    public TextMeshProUGUI inputDisplay; // Opcional (pode deixar None se usar tudo no wordDisplay)

    [Header("Cores (Rich Text)")]
    public Color correctColor = new Color(0.13f, 0.77f, 0.36f); // Verde
    public Color errorColor = new Color(0.93f, 0.27f, 0.27f);   // Vermelho
    public Color remainingColor = Color.white;                  // Branco

    [Header("Word Settings")]
    public TextAsset wordsFile;
    private string[] wordList;
    
    private string targetWord = "";
    private string currentInput = "";
    private bool isGameActive = false;
    private bool isFlashingError = false;

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
        isFlashingError = false;

        if (miniGameCanvas != null) miniGameCanvas.SetActive(true);
        UpdateColoredUI();
    }

    void Update()
    {
        if (!isGameActive || isFlashingError) return;

        foreach (char c in Input.inputString)
        {
            if (c == '\b') // Backspace
            {
                if (currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    UpdateColoredUI();
                }
            }
            else if (c == '\n' || c == '\r') // Enter (ignorado)
            {
                continue;
            }
            else
            {
                // Processa novo caractere digitado
                ProcessTypedChar(c);
            }
        }
    }

    void ProcessTypedChar(char typedChar)
    {
        int nextIndex = currentInput.Length;

        // Checa se o caractere digitado coincide com o caractere esperado da targetWord
        if (nextIndex < targetWord.Length && 
            char.ToLower(typedChar) == char.ToLower(targetWord[nextIndex]))
        {
            // ACERTOU a letra
            currentInput += targetWord[nextIndex]; // Mantém o casing original
            UpdateColoredUI();

            // Checa se completou a palavra inteira
            if (currentInput.Length == targetWord.Length)
            {
                CompleteMiniGame();
            }
        }
        else
        {
            // ERROU a letra -> Pisca em vermelho e reinicia do zero!
            StartCoroutine(FlashErrorAndReset());
        }
    }

    void UpdateColoredUI()
    {
        if (wordDisplay == null) return;

        string correctHex = ColorUtility.ToHtmlStringRGB(correctColor);
        string remainingHex = ColorUtility.ToHtmlStringRGB(remainingColor);

        string typedPart = targetWord.Substring(0, currentInput.Length);
        string remainingPart = targetWord.Substring(currentInput.Length);

        // Formata o texto com Rich Text Tags do TMP
        string formattedText = $"<color=#{correctHex}>{typedPart}</color><color=#{remainingHex}>{remainingPart}</color>";

        wordDisplay.text = formattedText;

        if (inputDisplay != null)
        {
            inputDisplay.text = currentInput;
        }
    }

    IEnumerator FlashErrorAndReset()
    {
        isFlashingError = true;

        if (wordDisplay != null)
        {
            string errorHex = ColorUtility.ToHtmlStringRGB(errorColor);
            wordDisplay.text = $"<color=#{errorHex}>{targetWord}</color>";
        }

        // Aguarda 0.25s para o jogador ver o erro em vermelho
        yield return new WaitForSeconds(0.25f);

        // Reinicia a palavra do início
        currentInput = "";
        isFlashingError = false;
        UpdateColoredUI();
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
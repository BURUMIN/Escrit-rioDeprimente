using UnityEngine;
using TMPro;
using System;

public class TypingManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject miniGameCanvasGroup;
    public TextMeshProUGUI wordOutput;
    public TextMeshProUGUI scoreOutput;
    public TextMeshProUGUI timerOutput;

    [Header("Configuração do JSON")]
    public string jsonFileName = "words";

    [Header("Configurações do Jogo")]
    public float timePerWord = 5f;
    public int pointsPerWord = 100;
    public int targetWordsToWin = 3;

    private string[] wordList;

    public static event Action OnMiniGameCompleted;
    public static event Action OnMiniGameFailed;

    private Word activeWord;
    private float currentTime;
    private int score = 0;
    private int wordsCompleted = 0;
    private bool isGameActive = false;

    void Awake()
    {
        LoadWordsFromJSON();
    }

    void Start()
    {
        if (miniGameCanvasGroup != null)
            miniGameCanvasGroup.SetActive(false);
    }

    void LoadWordsFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFile != null)
        {
            WordData data = JsonUtility.FromJson<WordData>(jsonFile.text);

            if (data != null && data.words != null && data.words.Length > 0)
            {
                wordList = data.words;
                Debug.Log($"[TypingManager] {wordList.Length} palavras carregadas via JSON!");
            }
            else
            {
                Debug.LogError("[TypingManager] O arquivo JSON está vazio ou mal formatado.");
            }
        }
        else
        {
            Debug.LogError($"[TypingManager] Arquivo '{jsonFileName}' não encontrado em Assets/Resources/!");
        }
    }

    public void StartMiniGame()
    {
        if (wordList == null || wordList.Length == 0)
        {
            Debug.LogError("[TypingManager] Impossível iniciar: Nenhuma palavra foi carregada.");
            return;
        }

        score = 0;
        wordsCompleted = 0;
        isGameActive = true;

        if (miniGameCanvasGroup != null)
            miniGameCanvasGroup.SetActive(true);

        UpdateScoreDisplay();
        SetNextWord();
    }

    public void StopMiniGame()
    {
        isGameActive = false;

        if (miniGameCanvasGroup != null)
            miniGameCanvasGroup.SetActive(false);
    }

    void Update()
    {
        if (!isGameActive) return;

        HandleTimer();

        foreach (char letter in Input.inputString)
        {
            TypeLetter(letter);
        }
    }

    void HandleTimer()
    {
        currentTime -= Time.deltaTime;

        if (timerOutput != null)
        {
            timerOutput.text = $"Tempo: {Mathf.Max(currentTime, 0):F1}s";
        }

        if (currentTime <= 0)
        {
            StopMiniGame();
            OnMiniGameFailed?.Invoke();
        }
    }

    void SetNextWord()
    {
        string randomWord = wordList[UnityEngine.Random.Range(0, wordList.Length)];
        activeWord = new Word(randomWord);
        currentTime = timePerWord;
        UpdateWordDisplay();
    }

    void TypeLetter(char letter)
    {
        if (activeWord.GetNextChar() == letter)
        {
            activeWord.TypeLetter();
            UpdateWordDisplay();

            if (activeWord.WordTyped())
            {
                wordsCompleted++;
                int timeBonus = Mathf.RoundToInt(currentTime * 10);
                AddScore(pointsPerWord + timeBonus);

                if (wordsCompleted >= targetWordsToWin)
                {
                    StopMiniGame();
                    OnMiniGameCompleted?.Invoke();
                }
                else
                {
                    SetNextWord();
                }
            }
        }
    }

    void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

    void UpdateWordDisplay()
    {
        if (wordOutput != null)
        {
            wordOutput.text = activeWord.GetFormattedText();
        }
    }

    void UpdateScoreDisplay()
    {
        if (scoreOutput != null)
        {
            scoreOutput.text = $"Pontos: {score}";
        }
    }
}
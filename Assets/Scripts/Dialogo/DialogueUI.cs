using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour 
{
    [Header("Componentes do Painel (Arraste do Inspector)")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI talkText;
    [SerializeField] private Image portraitImage;

    [Header("Configurações")]
    public float speed = 10f;
    [SerializeField] private bool open = false;

    private void Awake()
    {
        if (background == null) background = transform.Find("Background")?.GetComponent<Image>();
        if (nameText == null) nameText = transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
        if (talkText == null) talkText = transform.Find("Text")?.GetComponent<TextMeshProUGUI>();
        if (portraitImage == null) portraitImage = transform.Find("Portrait")?.GetComponent<Image>();

        if (open)
        {
            Enable();
        } else {
            Disable();
        }
    }

    private void Update() 
    {
        if (background == null) return;

        if (open) {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 1, speed * Time.deltaTime);
        } else {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);
        }
    }

    public void SetName(string name) 
    {
        if (nameText != null) nameText.text = name;
    }

    public void SetPortrait(Sprite sprite)
    {
        if (portraitImage != null)
        {
            if (sprite != null)
            {
                portraitImage.gameObject.SetActive(true);
                portraitImage.sprite = sprite;
            }
            else
            {
                portraitImage.gameObject.SetActive(false);
            }
        }
    }

    public void Enable() 
    {
        if (background != null) background.fillAmount = 0;
        open = true;
    }

    public void Disable() 
    {
        open = false;
        if (nameText != null) nameText.text = "";
        if (talkText != null) talkText.text = "";
        if (portraitImage != null) portraitImage.gameObject.SetActive(false);
    }
}
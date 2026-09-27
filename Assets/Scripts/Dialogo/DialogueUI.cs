using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour 
{
    private Image background;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI talkText;
    private Image portraitImage; // Referência para o Portrait

    public float speed = 10f;
    private bool open = false;

    private void Awake() 
    {
        Transform bgTransform = transform.Find("Background");
        if (bgTransform != null) background = bgTransform.GetComponent<Image>();

        Transform nameTransform = transform.Find("Name");
        if (nameTransform != null) nameText = nameTransform.GetComponent<TextMeshProUGUI>();

        Transform textTransform = transform.Find("Text");
        if (textTransform != null) talkText = textTransform.GetComponent<TextMeshProUGUI>();

        Transform portraitTransform = transform.Find("Portrait");
        if (portraitTransform != null) portraitImage = portraitTransform.GetComponent<Image>();
        
        Disable();
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
                portraitImage.gameObject.SetActive(false); // Oculta se não houver foto
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
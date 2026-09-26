using UnityEngine;

using UnityEngine.UI;
using textMeshProUGUI = TMPro.TextMeshProUGUI;

public class DialogueUI : MonoBehaviour
{
    Image background;
    textMeshProUGUI nameText;
    textMeshProUGUI talkText;

    public float speed = 10f;
    bool open = false;
    
    void Awake()
    {
        background = transform.GetChild(0).GetComponent<Image>();
        nameText = transform.GetChild(1).GetComponent<textMeshProUGUI>();
        talkText = transform.GetChild(2).GetComponent<textMeshProUGUI>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (open)
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 1, Time.deltaTime * speed);
        }
        else
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 0, Time.deltaTime * speed);
        }
    }

    public void setName(string name)
    {
        nameText.text = name;
    }

    public void setPortrait(Image portrait)
    {
        nameText.text = name;
    }

    public void enable()
    {
        background.fillAmount = 0;
        open = true;
    }

    public void disable()
    {
        open = false;
        nameText.text = "";
        talkText.text = "";
    }
}

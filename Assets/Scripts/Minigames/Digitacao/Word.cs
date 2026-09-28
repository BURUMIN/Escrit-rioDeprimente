using System;

[Serializable]
public class Word
{
    public string text;
    private int typeIndex = 0;

    public Word(string _text)
    {
        text = _text;
        typeIndex = 0;
    }

    public char GetNextChar()
    {
        return text[typeIndex];
    }

    public void TypeLetter()
    {
        typeIndex++;
    }

    public bool WordTyped()
    {
        return typeIndex >= text.Length;
    }

    public string GetFormattedText()
    {
        string typed = text.Substring(0, typeIndex);
        string remaining = text.Substring(typeIndex);
        return $"<color=#00FF00>{typed}</color>{remaining}";
    }
}
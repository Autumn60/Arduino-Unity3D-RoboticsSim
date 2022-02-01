using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : MonoBehaviour
{
    [SerializeField]
    Text textObj;

    [SerializeField]
    private string[] text = new string[8];

    [SerializeField]
    string nowText = "";

    [SerializeField]
    private int textLength;

    // Start is called before the first frame update
    void Start()
    {
        textLength = text.Length;
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(string newText)
    {
        newText = newText.Replace(" ", "\u00A0");
        nowText = "";
        textLength = text.Length;
        for (int i = 0; i < textLength - 1; i++)
        {
            text[i] = text[i + 1];
            nowText += (text[i] + '\n');
        }
        nowText += text[textLength - 1] = newText;
        textObj.text = nowText;
    }

    public void Clear()
    {
        for (int i = 0; i < textLength; i++)
            text[i] = "";
        textObj.text = "";
    }
}

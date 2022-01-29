using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : Module
{
    Monitor()
    {
        moduleID = "VS";
        moduleType = Module.ModuleType.Actuator;
    }

    [SerializeField]
    Text textObj;

    [SerializeField]
    string[] text = new string[10];

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

    public override void SetValue(string newText)
    {
        string tmp = "";
        for(int i = 0; i < textLength - 1; i++)
        {
            text[i] = text[i + 1];
            tmp += (text[i]+'\n');
        }
        tmp += text[textLength - 1] = newText;
        textObj.text = tmp;
    }

    public void Clear()
    {
        for (int i = 0; i < textLength; i++)
            text[i] = "";
        textObj.text = "";
    }
}

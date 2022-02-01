using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    [SerializeField]
    private GameObject[] group1;

    [SerializeField]
    private GameObject[] group2;

    private bool state;

    // Start is called before the first frame update
    void Start()
    {
        state = false;

        foreach(var g in group1)
        {
            g.SetActive(true);
        }

        foreach (var g in group2)
        {
            g.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        foreach (var g in group1)
        {
            g.SetActive(state);
        }

        foreach (var g in group2)
        {
            g.SetActive(!state);
        }

        state = !state;
    }
}

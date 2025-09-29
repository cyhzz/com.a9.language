using System.Collections;
using System.Collections.Generic;
using Com.A9.Language;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleButtonCtrl : MonoBehaviour
{
    public bool loop;
    public int current;
    public int max_val
    {
        get
        {
            return text.Count;
        }
    }
    public UnityEvent<int> OnValChange;
    public List<string> text;
    public Button btn;

    void Awake()
    {
        btn.onClick.AddListener(Next);
    }

    public string GetText(int idx)
    {
        return text[idx].Localize();
    }

    void Next()
    {
        int p = current;
        current = current + 1;
        if (current >= max_val)
        {
            if (loop)
            {
                current = 0;
            }
            else
            {
                current = max_val - 1;
            }
        }
        // current = Mathf.Min(current + 1, max_val - 1);
        if (p != current)
            OnValChange?.Invoke(current);
    }

    public void SetValue(int val, bool force = false)
    {
        int p = current;
        current = val;
        if (current != p || force == true)
            OnValChange?.Invoke(current);
    }
}

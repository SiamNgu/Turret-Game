using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayer : UIDisplayer<string>
{
    public TMP_Text text;

    protected override void Display()
    {
        text.text = value;
    }
}

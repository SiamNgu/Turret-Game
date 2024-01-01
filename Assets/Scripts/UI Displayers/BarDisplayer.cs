using UnityEngine;

public class BarDisplayer : UIDisplayer<float>
{
    public RectTransform barTransform;

    protected override void Display()
    {
        barTransform.localScale = new Vector3(value, 1f, 1f);
    }
}

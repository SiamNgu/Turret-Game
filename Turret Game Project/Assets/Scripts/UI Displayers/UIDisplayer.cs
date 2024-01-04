using UnityEngine;

public abstract class UIDisplayer<T> : MonoBehaviour
{
    public T value;
    private void Start()
    {
        Display();
    }

    protected void SetValue(T value)
    {
        this.value = value;
    }
    protected abstract void Display();
    public void Display(T value)
    {
        SetValue(value);
        Display();
    }
}

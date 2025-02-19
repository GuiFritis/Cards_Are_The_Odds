using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Int")]
public class SOInt : ScriptableObject
{
    public Action<int, int> OnValueChanged;
    public int Value
    {
        get{return _value;}
        set
        {
            OnValueChanged?.Invoke(value, value - _value);
            _value = value;
        }
    }

    [SerializeField]
    private int _value;
}

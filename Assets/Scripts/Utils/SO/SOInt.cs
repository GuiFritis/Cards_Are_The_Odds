using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Int")]
public class SOInt : ScriptableObject
{
    public Action<int> OnValueChanged;
    public int Value
    {
        get{return _value;}
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    [SerializeField]
    private int _value;
}

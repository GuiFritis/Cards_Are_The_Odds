using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/String")]
public class SOString : ScriptableObject
{
    public Action<string> OnValueChanged;
    public string Value
    {
        get{return _value;}
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    [SerializeField]
    private string _value;
}

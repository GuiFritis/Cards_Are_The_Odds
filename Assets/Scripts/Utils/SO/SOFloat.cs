using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Float")]
public class SOFloat : ScriptableObject
{
    public Action<float> OnValueChanged;
    public float Value
    {
        get{return _value;}
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    [SerializeField]
    private float _value;
}

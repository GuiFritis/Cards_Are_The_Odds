using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class CardsHolder : MonoBehaviour
{
    [SerializeField] private List<CardUI> _cards = new();

    private void Start()
    {
        OrganizeCards();
    }

    [Button]
    private void GetChildrenCard()
    {
        _cards = GetComponentsInChildren<CardUI>().ToList();
    }

    [Button]
    private void OrganizeCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].SetPosition(
                new Vector3(-160 + (80 * i), CardPositionY(i)), 
                Quaternion.Euler(Vector3.forward * (20 - (10 * i))), 
                i
            );
        }
    }

    private float CardPositionY(int i) => i switch
    {
        0   =>  -64,
        1   =>  -44,
        2   =>  -36,
        3   =>  -44,
        4   =>  -64,
        _ => -36
    };
}

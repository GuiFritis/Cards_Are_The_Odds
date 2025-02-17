using System.Collections.Generic;
using UnityEngine;

public class CardsHolder : MonoBehaviour
{
    private List<GameObject> _cards = new();

    private void OrganizaCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.rotation = Quaternion.Euler(Vector3.forward * (20 - (10 * i)));
            _cards[i].transform.position = new Vector3(-160 + (80 * i), CardPositionY(i));
        }
    }

    private float CardPositionY(int i) => i switch
    {
        1   =>  32,
        2   =>  12,
        3   =>  6,
        4   =>  12,
        5   =>  32,
        _ => 6
    };
}

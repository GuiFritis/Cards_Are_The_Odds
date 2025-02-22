using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class CardsHolder : MonoBehaviour
{
    [SerializeField] private List<CardUI> _cards = new();
    [SerializeField] private SOAudio _soAudio;

    void Awake()
    {
        CardBase.OnCardActivated += DisableCards;
    }

    private void Start()
    {
        OrganizeCards();
    }

    public void DrawCard(CardUI card)
    {
        SFX_Pool.Instance.Play(_soAudio);
        _cards.Add(card);
        card.Enable();
        card.transform.SetParent(transform);
        card.transform.localPosition = new Vector3(0, -200);
        card.transform.localScale = Vector3.zero;
        OrganizeCards();
    }

    public void Discard(CardUI card)
    {
        _cards.Remove(card);
    }

    public void DisableCards(CardBase cardBase)
    {
        _cards.Remove(cardBase.GetCardUI);
        foreach (CardUI card in _cards)
        {
            card.Disable();
        }
    }

    public void EnableCards()
    {
        foreach (CardUI card in _cards)
        {
            card.Enable();
        }
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

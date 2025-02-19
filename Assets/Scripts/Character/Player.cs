using System.Collections.Generic;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Character))]
public class Player : MonoBehaviour
{
    [SerializeField] private CardsHolder _cardHolder;
    [SerializeField] private List<CardBase> _cards = new();
    private List<CardBase> _usedCards = new();
    private List<CardBase> _drawnCards = new();

    void Awake()
    {
        Character.OnTurnEnd += TurnEnd;
        CardBase.OnCardUsed += CardUsed;
    }

    private void TurnEnd(Character character)
    {
        if(character.gameObject.Equals(gameObject))
        {
            DrawCard();
        }
    }

    private void DrawCard()
    {
        if(_drawnCards.Count == 4)
        {
            bool bothTypes = false;
            CardType cardType = _drawnCards[0].GetCardSO.cardType;
            for (int i = 1; i < _drawnCards.Count; i++)
            {
                if(!_drawnCards[i].GetCardSO.cardType.Equals(cardType))
                {
                    bothTypes = true;
                    break;
                }
            }

            CardBase card = null;
            if(!bothTypes)
            {
                card = _cards.Find(i => !i.GetCardSO.cardType.Equals(cardType));
            }
            card ??= _cards.GetRandom();
            _cards.Remove(card);
            _drawnCards.Add(card);
            _cardHolder.DrawCard(card.GetComponent<CardUI>());
        }
        if(_cards.Count <= 2)
        {
            Shuffle();
        }
    }

    private void CardUsed(CardBase card)
    {
        _drawnCards.Remove(card);
        _usedCards.Add(card);
        _cardHolder.Discard(card.GetComponent<CardUI>());
    }

    private void Shuffle()
    {
        _cards.AddRange(_usedCards);
        _usedCards.Clear();
        _cards.Shuffle();
    }
}

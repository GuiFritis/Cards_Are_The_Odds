using System.Collections;
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
        Character.OnTurnStart += TurnStart;
        Character.OnTurnEnd += TurnEnd;
        CardBase.OnCardActivated += CardUsed;
        CardBase.OnCardFinished += EndTurn;
    }

    void Start()
    {
        StartCoroutine(DrawCardsOnStart());
    }

    private void TurnStart(Character character)
    {
        if(character.gameObject.Equals(gameObject))
        {
            _cardHolder.EnableCards();
        }
    }

    private void TurnEnd(Character character)
    {
        if(character.gameObject.Equals(gameObject))
        {
            if(_drawnCards.Count < 5)
            {
                DrawCard();
            }
        }
    }

    private void DrawCard()
    {
        CardBase card = null;
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
            if(!bothTypes)
            {
                card = _cards.Find(i => !i.GetCardSO.cardType.Equals(cardType));
                if(card == null)
                {
                    Shuffle();
                    card = _cards.Find(i => !i.GetCardSO.cardType.Equals(cardType));
                }
            }
        }

        card ??= _cards.GetRandom();
        _cards.Remove(card);
        if(card.gameObject.scene.rootCount == 0)
        {
            card = Instantiate(card, transform);
        }
        _drawnCards.Add(card);
        _cardHolder.DrawCard(card.GetCardUI);

        if(_cards.Count <= 2)
        {
            Shuffle();
        }
    }

    private void CardUsed(CardBase card)
    {
        card.transform.SetParent(transform);
        _drawnCards.Remove(card);
        _usedCards.Add(card);
    }

    private void EndTurn(CardBase card)
    {
        GameManager.Instance.GetPlayer.EndTurn();
    }

    private void Shuffle()
    {
        _cards.AddRange(_usedCards);
        _usedCards.Clear();
        _cards.Shuffle();
    }

    private IEnumerator DrawCardsOnStart()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(.2f);
            DrawCard();
        }
    }
}

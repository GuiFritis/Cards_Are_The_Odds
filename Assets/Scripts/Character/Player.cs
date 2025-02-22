using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

[RequireComponent(typeof(Character))]
public class Player : MonoBehaviour
{
    [SerializeField] private CardsHolder _cardHolder;
    [SerializeField] private List<CardBase> _cards = new();
    [SerializeField] private Image _panel;
    private List<CardBase> _usedCards = new();
    private List<CardBase> _drawnCards = new();

    void Awake()
    {
        Character.OnTurnStart += TurnStart;
        Character.OnTurnEnd += TurnEnd;
        CardBase.OnCardActivated += CardUsed;
        CardBase.OnCardFinished += EndTurn;
        if(TryGetComponent<HealthBase>(out HealthBase health))
        {
            health.OnDamage += FlashPanel;
        }
    }

    void Start()
    {
        StartCoroutine(DrawCardsOnStart());
    }

    private void TurnStart(Character character)
    {
        if(character.gameObject.Equals(gameObject))
        {
            if(character.IsStuned)
            {
                DisplayStunned();
            }
            else
            {
                _cardHolder.EnableCards();
            }
        }
    }

    private void DisplayStunned()
    {
        DamageText_UI text_UI = DamageText_Pooling.Instance.GetPoolItem();
        text_UI.TextMesh.text = "Stunned";
        text_UI.TextMesh.color = Color.white;
        text_UI.transform.position = transform.position + (20f * Vector3.up);
        text_UI.Play(0.8f, 28);
    }

    private void TurnEnd(Character character)
    {
        if(character.gameObject.Equals(gameObject))
        {
            _cardHolder.DisableCards();
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

    private void FlashPanel(HealthBase hp, int damage)
    {
        if(damage > 0)
        {
            _panel.DOKill();
            _panel.color = Color.clear;
            _panel.DOColor(new Color(1, 0, 0, .6f), 0.15f).SetLoops(2, LoopType.Yoyo);
        }
    }
}

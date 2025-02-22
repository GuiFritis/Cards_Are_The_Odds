using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<BaseAction> _actions = new();
    [SerializeField] private Sprite _enemySprite;
    public Sprite EnemySprite => _enemySprite;
    [SerializeField] private ParticleSystem _blowVFX;
    private int _actionIndex;
    private HealthBase _health;

    void Awake()
    {
        _health = GetComponent<HealthBase>();
    }

    void OnEnable()
    {        
        Character.OnTurnStart += Act;
        _health.OnDamage += PlayBlowVFX;
    }

    void OnDisable()
    {        
        Character.OnTurnStart -= Act;
        _health.OnDamage -= PlayBlowVFX;
    }

    private void Act(Character character)
    {
        if(character.gameObject.Equals(gameObject))
        {
            StartCoroutine(Acting(character));
        }
    }

    private IEnumerator Acting(Character character)
    {
        yield return new WaitForSeconds(.5f);
        if(!character.IsStuned)
        {
            ShowAction(_actions[_actionIndex].ActionName);
            yield return StartCoroutine(_actions[_actionIndex].Activate(character.Advantage));
        }
        else
        {
            ShowAction("Sunned");
        }
        _actionIndex++;
        if(_actionIndex >= _actions.Count)
        {
            _actionIndex = 0;
        }
        character.EndTurn();
    }

    private void ShowAction(string action)
    {
        DamageText_UI _damageText = DamageText_Pooling.Instance.GetPoolItem();
        _damageText.TextMesh.text = action;
        _damageText.transform.position = transform.position - (100f * Vector3.up);
        _damageText.TextMesh.color = Color.white;
        _damageText.Play(1, 35);
    }

    private void PlayBlowVFX(HealthBase hp, int damage)
    {
        if(_blowVFX != null)
        {
            _blowVFX.Play();
        }
    }
}

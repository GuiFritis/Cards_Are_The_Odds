using System.Collections;
using System.Collections.Generic;
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
        Character.OnTurnStart += Act;
        _health = GetComponent<HealthBase>();
        _health.OnDamage += PlayBlowVFX;
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
            yield return StartCoroutine(_actions[_actionIndex].Activate(character.Advantage));
        }
        _actionIndex++;
        if(_actionIndex >= _actions.Count)
        {
            _actionIndex = 0;
        }
        character.EndTurn();
    }

    private void PlayBlowVFX(HealthBase hp, int damage)
    {
        if(_blowVFX != null)
        {
            _blowVFX.Play();
        }
    }
}

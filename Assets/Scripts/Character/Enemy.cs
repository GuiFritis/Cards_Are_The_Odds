using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<BaseAction> _actions = new();
    private int _actionIndex;

    void Start()
    {
        Character.OnTurnStart += Act;
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
        yield return new WaitForSeconds(1);
        if(!character.IsStuned)
        {
            _actions[_actionIndex].Activate(character.Advantage);
        }
        _actionIndex++;
        if(_actionIndex >= _actions.Count)
        {
            _actionIndex = 0;
        }
        character.EndTurn();
    }
}

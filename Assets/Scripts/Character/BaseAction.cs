using System.Collections;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour, IAction
{
    protected Character _thisCharacter;
    protected Character _enemyCharacter;
    [SerializeField] protected string _actionName;
    public string ActionName => _actionName;
    [SerializeField] protected int _success;
    [SerializeField] protected int _failure;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _duration;
    [SerializeField] protected SOAudio _soAudio;
    public System.Action<BaseAction> OnActionUsed;

    protected abstract IEnumerator CritSuccess(int result = 0);
    protected abstract IEnumerator Success(int result = 0);
    protected abstract IEnumerator Failure(int result = 0);
    protected abstract IEnumerator CritFailure(int result = 0);

    public IEnumerator Activate(int advantage = 0)
    {
        int result = 0;
        yield return StartCoroutine(Dice.Instance.ThrowDice(i => result = i, advantage, _success, _failure));
        switch (result)
        {
            case 20:
                yield return StartCoroutine(CritSuccess());
                break;
            case var _ when result >= _success:
                yield return StartCoroutine(Success());
                break;
            case var _ when result >= _failure:
                yield return StartCoroutine(Failure());
                break;
            case var _ when result < _failure:
                yield return StartCoroutine(CritFailure());
                break;
        }
        yield return new WaitForSeconds(.2f);
        OnActionUsed?.Invoke(this);
    }

    void Start()
    {
        _thisCharacter = GameManager.Instance.GetEnemy;
        _enemyCharacter = GameManager.Instance.GetPlayer;
    }

    protected void PlayAudio()
    {
        SFX_Pool.Instance.Play(_soAudio);
    }
}

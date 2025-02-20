using UnityEngine;

public class BaseAction : MonoBehaviour, IAction
{
    protected Character _thisCharacter;
    protected Character _enemyCharacter;
    [SerializeField] protected string _actionName;
    [SerializeField] protected int _success;
    [SerializeField] protected int _failure;
    [SerializeField] protected int _damage;
    public System.Action<BaseAction> OnActionUsed;

    public virtual void Activate(int advantage = 0){}

    void Start()
    {
        _thisCharacter = GameManager.Instance.GetEnemy;
        _enemyCharacter = GameManager.Instance.GetPlayer;
    }
}

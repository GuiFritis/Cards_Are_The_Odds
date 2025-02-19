using UnityEngine;

public class BaseAction : MonoBehaviour, IAction
{
    private Character _thisCharacter;
    private Character _enemyCharacter;

    public virtual void Activate(int advantage = 0){}
}

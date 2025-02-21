using System.Collections;

public interface IAction
{
    IEnumerator Activate(int advantage = 0);
}

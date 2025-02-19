public interface ICard : IAction
{
    CardSO GetCardSO {get;}
    bool CanUse();
}

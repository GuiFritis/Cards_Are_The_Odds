public interface ICard
{
    CardSO GetCardSO {get;}
    bool CanUse();
    void Use();
}

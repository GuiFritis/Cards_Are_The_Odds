public interface ICard
{
    bool PodeUsar();
    void Use();
    protected void FalhaCritica();
    protected void Falha();
    protected void Sucesso();
    protected void SucessoCritico();
}

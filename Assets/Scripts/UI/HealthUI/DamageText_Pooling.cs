using Utils;

public class DamageText_Pooling : PoolBase<DamageText_UI, DamageText_Pooling>
{
    protected override DamageText_UI CreatePoolItem()
    {        
        DamageText_UI instance =  Instantiate(PFB_item, gameObject.transform);
        instance.Pool = _pool;
        return instance;
    }
}

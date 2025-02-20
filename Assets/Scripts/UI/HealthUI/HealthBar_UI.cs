using UnityEngine;
using DG.Tweening;
using Utils;

public class HealthBar_UI : MonoBehaviour, IPoolItem
{
    [SerializeField] private Transform _maskTransform;
    private HealthBase _health;

    public void SetHealth(HealthBase hp)
    {
        _health = hp;
        _health.OnDamage += OnDamageTaken;
        _maskTransform.localScale = Vector3.one;
    }

    public void GetFromPool()
    {
        gameObject.SetActive(false);
    }

    public void ReturnToPool()
    {
        _health.OnDamage -= OnDamageTaken;
        gameObject.SetActive(false);
        _health = null;
    }

    private void OnDamageTaken(HealthBase hp, int damage)
    {
        gameObject.SetActive(true);
        UpdateBar();
    }

    private void UpdateBar()
    {        
        _maskTransform.DOKill();
        _maskTransform.DOScaleX(_health.CurrentHealth/_health.baseHealth, .25f).SetEase(Ease.OutCirc);
    }
}

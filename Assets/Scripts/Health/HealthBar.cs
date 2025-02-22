using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthBase _health;
    [SerializeField] private Image _imageHealthBar;

    private void OnValidate()
    {
        if(_health == null)
        {
            _health = GetComponent<HealthBase>();
        }
    }

    private void OnEnable()
    {
        SetUpHealthBar();
        _health.OnDamage += OnDamage;
    }

    private void SetUpHealthBar()
    {
        _imageHealthBar.DOKill();
        float fill = (1f * _health.CurrentHealth)/_health.baseHealth;
        _imageHealthBar.DOFillAmount(fill, .2f);
    }

    private void OnDamage(HealthBase hp, int damage)
    {
        SetUpHealthBar();
    }
}

using UnityEngine;

[RequireComponent(typeof(HealthBase))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthBase _health;
    [SerializeField] private float _barOffsetY;
    private HealthBar_UI _barUI;

    private void OnValidate()
    {
        if(_health == null)
        {
            _health = GetComponent<HealthBase>();
        }
    }

    private void OnEnable()
    {
        if(HealthBar_Pooling.Instance == null)
        {
            enabled = false;
        }
        else
        {
            SetUpHealthBar();
            _health.OnDeath += OnDeath;
        }
    }
    private void SetUpHealthBar()
    {
        _barUI = HealthBar_Pooling.Instance.GetPoolItem();
        _barUI.SetHealth(_health);
        UpdateUiPosition();
    }

    private void Update()
    {
        UpdateUiPosition();
    }

    private void UpdateUiPosition()
    {
        if(_barUI != null)
        {
            _barUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + _barOffsetY * Vector3.up);
        }
    }

    private void OnDeath(HealthBase hp)
    {
        hp.OnDeath -= OnDeath;
        HealthBar_Pooling.Instance.ReturnPoolItem(_barUI);
        _barUI = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _barOffsetY * Vector3.up, .3f);
    }
}

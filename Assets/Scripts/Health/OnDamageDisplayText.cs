using UnityEngine;

[RequireComponent(typeof(HealthBase))]
public class OnDamageDisplayText : MonoBehaviour
{
    [SerializeField] private HealthBase _health;
    [SerializeField] private float _spawnTextOffsetY;
    private DamageText_UI _damageText;
    private Camera _camera;

    private void OnValidate()
    {
        if(_health == null)
        {
            _health = GetComponent<HealthBase>();
        }
    }

    private void Start()
    {
        _health.OnDamage += PopText;
        _camera = Camera.main;
    }

    private void PopText(HealthBase hp, float damage)
    {
        if(DamageText_Pooling.Instance != null)
        {
            _damageText = DamageText_Pooling.Instance.GetPoolItem();
            _damageText.TextMesh.text = (damage <= 1 ? 1 : Mathf.Round(damage)).ToString();
            _damageText.transform.position = _camera.WorldToScreenPoint(transform.position + (_spawnTextOffsetY * Vector3.up));
            _damageText.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + (_spawnTextOffsetY * Vector3.up), .1f);
    }
}

using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private  float _maxHealth;
    private float _currentHealth;

    public Image _healthBar;
    public float maxHealth => _maxHealth;
    public float currHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }
    public void SetHealth(float _damage)
    {
        currHealth -= _damage;
        if (_healthBar != null)
        {
            _healthBar.fillAmount = currHealth / maxHealth;
        }
    }

}

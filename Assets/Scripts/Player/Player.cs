using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private PlayerMovement _playerMovement;
    private Weapon _currentWeapon;
    private Coroutine _coroutine;
    private float _currentHealth;
    private float _targetHealth;
    private float _recoveryRate = 10f;

    public event UnityAction<float, float> HealthChanged;

    public float Money { get; private set; }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _currentWeapon = _weapons[0];
        _currentHealth = _health;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _playerMovement.Velocity.x == 0)
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void ApplyDamage(float damage)
    {
        _targetHealth = _currentHealth - damage;
        OnChangeHealth();

        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    public void AddHealth(float health)
    {
        _targetHealth = _currentHealth + health;
        OnChangeHealth();

        if (_currentHealth > _health)
            _currentHealth = _health;
    }

    public void AddMoney(float money)
    {
        Money += money;
    }

    private void OnChangeHealth()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeHealth());
    }

    private IEnumerator ChangeHealth()
    {
        while (_currentHealth != _targetHealth)
        {
            _currentHealth = Mathf.MoveTowards
            (_currentHealth, _targetHealth, _recoveryRate * Time.deltaTime);
            HealthChanged?.Invoke(_currentHealth, _health);

            yield return null;
        }
    }
}

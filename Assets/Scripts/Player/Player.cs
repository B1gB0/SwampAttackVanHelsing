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
    [SerializeField] private GameObject _hitBox;

    private PlayerMovement _playerMovement;
    private Weapon _currentWeapon;
    private Coroutine _coroutine;
    private bool _isAlive = true;
    private bool _crossbowIsBuyed = false;
    private bool _knifeIsBuyed = false;
    private int _currentWeaponNumber = 0;
    private float _currentHealth;
    private float _targetHealth;
    private float _recoveryRate = 10f;

    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<float> MoneyChanged;

    public float Money { get; private set; }
    public bool IsAlive => _isAlive;
    public bool CrossbowIsBuyed => _crossbowIsBuyed;
    public bool KnifeIsBuyed => _knifeIsBuyed;
    public List<Weapon> Weapons => _weapons;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _health;
        Money += 2f;
    }

    private void Update()
    {
        foreach (Weapon weapon in _weapons)
        {
            if(weapon.TryGetComponent<Crossbow>(out Crossbow crossbow))
            {
                _crossbowIsBuyed = crossbow.IsBuyed;
            }
            else if(weapon.TryGetComponent<Knife>(out Knife knife))
            {
                _knifeIsBuyed = knife.IsBuyed;
            }
        }

        if (_crossbowIsBuyed)
        {
            if (Input.GetMouseButtonDown(0) && _playerMovement.Velocity.x == 0)
            {
                _currentWeapon.Shoot(_shootPoint);
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        _targetHealth = _currentHealth - damage;
        OnChangeHealth();

        if (_currentHealth <= 0)
        {
            _isAlive = false;
            gameObject.SetActive(false);
        }
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
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void OnChangeHealth()
    {
        if (_isAlive)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangeHealth());
        }
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

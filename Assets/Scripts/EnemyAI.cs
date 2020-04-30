using System.Collections;
using UnityEngine;
public class EnemyAI : Health
{
    [SerializeField] private  float _enemiesSearchRadius = 10f;
    private bool _enemyFound = false;
    private GameObject _player;
    private Rigidbody _rb;
    [SerializeField] private  float _maxSpeed = 100f;
    private PlayerController _shootController;
    private  float _shootDelay = 0.5f;
    private bool _canShoot = true;
    private const float _collisionContactForce = 1000;

    private void Start()
    {
        currHealth = maxHealth;
        _rb = GetComponent<Rigidbody>();
        _shootController = GetComponent<PlayerController>();
    }
    private void FixedUpdate()
    {
        FindPlayer();
        if (_player != null)
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
            Vector3 moveDir = (-transform.position + _player.transform.position);
            _rb.velocity += moveDir * Time.fixedDeltaTime;
            _rb.transform.LookAt(_player.transform);
            if (_canShoot)
            {
                StartCoroutine(Shooting(_shootDelay));
            }
        }
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Shooting(float time)
    {
        _canShoot = false;
        _shootController.Shoot();
        yield return new WaitForSeconds(time);
        _canShoot = true;
    }

    private GameObject FindPlayer()
    {
        if (!_enemyFound)
        {
            Collider[] hitCol = Physics.OverlapSphere(transform.position, _enemiesSearchRadius);
            if (hitCol != null)
            {
                for (int i = 0; i < hitCol.Length; i++)
                {
                    if (hitCol[i].GetComponent<PlayerInput>())
                    {
                        _enemyFound = true;
                        _player = hitCol[i].gameObject;

                    }
                }

            }
        }
        return _player;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Bullet>())
        {
            SetHealth(collision.collider.GetComponent<Bullet>()._damage);
        }
        else
        {
            SetHealth(collision.collider.contactOffset * _collisionContactForce);

        }
    }

}

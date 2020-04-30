using UnityEngine;

internal class Obstacle : Health
{
    public GameObject _destroyObstacleVFX = null;
    [SerializeField] private  float _destroyingTime = 2f;
    private void Start()
    {
        currHealth = maxHealth;
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 100, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Bullet>() && collision.collider != null)
        {
            SetHealth(collision.collider.GetComponent<Bullet>()._damage);
        }
        if (currHealth <= 0)
        {
            Destroy(Instantiate(_destroyObstacleVFX, transform.position, Quaternion.identity), _destroyingTime);
            Destroy(gameObject);

        }
    }
}

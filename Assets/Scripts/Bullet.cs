using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject _vfx;
    public float _destroyTime = 0.1f;
    public float _damage = 50f;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(_vfx, transform.position, Quaternion.identity), _destroyTime);
        Destroy(gameObject);
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] _shootPos;
    public GameObject _bullets;
    public float _bulletForce = 5000f;

    public void Shoot()
    {
        GameObject obj;
        for (int i = 0; i < _shootPos.Length; i++)
        {
            obj = Instantiate(_bullets, _shootPos[i].transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * _bulletForce);
            Destroy(obj, 1f);
        }

    }

}

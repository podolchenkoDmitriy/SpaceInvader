using System.Collections;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField] GameObject _winText = null;
    [SerializeField] GameObject _loseText = null;
    [SerializeField] GameObject _tutorialText = null;
    [SerializeField] float _disableTextTime = 2f;
    PlayerInput _player;
    private void Start()
    {
        _player = FindObjectOfType<PlayerInput>().GetComponent<PlayerInput>();
        StartCoroutine(DisableText(_disableTextTime));
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerInput>())
        {
            _winText.SetActive(true);
            Destroy(_player.gameObject);
        }
    }
    IEnumerator DisableText(float time)
    {
        yield return new WaitForSeconds(time);
        _tutorialText.SetActive(false);
    }

    private void Update()
    {
        if (_player != null)
        {
            if (_player.currHealth <= 0)
            {
                _loseText.SetActive(true);
                Destroy(_player.gameObject);
            }
        }
    }
}

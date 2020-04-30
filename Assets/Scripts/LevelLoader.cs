using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator _animator = null;
    [SerializeField] float _transitionTim = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        _animator.SetTrigger("Start");
        yield return new WaitForSeconds(_transitionTim);
        SceneManager.LoadScene(levelIndex);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }
}

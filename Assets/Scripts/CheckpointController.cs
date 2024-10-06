using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    public string nextLevelName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Произошел коллижен ");
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
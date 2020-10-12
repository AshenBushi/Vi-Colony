using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //public int Health { get; } = 100;
    public float Speed { get; } = 100;

    public void Die()
    {
        SceneManager.LoadScene(0);
    }
}

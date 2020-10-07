using UnityEngine;
public class Player : MonoBehaviour
{
    public int Health { get; } = 100;
    public float Speed { get; } = 100;

    private void Die()
    {
        Destroy(gameObject);
    }
}

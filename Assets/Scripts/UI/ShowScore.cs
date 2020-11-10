using TMPro;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;

    private TMP_Text _text;

    private void OnEnable()
    {
        _playerMover.OnJump += OnJump;
    }

    private void OnDisable()
    {
        _playerMover.OnJump -= OnJump;
    }

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        OnJump(0);
    }

    private void OnJump(int jumps)
    {
        _text.text = jumps.ToString();
    }
}

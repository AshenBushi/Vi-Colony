using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowJumps : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TMP_Text _text;

    private void OnEnable()
    {
        _player.Jumping += OnJumping;
    }

    private void OnDisable()
    {
        _player.Jumping -= OnJumping;
    }

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        OnJumping();
    }

    private void OnJumping()
    {
        _text.text = _player.PlayerJumps.ToString();
    }
}

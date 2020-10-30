using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TMP_Text _text;

    private void OnEnable()
    {
        _player.OnJumping += OnJumping;
    }

    private void OnDisable()
    {
        _player.OnJumping -= OnJumping;
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

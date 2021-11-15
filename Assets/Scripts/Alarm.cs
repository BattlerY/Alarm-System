﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _door;
    [SerializeField] private Sprite _openDoor;
    [SerializeField] private Sprite _closeDoor;
    [SerializeField] private float _alarmFullVolumeTime;
    [SerializeField] private int _houseId;

    private AudioSource _alarmSignal;
    private Coroutine _changeVolume;

    private void Awake()
    {
        _alarmSignal = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && player.CanEnter(_houseId)==false)
        {
            _door.sprite = _openDoor;
            _alarmSignal.volume = 0;
            _alarmSignal.Play();
            _changeVolume = StartCoroutine(ChangeVolume());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _door.sprite = _closeDoor;
        StopCoroutine(_changeVolume);
    }

    private IEnumerator ChangeVolume()
    {
        float _alarmContinueTime = 0;
        float startPosition = _alarmSignal.volume;
        float endPosition = 1;
        
        while (true)
        {
            _alarmContinueTime += Time.deltaTime;
            _alarmSignal.volume = Mathf.MoveTowards(startPosition, endPosition, _alarmContinueTime / _alarmFullVolumeTime);

            if (_alarmSignal.volume == 0 || _alarmSignal.volume == 1)
            {
                float temp = startPosition;
                startPosition = endPosition;
                endPosition = temp;
                _alarmContinueTime = 0;
            }

            yield return null;
        }
    }
}

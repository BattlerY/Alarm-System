using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _door;
    [SerializeField] private Sprite _openDoor;
    [SerializeField] private Sprite _closeDoor;
    [SerializeField] private float _alarmFullVolumeTime;

    private AudioSource _alarmSignal;
    private float _alarmContinueTime;
    private bool _isInsiderInside;

    private void Awake()
    {
        _isInsiderInside = false;
        _alarmSignal = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Insider"))
        {
            _alarmContinueTime = 0;
            _door.sprite = _openDoor;
            _alarmSignal.volume = 0;
            _alarmSignal.Play();
            _isInsiderInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _door.sprite = _closeDoor;
        _alarmSignal.Pause();
        _isInsiderInside = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isInsiderInside)
        {
            _alarmContinueTime += Time.deltaTime;
            _alarmSignal.volume = Mathf.MoveTowards(0, 1, _alarmContinueTime / _alarmFullVolumeTime);
        }
    }
}

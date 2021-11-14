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
    [SerializeField] private int _houseId;

    private AudioSource _alarmSignal;
    private float _alarmContinueTime;
    private Coroutine _changeVolumeCoroutine;
    private bool _isInsiderInside;

    private void Awake()
    {
        _alarmSignal = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && player.CanEnter(_houseId)==false)
        {
            _isInsiderInside = true;
            _door.sprite = _openDoor;
            _alarmSignal.volume = 0;
            _alarmSignal.Play();
            _changeVolumeCoroutine = StartCoroutine(ChangeVolume(true));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _door.sprite = _closeDoor;
        _isInsiderInside = false;
        StopCoroutine(_changeVolumeCoroutine);
        StartCoroutine(ChangeVolume(false));
    }

    private IEnumerator ChangeVolume(bool isIncrease)
    {
        float startPosition = _alarmSignal.volume;
        float endPosition = isIncrease == true ? 1 : 0;
        _alarmContinueTime = 0;

        while (_alarmSignal.volume != endPosition)
        {
            _alarmContinueTime += Time.deltaTime;
            _alarmSignal.volume = Mathf.MoveTowards(startPosition, endPosition, _alarmContinueTime / _alarmFullVolumeTime);
            yield return null;
        }

        if((isIncrease==false && _isInsiderInside) || isIncrease)
            _changeVolumeCoroutine = StartCoroutine(ChangeVolume(!isIncrease));
        else
            _alarmSignal.Pause();
    }
}

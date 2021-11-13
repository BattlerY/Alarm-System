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
    private Coroutine _volumeIncrease;

    private void Awake()
    {
        _alarmSignal = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && player.CanEnter(_houseId)==false)
        {
            _alarmContinueTime = 0;
            _door.sprite = _openDoor;
            _alarmSignal.volume = 0;
            _alarmSignal.Play();
            _volumeIncrease = StartCoroutine(IncreaseVolume());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _door.sprite = _closeDoor;
        _alarmSignal.Pause();
        StopCoroutine(_volumeIncrease);
    }

    private IEnumerator IncreaseVolume()
    {
        while (_alarmContinueTime<_alarmFullVolumeTime)
        {
            _alarmContinueTime = _alarmContinueTime + Time.deltaTime;
            _alarmSignal.volume = Mathf.MoveTowards(0, 1, _alarmContinueTime / _alarmFullVolumeTime);
            yield return null;
        }
    }
}

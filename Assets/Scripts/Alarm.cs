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
        bool isRun = true;
        int direction = 1;

        while (isRun)
        {
            _alarmContinueTime += Time.deltaTime * direction;
            _alarmSignal.volume = Mathf.MoveTowards(0, 1, _alarmContinueTime / _alarmFullVolumeTime);

            if (_alarmSignal.volume == 0 || _alarmSignal.volume == 1)
                direction *= -1;

            yield return null;
        }
    }
}

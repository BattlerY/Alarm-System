using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{https://github.com/BattlerY/Alarm-System/blob/main/Assets/Scripts/Player.cs
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.Translate(transform.up * _speed * Time.deltaTime * Input.GetAxis("Vertical"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.Translate(transform.up * _speed * Time.deltaTime * Input.GetAxis("Vertical"));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}

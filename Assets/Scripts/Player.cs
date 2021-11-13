using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private List<int> _houseKeys;

    private void Awake()
    {
        _houseKeys = new List<int>();
    }

    private void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.Translate(transform.up * _speed * Time.deltaTime * Input.GetAxis("Vertical"));
    }

    public bool CanEnter(int keyId) => _houseKeys.Contains(keyId);

}

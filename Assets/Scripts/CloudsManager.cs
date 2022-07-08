using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsManager : MonoBehaviour
{
  [SerializeField] private Transform _cloud, _spawn, _anotherSpawn;
  [SerializeField] private float _minScale, _maxScale;
  [SerializeField] private float _minSpeed, _maxSpeed;
  [SerializeField] private float _yOffset;
  private float _speed;
  private Vector3 _defaultScale;
  // Start is called before the first frame update

  private void Start() {
    _defaultScale = _cloud.localScale;
    Spawn();
  }
  private void FixedUpdate() {
    _cloud.position += _spawn.right * _speed;
    if (Mathf.Abs(transform.TransformPoint(_cloud.position)[0] - transform.TransformPoint(_anotherSpawn.position)[0]) < 10000f)
      Spawn();
  }

  private void Spawn() {
    _cloud.position = _spawn.position; 
    _cloud.localPosition += new Vector3(0f, 0f, Random.Range(0f, _yOffset));
    _speed = Random.Range(_minSpeed, _maxSpeed);
    _cloud.localScale = _defaultScale * Random.Range(_minScale, _maxScale);
  }

}

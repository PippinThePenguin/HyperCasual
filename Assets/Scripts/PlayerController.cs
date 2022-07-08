using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

  //public ElGeneriko.Cubes PlayerCubes;
  public ElGeneriko.PlayerCubes PlStats;
  [SerializeField] private Transform body;
  [SerializeField] private Transform cam;
  [SerializeField] private float lClamp, rClamp, modif;
  [SerializeField] private float previous;
  [SerializeField] private bool newTap;
  public UnityAction updateAction;
  private FloatingCubeManager floatingCubeManager;
  private ColorFlip flip;
  [SerializeField] private Spawner spawner;
  public Vector3 position;
  public Vector3 eposition;
  private Vector3 _lastPosition;
  private Vector3 _currPosition;
  [SerializeField] private float _step = 0f;
  [SerializeField] private float _stepStep = 0.5f;
  
  [SerializeField] private float prev;


  private void Awake() {
    PlStats = new ElGeneriko.PlayerCubes();
    //flip = FindObjectOfType<ColorFlip>();
  }
  void Start() {   
    
    updateAction = StartGame;
  }

  private void StartGame() {
    if (Input.touchCount > 0) {
      spawner.SpawnPattern3(spawner.Seq.StartNewSeq());
      updateAction = Inputer;
      //spawner.SpawnPattern3(spawner.Seq.StartNewSeq());
      Debug.Log("START");
    }
  }


  // Update is called once per frame
  void Update() {
    //updateAction?.Invoke();
    Inputer();
    LerpMover();
  }

  public void Inputer() {
    if (Input.touchCount > 0) {
      if (newTap) {
        newTap = false;
        position = Input.GetTouch(0).position;
        prev = 0f;
      } else {
        //PositionUpdate((Input.GetTouch(0).deltaPosition.x) * modif);
        eposition = Input.GetTouch(0).position;
      }

    } else {
      newTap = true;
      makeSwipe();
    }
  }

  private void makeSwipe() {
    
    Vector3 swipe = eposition - position;
    if (Math.Abs(swipe.x) > Math.Abs(swipe.y) && Math.Abs(swipe.x) > 10f) {
      float a = _currPosition.x + (swipe.x / Math.Abs(swipe.x)) * 25f;
      a = Mathf.Clamp(a, -25, 25);
      _lastPosition = _currPosition;
      _currPosition = new Vector3(a, _currPosition.y, _currPosition.z);
      _step = 0f;
      position = new Vector3();
      eposition = new Vector3();      
    }
  }
  
   private void LerpMover() {   
    if (body.position != _currPosition) {
      _step += _stepStep;
      _step = Mathf.Clamp01(_step);
      body.position = Vector3.Lerp(_lastPosition, _currPosition, _step);      
    }
  }
  private void PositionUpdate(float change) {
    float x = body.position.x;
    float b = change;
    if (Math.Abs(b) > 0.5) {
      x += change;
      prev = change;
    } else {
      x += change;
    }    
    //Debug.Log(x);
    x = Mathf.Clamp(x, lClamp, rClamp);
    //Debug.Log(x + " post");
    body.position = new Vector3(x, body.position.y, body.position.z);
    //cam.position = new Vector3(x, cam.position.y, cam.position.z);
  }

  public void CubeSwitch() {
    PlStats.Switch();
  }
}


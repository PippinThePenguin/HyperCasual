using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGates : MonoBehaviour
{
    [SerializeField] private float speed, speedstep;
    private ColorFlip flip;
    private bool go = false;
    //[SerializeField] private ElGeneriko.Color color;

    void Start()
    {
        go = false;
        flip = FindObjectOfType<ColorFlip>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (go)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
            speed += speedstep;
        }
    }

    public void StartGame()
    {
        go = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            //Debug.Log("pig");
            GameLinker.SceneGameLinker.playerScore.SwitchTo(ElGeneriko.Color.black);
            flip.ChangeColors(ElGeneriko.Color.black);
            gameObject.SetActive(false);
        }
    }
}

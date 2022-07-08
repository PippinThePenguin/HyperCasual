using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChecker : MonoBehaviour, IRoadBlocks
{
    [SerializeField] private float step;
    [SerializeField] private bool isActivated = false;

    [SerializeField] private PlayerController player;
    // Start is called before the first frame update

    public void OnStart()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - step);
    }

    public void Activation(string mes, int amount)
    {
        if (!isActivated)
        {
            isActivated = true;
            player.PlStats.GameCheck(player.PlStats.current, amount);
            //Debug.Log(mes);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        isActivated = false;
    }

    public void OnBlockPlace(BlockSet settings, float speed)
    {
        step = speed;
        isActivated = false;
    }
}

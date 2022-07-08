using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<CubeSequence> list;
    private int num, count, lastCount;

    [SerializeField] private float startSpeed, endSpeed, speedDelta, seqDelta;
    [SerializeField] private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<SeqLetter> GetSeq()
    {
        if (num == list.Capacity)
            num = 0;
        SpeedCheck();

        num += 1;
        count += 1;
        return SpeedUpdate(list[num - 1].MainSeq);
    }

    public List<SeqLetter> StartNewSeq()
    {
        num = 0;
        count = 0;
        lastCount = 0;
        currentSpeed = startSpeed;

        SpeedCheck();

        num += 1;
        count += 1;
        return SpeedUpdate(list[num - 1].MainSeq);
    }

    private void SpeedCheck()
    {
        if ((currentSpeed < endSpeed) && (count - lastCount >= seqDelta))
        {
            currentSpeed += speedDelta;
        }
    }

    private List<SeqLetter> SpeedUpdate(List<SeqLetter> income)
    {
        foreach (SeqLetter letter in income)
        {
            letter.speed = currentSpeed;
        }
        return income;
    }

}

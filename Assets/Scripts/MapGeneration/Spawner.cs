using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] public BlockSet BlockSettings;
    //public Transform pref;
    public UnityAction UpdateAction;
    private float time = 0;
    public List<string> names;
    public List<float> times;
    public PlayerController controller;
    public Sequenator Sequenator;
    public SequencePlacer Seq;
    public bool gameStoped = false;
    
    
    // Start is called before the first frame update
    void Start()
    {

        //NewSpawnPattern(Sequenator.SeqList[0]);
        controller.PlStats.EndEvent.AddListener(() => gameStoped = true);
        //SpawnPattern3(Seq.StartNewSeq());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateAction?.Invoke();
        /*if ((Time.time - time)> 2)
        {
            PoolBlocks.BlockPool.GetNextBlock(transform.position, "plum");
            time = Time.time;

        } */
    }

    async public void SpawnPattern(List<string> obstacles, List<float> timers)
    {
        
        if (obstacles.Count == timers.Count)
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                //new WaitForSeconds(timers[i]);
                await Task.Delay(TimeSpan.FromSeconds(timers[i]));
                PoolBlocks.BlockPool.GetNextBlock(transform.position, obstacles[i], BlockSettings, 0.1f);

            }
            SpawnPattern(names, times);
        }
        else
        {
            Debug.LogWarning("wrong pattern config");
        }
    }

    async public void NewSpawnPattern(Sequence seq)
    {

        for (int i = 0; i < seq.mainSeq.Count; i++)
        {
            //new WaitForSeconds(timers[i]);
            await Task.Delay(TimeSpan.FromSeconds(seq.mainSeq[i].preTime));
            PoolBlocks.BlockPool.GetNextBlock(transform.position, seq.mainSeq[i].tag, seq.mainSeq[i].settings, 0.1f);

        }
        NewSpawnPattern(seq);

    }
    async public void SpawnPattern3(List<SeqLetter> mainSeq)
    {
        if (gameStoped)
            return;

        for (int i = 0; i < mainSeq.Count; i++)
        {
            //new WaitForSeconds(timers[i]);
            await Task.Delay(TimeSpan.FromSeconds(mainSeq[i].preTime));
            if (gameStoped)
                return;
            PoolBlocks.BlockPool.GetNextBlock(transform.position, mainSeq[i].tag, mainSeq[i].settings, mainSeq[i].speed);

        }
        SpawnPattern3(Seq.GetSeq());

    }

    public void Continue()
    {
        
        gameStoped = false;
        SpawnPattern3(Seq.StartNewSeq());
    }


}

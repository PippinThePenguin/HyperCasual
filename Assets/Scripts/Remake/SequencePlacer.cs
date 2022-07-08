using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePlacer : MonoBehaviour
{
    public bool dothing = false;
    public BlockSequence block;
    [SerializeField] private List<BlockSequence> list;
    [SerializeField] private int currentSeq, speedSeq, lastCount;

    [SerializeField] private float startSpeed, endSpeed, speedDelta, seqDelta;
    [SerializeField] private float currentSpeed;
    [SerializeField] private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        currentSeq = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (dothing)
            block = CreateRandomSeq(SeqColorBlockType.Both);
    }

    public List<SeqLetter> GetSeq()
    {
        PlayerScore score = new PlayerScore();
        score.whiteScore = player.PlStats.white.number;
        score.blackScore = player.PlStats.black.number;
        score.Score = score.whiteScore + score.blackScore;
        BlockSequence set = ChoseNextSeq(score);
        SpeedCheck();

        currentSeq += 1;
        speedSeq += 1;
        return SpeedUpdate(set.Sequence);
    }

    public List<SeqLetter> StartNewSeq()
    {
        currentSeq = 0;
        speedSeq = 0;
        
        currentSpeed = startSpeed;

        SpeedCheck();

        currentSeq += 1;
        speedSeq += 1;
        return SpeedUpdate(CreateRandomSeq(SeqColorBlockType.NoColor).Sequence);
    }

    private void SpeedCheck()
    {
        if ((currentSpeed < endSpeed) && (speedSeq >= seqDelta))
        {
            currentSpeed += speedDelta;
            speedSeq = 0;
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

    private BlockSequence ChoseNextSeq(PlayerScore playerScore)
    {
        List<int> blackAndWhite = playerScore.GetScore();
        int sum = playerScore.Score;

        float randomised = Random.value;

        float tresh = ((((float)Mathf.Max(blackAndWhite[0], blackAndWhite[1]))/sum)*2 -1);

        Debug.Log(randomised + " random, " + tresh + " treshhold  " + ((float)Mathf.Max(blackAndWhite[0], blackAndWhite[1])) / sum);
        if (randomised <= tresh)
        {
            //do mono
            if (blackAndWhite[0] > blackAndWhite[1])
            {
                //do white
                return CreateRandomSeq(SeqColorBlockType.White, Random.Range(2, 6));
            }
            else
            {
                //do black
                return CreateRandomSeq(SeqColorBlockType.Black, Random.Range(2, 6));
            }
        }
        else if (randomised < (tresh + (1 - tresh)/2))
        {
            //do freegates
            return CreateRandomSeq(SeqColorBlockType.Both, Random.Range(2, 6));

        }
        else{
            //do nogates
            return CreateRandomSeq(SeqColorBlockType.NoColor, Random.Range(2, 6));
        }
        
    }


    private BlockSequence CreateRandomSeq(SeqColorBlockType type, int length = 5)
    {
        List<SeqLetter>  sequence = new List<SeqLetter>();
        ElGeneriko.MathCubes wcu = new ElGeneriko.MathCubes(true, new ElGeneriko.Cubes(ElGeneriko.Color.white, 1));
        ElGeneriko.MathCubes bcu = new ElGeneriko.MathCubes(true, new ElGeneriko.Cubes(ElGeneriko.Color.black, 1));

        float sec = Random.Range(1.3f, 2.6f);
        if (type == SeqColorBlockType.Both)
        {
            BlockSet blockSet = new BlockSet(new List<ElGeneriko.MathCubes>());
            blockSet.AddCube(wcu);
            blockSet.AddCube(bcu);
            sequence.Add(new SeqLetter("color", sec, blockSet));
        }
        else if (type == SeqColorBlockType.Black)
        {
            BlockSet blockSet = new BlockSet(new List<ElGeneriko.MathCubes>());
            blockSet.AddCube(new ElGeneriko.MathCubes(true, new ElGeneriko.Cubes(ElGeneriko.Color.black, 1)));
            blockSet.AddCube(new ElGeneriko.MathCubes(true, new ElGeneriko.Cubes(ElGeneriko.Color.black, 1)));
            sequence.Add(new SeqLetter("onecolor", sec, blockSet));
        }
        else if (type == SeqColorBlockType.White)
        {
            BlockSet blockSet = new BlockSet(new List<ElGeneriko.MathCubes>());
            blockSet.AddCube(new ElGeneriko.MathCubes(true, new ElGeneriko.Cubes(ElGeneriko.Color.white, 1)));
            blockSet.AddCube(new ElGeneriko.MathCubes(true, new ElGeneriko.Cubes(ElGeneriko.Color.white, 1)));
            sequence.Add(new SeqLetter("onecolor", sec, blockSet));
        }
        for (int i = 1; i < length; i++ )
        {
            //Debug.Log(i);
            BlockSet blockSet = new BlockSet(new List<ElGeneriko.MathCubes>());
            blockSet.AddCube(new ElGeneriko.MathCubes(true, new ElGeneriko.Cubes(ElGeneriko.Color.white, 1)));
            blockSet.AddCube(new ElGeneriko.MathCubes(true, new ElGeneriko.Cubes(ElGeneriko.Color.black, 1)));
            float sec2 = Random.Range(0.8f, 1.6f);
            sequence.Add(new SeqLetter("plum", sec2, blockSet));
        }

        return new BlockSequence(sequence);
    }
}

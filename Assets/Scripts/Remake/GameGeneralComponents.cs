using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameColor
{
    Black,
    White,
}

public class PlayerScore
{
    public GameColor PlayerColor;
    public int Score;

    public UnityEvent EndEvent;
    public UnityEvent MathEvent;
    public UnityEvent<GameColor> ColorEvent;

    public int whiteScore, blackScore;

    public PlayerScore(GameColor color = GameColor.White, int startscore = 0)
    {
        PlayerColor = color;
        Score = startscore;
        whiteScore = Score / 2;
        blackScore = Score / 2;
        EndEvent = new UnityEvent();
        MathEvent = new UnityEvent();
        ColorEvent = new UnityEvent<GameColor>();
    }

    public void Add(GameStat stats)
    {
        if (stats.GameColor == PlayerColor)
        {
            Score += stats.Value;
            if (PlayerColor == GameColor.Black)
                blackScore += stats.Value;
            else
                whiteScore += stats.Value;
            MathEvent?.Invoke();
        }
        else
        {
            EndEvent?.Invoke();
        }
    }

    public void SwitchColorTo(GameColor targetColor)
    {
        PlayerColor = targetColor;
        ColorEvent?.Invoke(PlayerColor);
    }

    public List<int> GetScore()
    {
        List<int> bw = new List<int>();
        bw.Add(blackScore);
        bw.Add(whiteScore);
        return bw;
    }

}

public class GameStat
{
    public GameColor GameColor;
    public int Value;
   
    public GameStat(GameColor color = GameColor.White, int val = 0)
    {
        GameColor = color;
        Value = val;        
    }
}

[Serializable]
public class BlockSettings
{
    public List<GameStat> CellsSettings;
    public bool Randomise;

    public BlockSettings(List<GameStat> newSet = null, bool rand = false)
    {
        CellsSettings = newSet;
        Randomise = rand;
    }

    public void AddCell(GameStat newCell)
    {
        CellsSettings.Add(newCell);
    }
}

[Serializable]
public class SeqBlockSettings
{
    public string BlockTag;
    public float PreTime;
    public float Speed;
    public BlockSettings Settings;

    public SeqBlockSettings(string targetTag, float timeBeforeSpawn, BlockSettings blockSettings)
    {
        BlockTag = targetTag;
        PreTime = timeBeforeSpawn;
        Settings = blockSettings;
    }
}

public class GameSequenceComp
{   
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BlockSet
{
    public List<ElGeneriko.MathCubes> CellsSet;
    public bool Randomise;

    public BlockSet(List<ElGeneriko.MathCubes> newSet = null, bool rand = true)
    {
        CellsSet = newSet;
        Randomise = rand;
    }

    public void AddCube(ElGeneriko.MathCubes cube)
    {
        CellsSet.Add(cube);
    }
}
[Serializable]
public class Sequence
{
    public List<SeqLetter> mainSeq;
    public Sequence(List<SeqLetter> incomeSeq = null)
    {
        mainSeq = new List<SeqLetter>();
        mainSeq = incomeSeq;
    }

}
[Serializable]
public class SeqLetter
{
    public string tag;
    public float preTime;
    public float speed;
    public BlockSet settings;

    public SeqLetter(string targetTag, float timeBeforeSpawn, BlockSet blockSettings)
    {
        tag = targetTag;
        preTime = timeBeforeSpawn;
        settings = blockSettings;
    }
}

public class ElGeneriko
{
    [Serializable]
    public enum Color
    {
        black,
        white,
        pink,
        green,
        red,
        blue,
    }
    public class PlayerCubes
    {
        public Cubes white, black, current;
        public UnityEvent EndEvent;
        public UnityEvent<Cubes> MathEvent;
        public UnityEvent<Color> ColorEvent;
        public PlayerCubes()
        {
            white = new Cubes(Color.white, 0);
            black = new Cubes(Color.black, 0);
            white.EndEvent = EndEvent;
            current = white;
            EndEvent = new UnityEvent();
            MathEvent = new UnityEvent<Cubes>();
            ColorEvent = new UnityEvent<Color>();

        }
        
        public void Switch()
        {
            if (current == white)
            {
                current = black;
                Debug.LogWarning("Black!");
            }
            else if (current == black)
            {
                current = white;
                Debug.LogWarning("White!");
            }
            else
            {
                Debug.LogWarning("Cube Switch Failed!");
            }
        }

        public void SwitchTo(Color newCol)
        {
            if (newCol == Color.white)
            {
                current = white;
                Debug.Log("White!");
            }
            else if (newCol == Color.black)
            {
                current = black;
                Debug.Log("Black!");
            }
            else
            {
                Debug.LogWarning("Cube Switch Failed!");
            }
            ColorEvent?.Invoke(newCol);
        }

        public void Math(MathCubes math)
        {
            if (math.IsAdd)
                Add(math.CubeStats);
            else
                Multiply(math.CubeStats);
        }
        
        public void Add(Cubes ext)
        {
            if (ext.color != current.color)
            {
                EndEvent?.Invoke();
                return;
            }
            current.Add(ext);
            MathEvent?.Invoke(current);
            GameCheck(current);
        }
        public void Multiply(Cubes ext)
        {
            if (ext.color != current.color)
            {
                EndEvent?.Invoke();
                return;
            }
            current.Multiply(ext);
            MathEvent?.Invoke(current);
            GameCheck(current);
        }
        
        public void GameCheck(Cubes color, int treshhold  = 1)
        {
            if (color.number < treshhold)
            {
                //endgame
                EndEvent?.Invoke();
            }
        }
    }
    [Serializable]
    public class Cubes
    {
        public Color color;
        public int number;
        public UnityEvent EndEvent;

        public Cubes(Color col, int num)
        {
            color = col;
            number = num;
        }
        public void Add(Cubes ext)
        {
            if (ext.color == color)
            {
                number += ext.number;
            }
            else
            {
                
                number -= ext.number;
            }
            number = Mathf.Clamp(number, 0, 100);
            //Debug.Log(number);
        }
        public void Multiply(Cubes ext)
        {
            if (ext.color == color)
                number *= ext.number;
            else
                number /= ext.number;
        }
    }
    [Serializable]
    public class MathCubes
    {
        public Cubes CubeStats;
        public bool IsAdd;
        public Color color;
        public int number;
        public MathCubes(bool addition = true, Cubes cubestats = null)
        {
            IsAdd = addition;
            CubeStats = cubestats;
            color = CubeStats.color;
            number = CubeStats.number;
        }
    }
}

public interface IRoadBlocks
{
    public void OnStart();

    public void OnBlockPlace(BlockSet blockSet, float speed);
}

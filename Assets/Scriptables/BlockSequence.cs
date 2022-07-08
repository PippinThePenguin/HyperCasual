using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeqColorBlockType
{
    Both,
    Black,
    White,
    NoColor,
}


[CreateAssetMenu(fileName = "new Sequence", menuName = "Sequence")]
public class BlockSequence : ScriptableObject
{
    public new string name;
    public List<SeqLetter> Sequence;
    public SeqColorBlockType Type;
    public bool IsRandomised;
    public int Difficulty = 0;
    public BlockSequence(List<SeqLetter> incomeSeq = null)
    {
        Sequence = new List<SeqLetter>();
        Sequence = incomeSeq;
    }




}

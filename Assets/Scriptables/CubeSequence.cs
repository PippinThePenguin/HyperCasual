using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SequenceType
{
    comonSeq,
    blackSeq,
    whiteSeq,
}

[CreateAssetMenu(fileName = "new Sequence", menuName = "Sequence")]
public class CubeSequence : ScriptableObject
{
    public new string name;
    public List<SeqLetter> MainSeq;
    public SequenceType Type;
    public CubeSequence(List<SeqLetter> incomeSeq = null)
    {
        MainSeq = new List<SeqLetter>();
        MainSeq = incomeSeq;
    }




}

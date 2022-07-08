using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Sequenator : MonoBehaviour
{
    [HideInInspector] public List<Sequence> SeqList;
    //[SerializeField]
    public Sequence EditedSeq;
    private Sequence hideseq;

    private void Awake()
    {
        ClearCash();
        DontDestroyOnLoad(this);
    }
    public void ClearBase()
    {
        SeqList = new List<Sequence>();
    }

    public void ClearCash()
    {
        EditedSeq = new Sequence();
        hideseq = new Sequence();
    }

    public void AddCash()
    {
        hideseq = EditedSeq;
        SeqList.Add(hideseq);
        //ClearCash();
    }

    public void Pig()
    {

    }
}






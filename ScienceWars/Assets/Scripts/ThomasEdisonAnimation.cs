using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThomasEdisonAnimation : MonoBehaviour {
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void walk(bool move)
    {
        anim.SetBool(EdisonParam.walk, move);
    }
    
    public void defence(bool def)
    {
        anim.SetBool(EdisonParam.defence,def);
    }

    public void oneHand()
    {
        anim.SetTrigger(EdisonParam.oneHand);
    }

    public void slam1()
    {
        anim.SetTrigger(EdisonParam.slam1);
    }

    public void combo1()
    {
        anim.SetTrigger(EdisonParam.combo1);
    }
    public void slam1cmpt()
    {
        anim.SetTrigger(EdisonParam.slam1cmpt);
    }
    public void slam1reset()
    {
        anim.ResetTrigger(EdisonParam.slam1cmpt);
    }
}

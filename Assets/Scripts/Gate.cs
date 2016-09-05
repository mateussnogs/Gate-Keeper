using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
    [SerializeField]
    private Animator anim;



    public void GetHit()
    {
        anim.SetTrigger("Hit");


    }
}

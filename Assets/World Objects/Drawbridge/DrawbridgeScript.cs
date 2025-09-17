using UnityEngine;

public class DrawbridgeScript : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Trigger()
    {
        anim.SetTrigger("Move");
    }
}

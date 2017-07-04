using UnityEngine;
using System.Collections;

public class NPCMotor : MonoBehaviour
{
    TextBoxManager tBoxManager;
    Animator animator;
    bool isActivator;

    void Start ()
    {
        tBoxManager = FindObjectOfType<TextBoxManager>();
        animator = GetComponent<Animator>();
        isActivator = tBoxManager.canTalk;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Talk();
	}

    void Talk()
    {
        if(tBoxManager.isActive)
        {
            if (isActivator == false)
                animator.SetBool("isTalking", true);
            else if (isActivator == true)
                animator.SetBool("isTalking", false);
        }
    }
}

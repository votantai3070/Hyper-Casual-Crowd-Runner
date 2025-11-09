using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Transform runnersParent;

    public void Run()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Animator anim = runnersParent.GetChild(i).GetComponent<Animator>();

            anim.Play("Run");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Animator anim = runnersParent.GetChild(i).GetComponent<Animator>();

            anim.Play("Idle");
        }
    }
}

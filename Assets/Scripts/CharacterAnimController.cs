using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    void ResetStates()
    {
        _anim.SetBool("isIdle", false);
        _anim.SetBool("isWalk", false);
        _anim.SetBool("isAction1", false);
        _anim.SetBool("isAction2", false);
    }

    public void EnableIdle()
    {
        _anim.SetBool("isIdle", true);
    }

    public void DisableIdle()
    {
        _anim.SetBool("isIdle", false);
    }

    public void EnableWalk()
    {
        _anim.SetBool("isWalk", true);
    }

    public void DisableWalk()
    {
        _anim.SetBool("isWalk", false);
    }

    public void EnableAction1()
    {
        _anim.SetBool("isAction1", true);
    }

    public void DisableAction1()
    {
        _anim.SetBool("isAction1", false);
    }

    public void EnableAction2()
    {
        _anim.SetBool("isAction2", true);
    }

    public void DisableAction2()
    {
        _anim.SetBool("isAction2", false);
    }
}

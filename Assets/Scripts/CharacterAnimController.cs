using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    [Header("Animation States (Inspector Editable)")]
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isWalk;
    [SerializeField] private bool isAction1;
    [SerializeField] private bool isAction2;

    [Header("Animator")]
    [SerializeField] private Animator _anim;

    // 캐시된 이전 값들
    private bool prevIdle;
    private bool prevWalk;
    private bool prevAction1;
    private bool prevAction2;

    void Update()
    {
        if (_anim == null) return;

        if (prevIdle != isIdle)
        {
            _anim.SetBool("isIdle", isIdle);
            prevIdle = isIdle;
        }

        if (prevWalk != isWalk)
        {
            _anim.SetBool("isWalk", isWalk);
            prevWalk = isWalk;
        }

        if (prevAction1 != isAction1)
        {
            _anim.SetBool("isAction1", isAction1);
            prevAction1 = isAction1;
        }

        if (prevAction2 != isAction2)
        {
            _anim.SetBool("isAction2", isAction2);
            prevAction2 = isAction2;
        }
    }

    void ResetStates()
    {
        _anim.SetBool("isIdle", false);
        _anim.SetBool("isWalk", false);
        _anim.SetBool("isAction1", false);
        _anim.SetBool("isAction2", false);

        isIdle = prevIdle = false;
        isWalk = prevWalk = false;
        isAction1 = prevAction1 = false;
        isAction2 = prevAction2 = false;
    }

    public void EnableIdle()
    {
        _anim.SetBool("isIdle", true);
        isIdle = prevIdle = true;
    }

    public void DisableIdle()
    {
        _anim.SetBool("isIdle", false);
        isIdle = prevIdle = false;
    }

    public void EnableWalk()
    {
        _anim.SetBool("isWalk", true);
        isWalk = prevWalk = true;
    }

    public void DisableWalk()
    {
        _anim.SetBool("isWalk", false);
        isWalk = prevWalk = false;
    }

    public void EnableAction1()
    {
        _anim.SetBool("isAction1", true);
        isAction1 = prevAction1 = true;
    }

    public void DisableAction1()
    {
        _anim.SetBool("isAction1", false);
        isAction1 = prevAction1 = false;
    }

    public void EnableAction2()
    {
        _anim.SetBool("isAction2", true);
        isAction2 = prevAction2 = true;
    }

    public void DisableAction2()
    {
        _anim.SetBool("isAction2", false);
        isAction2 = prevAction2 = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    [Header("Walk")]
    [SerializeField] private List<AudioClip> _walk;

    [Header("Attack")]
    [SerializeField] private List<AudioClip> _attack;

    [Header("Dash")]
    [SerializeField] private List<AudioClip> _dash;

    [Header("Jump")]
    [SerializeField] private List<AudioClip> _jump;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void playRandomClip(List<AudioClip> pAudioClips)
    {
        int random = Random.Range(0, pAudioClips.Count);
        //_audioSource.PlayOneShot(pAudioClips[random]); //TODO: all ready for sounds
    }

    public void AttackAnimation()
    {
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        if (IsJumping)
        {
            _animator.Play("PlayerJumpAttack");
        }
        else if (!IsAttacking)
        {
            _animator.Play("PlayerAttack");
        }
        else
        {
            if (currentState.IsName("PlayerAttack"))
            {
                _animator.Play("PlayerAttack2");
            }
        }
    }

    public void DashAnimation()
    {
        playRandomClip(_dash);
        _animator.Play("PlayerDash");
    }

    public void JumpAnimation()
    {
        playRandomClip(_jump);
        _animator.Play("PlayerJump");
    }

    public void WalkAnimation()
    {
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        if (!currentState.IsName("PlayerWalk") && !IsAttacking)
        {
            _animator.Play("PlayerWalk");
        }
    }

    public void StopWalking()
    {
        if (!IsAttacking)
        {
            StopAll();
        }
    }

    public void DamageAnimation()
    {
        _animator.Play("PlayerDamage");
    }

    public void ForceIdleAnimation()
    {
        _animator.Play("PlayerIdle");
    }

    public void StopAll()
    {
        if (IsDamaged || IsJumping || IsDashing)
        {
            return;
        }

        _animator.Play("PlayerIdle");
    }

    public void FreezeAnimations()
    {
        _animator.speed = 0;
    }

    public void ResumeAnimation()
    {
        _animator.speed = 1;
    }

    private void walkSound()
    {
        playRandomClip(_walk);
    }

    public bool IsAttacking
    {
        get
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            return (currentState.IsName("PlayerAttack") || (currentState.IsName("PlayerAttack2")));
        }
    }

    public bool IsJumping
    {
        get
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            return (currentState.IsName("PlayerJump") || currentState.IsName("PlayerJumpAttack"));
        }
    }

    public bool IsDamaged
    {
        get
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            return (currentState.IsName("PlayerDamage"));
        }
    }

    public bool IsDashing
    {
        get
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            return (currentState.IsName("PlayerDash"));
        }
    }
}

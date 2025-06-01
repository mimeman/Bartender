using System.Collections;
using UnityEngine;

public class NPCAnimationHandler : MonoBehaviour
{
    [Header("Animator")]
    [Tooltip("NPC�� Animator ������Ʈ")]
    public Animator animator;

    private string currentBoolAnimation; // ���� Ȱ��ȭ�� Bool �Ķ���� �̸�


    // Bool Ÿ�� �ִϸ��̼��� �Ѱ�, ���� �ִϸ��̼��� �ڵ����� ���ش�.
    public void SetAnimation(string animationName, bool state)
    {
        if (currentBoolAnimation == animationName && animator.GetBool(animationName) == state)
            return;

        // ���� �ִϸ��̼� ��Ȱ��ȭ
        if (!string.IsNullOrEmpty(currentBoolAnimation) && currentBoolAnimation != animationName)
        {
            animator.SetBool(currentBoolAnimation, false);
        }

        animator.SetBool(animationName, state);
        currentBoolAnimation = state ? animationName : "";
    }

    // Trigger �ִϸ��̼� ����

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }


    // ���� ���� ���� �ִϸ��̼��� �̸��� ��ġ�ϰ�, �Ϸ�Ǿ����� üũ
    public bool CheckFinishAnimation(string animationStateName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationStateName) && stateInfo.normalizedTime >= 1.0f;
    }


}

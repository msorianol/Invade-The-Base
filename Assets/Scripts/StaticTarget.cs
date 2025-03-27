using UnityEngine;

public class StaticTarget : MonoBehaviour
{
    [SerializeField] private ShootingRange m_ShootingRange;
    [SerializeField] private Animator m_StaticTargetAnimator;
    private int m_HitCounter;

    public void StaticRegisterHit()
    {
        m_HitCounter++;
        m_ShootingRange.m_Score += 50;

        if (m_HitCounter >= 3)
        {
            m_StaticTargetAnimator.SetBool("TargetFall", true);
            m_StaticTargetAnimator.SetBool("TargetRise", false);
            m_HitCounter = 0;
        }
    }
}

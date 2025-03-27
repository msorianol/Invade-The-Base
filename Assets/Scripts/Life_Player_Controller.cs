using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Life_Player_Controller : MonoBehaviour
{
    [SerializeField] private Slider m_HealthScrollBar;
    [SerializeField] private Slider m_LerpHealthBar;
    [SerializeField] private Slider m_LerpShieldBar;
    [SerializeField] private Slider m_ShieldScrollBar;
    [SerializeField] private float lerpSpeed = 0.05f;
    private Player_Controller m_PlayerController;
    private WeaponController m_WeaponController;
    private Animator m_Animator;

    private float m_MaxPlayerHealth = 100;
    private float m_Health;
    private float m_MaxShield = 100;
    private float m_ShieldAmount;
    private bool m_PlayerShield;

    public bool m_PlayerCanPickHealth = false;
    public bool m_PlayerCanPickShield = false;

    private void OnEnable()
    {
        LifeItem.OnHealthPicked += UpdateHealth;
        ShieldItem.OnShieldPicked += UpdateShield;
    }

    private void OnDisable()
    {
        LifeItem.OnHealthPicked -= UpdateHealth;
        ShieldItem.OnShieldPicked -= UpdateShield;
    }

    private void Start()
    {
        m_PlayerController = GetComponent<Player_Controller>();
        m_WeaponController = GetComponent<WeaponController>();
        m_Health = m_MaxPlayerHealth;
        m_ShieldAmount = m_MaxShield;
        m_PlayerShield = true;
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (m_HealthScrollBar.value != m_LerpHealthBar.value)
        {
            m_LerpHealthBar.value = Mathf.Lerp(m_LerpHealthBar.value, m_Health, lerpSpeed * Time.deltaTime);
        }

        if (m_ShieldScrollBar.value != m_LerpShieldBar.value)
        {
            m_LerpShieldBar.value = Mathf.Lerp(m_LerpShieldBar.value, m_ShieldAmount, lerpSpeed * Time.deltaTime);
        }

        m_HealthScrollBar.value = m_Health;
        m_ShieldScrollBar.value = m_ShieldAmount;

        if (m_ShieldAmount > m_MaxShield)
            m_ShieldAmount = 100;

        if (m_Health > m_MaxPlayerHealth)
            m_Health = 100;

        if (m_Health <= 0)
        {
            m_Animator.SetBool("Death", true);
            Death();
        }

        if (m_ShieldAmount < 0)
        {
            m_PlayerShield = false;
            m_ShieldAmount = 0;
        }

        if (m_Health < m_MaxPlayerHealth)
            m_PlayerCanPickHealth = true;
        else
            m_PlayerCanPickHealth = false;

        if (m_ShieldAmount < m_MaxShield)
            m_PlayerCanPickShield = true;
        else
            m_PlayerCanPickShield = false;

        if (Input.GetKeyDown(KeyCode.M))
        {
            m_Health -= 10.0f;
            m_ShieldAmount -= 10.0f;
        }
    }

    public void UpdateHealth(float value)
    {
        if (value < 0)
        {
            if (m_PlayerShield)
            {
                m_ShieldAmount += value * 0.75f;
                m_Health += value * 0.25f;
            }
            else
                m_Health += value;
        }
        else
            m_Health += value;
    }

    public void UpdateShield(float value)
    {
        m_ShieldAmount += value;
    }

    private void Death()
    {
        GameManager.instance.ReStartGame(false);
    }

    public void KilledByDeadZone()
    {
        m_Health = 0;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("DeadZone"))
        {
            KilledByDeadZone();
        }
    }
}

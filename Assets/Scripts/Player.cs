using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Player_Controller m_PlayerController; 
    private Life_Player_Controller m_LifePlayerController;
    public GameObject m_PlayerPosition; 
    private Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();  
        m_LifePlayerController = GetComponent<Life_Player_Controller>();
        m_PlayerController = GetComponent<Player_Controller>(); 
    }

    public Vector3 GetPlayerPosition()
    {
        return m_PlayerPosition.transform.position;
    }

    public void StopAnimation()
    {
        m_Animator.SetBool("Aim", false);
        m_Animator.SetBool("Shooting", false);
        m_Animator.SetBool("Reloading", false);
        m_Animator.SetBool("Walking", false);
    }
}

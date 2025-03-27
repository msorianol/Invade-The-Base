using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject m_DeathUI;
    [SerializeField] private GameObject m_GameUI;
    [SerializeField] private Player m_Player; 
    [SerializeField] private Player_Controller m_PlayerController;
    [SerializeField] private Weapon_Controller m_WeaponController;
    [SerializeField] private Life_Player_Controller m_LifePlayerController;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private GameObject m_BulletDecal;
    [SerializeField] int m_MaxBulletEnemyOnScene;

    [SerializeField] private TMP_Text m_TextCanvas; 

    private CPoolElemt m_CPoolBullet;
    private CPoolElemt m_CPoolBulletDecal;
    public Vector3 m_direction;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_CPoolBullet = new CPoolElemt(m_MaxBulletEnemyOnScene, m_Bullet);
        m_CPoolBulletDecal = new CPoolElemt(m_MaxBulletEnemyOnScene, m_BulletDecal);
    }
    
    public void ReStartGame(bool l_EndGame)
    {
        if (l_EndGame)
        {
            m_Player.StopAnimation();   
            m_TextCanvas.text = "You Win";
        }

        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        m_PlayerController.enabled = false;
        m_WeaponController.enabled = false;
        m_GameUI.SetActive(false);  
        m_DeathUI.SetActive(true);
    }
  
    public void SetPlayer(Player l_player)
    {
        m_Player = l_player;    
    }

    public Player GetPlayer()
    {
        return m_Player;
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);       
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public GameObject GetBullet()
    {
        return m_CPoolBullet.GetNextElement();
    }

    public GameObject GetBulletDecal() 
    {
        return m_CPoolBulletDecal.GetNextElement();
    }

    public void BulletDestroy(GameObject l_bullet)
    {
        m_CPoolBulletDecal.m_Pool.Remove(l_bullet);

        if(m_CPoolBulletDecal.m_Pool.Remove(l_bullet))
            m_CPoolBulletDecal.AddBullet(m_BulletDecal);           
    }

    public void GetPlayer(Player l_Player)
    {
        m_Player = l_Player;    
    }

    public bool CanPickHealth()
    {
        if (m_LifePlayerController.m_PlayerCanPickHealth)
            return true;
        else
            return false;
    }

    public bool CanPickAmmo()
    {
        if (m_WeaponController.m_CanCollectAmmo)
            return true;
        else 
            return false;
    }

    public bool CanPickShield()
    {
        if (m_LifePlayerController.m_PlayerCanPickShield)
            return true;
        else
            return false;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll(); 
        PlayerPrefs.Save();      
    }
}

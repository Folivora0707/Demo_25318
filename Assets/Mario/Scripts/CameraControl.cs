using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform m_traPlayer;
    private void Awake()
    {
        m_traPlayer = GameObject.FindWithTag("Player").transform;
        //Debug.Log("捕获玩家坐标：" + m_traPlayer.position.x + " " + m_traPlayer.position.y);
    }

    private void LateUpdate()
    {
        CameraFollow();
        CheckBorder();
    }

    // 相机跟随
    private void CameraFollow()
    {
        Vector3 v3Position = transform.position;
        v3Position.x = m_traPlayer.position.x;
        v3Position.y = m_traPlayer.position.y;

        transform.position = v3Position;
    }

    #region 检查相机是否越界 

    [SerializeField] private float m_fX_Left = 3f;
    [SerializeField] private float m_fX_Right = 202f; 
    [SerializeField] private float m_fY_Up = 2f;
    [SerializeField] private float m_fY_Down = -10f;
    [SerializeField] private float m_fY_Mid = -4f;
    private void CheckBorder()
    {
        Vector3 v3Postion = transform.position;
        if (v3Postion.x < m_fX_Left) v3Postion.x = m_fX_Left;
        if (v3Postion.x > m_fX_Right) v3Postion.x = m_fX_Right;
        if (v3Postion.y > m_fY_Mid) v3Postion.y = m_fY_Up;
        else v3Postion.y = m_fY_Down;

        transform.position = v3Postion;
    }

    #endregion

}

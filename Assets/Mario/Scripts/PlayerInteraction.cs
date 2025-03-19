using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Rigidbody2D m_rbPlayer;

    private void Awake()
    {
        //m_rbPlayer = GetComponentInChildren<Rigidbody2D>(false); // 第一个激活的子对象
        m_rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckInput();
    }

    #region 输入检测

    private float m_fHorizontalAxis;
    private Vector2 m_v2Velocity;
    [SerializeField] private float m_fSpeed = 8f;

    private void CheckInput()
    {
        m_fHorizontalAxis = Input.GetAxis("Horizontal");
        m_v2Velocity.x = Mathf.MoveTowards(m_v2Velocity.x, m_fHorizontalAxis * m_fSpeed, m_fSpeed * Time.deltaTime);
    }

    #endregion

    private void FixedUpdate()
    {
        PlayerRun();
    }

    #region 角色移动

    private void PlayerRun()
    {
        Vector3 v3Pos = transform.position;
        Vector2 v2Pos = m_v2Velocity * Time.fixedDeltaTime;
        v3Pos.x += v2Pos.x;
        v3Pos.x = KeepPosInCamera(v3Pos.x);
        v3Pos.y += v2Pos.y;

        transform.position = v3Pos;
    }

    // 保证角色在横向不会走出相机范围
    private float KeepPosInCamera(float fX)
    {
        Vector2 v2Left = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 v2Right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        return Mathf.Clamp(fX, v2Left.x, v2Right.x);
    }

    #endregion

    #region 角色跳跃

    [SerializeField] private float m_fMaxJumpHeight = 5f;
    [SerializeField] private float m_fMaxJumpTime = 1f;

    public float m_fJumpForce => (2f * m_fMaxJumpHeight) / (m_fMaxJumpTime / 2f);
    public float m_fGravity => (-2f * m_fMaxJumpHeight) / Mathf.Pow((m_fMaxJumpTime / 2f), 2);

    public bool m_bGrounded { get; private set; }
    public bool m_bJumping { get; private set; }



    #endregion

}

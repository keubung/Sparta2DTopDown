using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    // 실제로 이동이 일어날 컴포넌트

    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private CharacterStatsHandler characterStatsHandler;

    private Vector2 movementDirection = Vector2.zero;
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private void Awake()    // 주로 내 컴포넌트 안에서 끝나는 것, 보통 다른 컴프넌트의 값을 변경하지 않음.
    {
        // controller랑 TopDownMovement랑 같은 게임오브젝트 안에 있다라는 가정
        controller = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void FixedUpdate()
    {
        // FixedUpdate는 물리업데이트관련
        // rigidbody의 값을 바꾸니까 FixedUpdate에서 한다.
        //
        ApplyMovement(movementDirection);

        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    public void ApplyKnockback(Transform Other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(Other.position - transform.position).normalized * power;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatsHandler.CurrentStat.speed;

        if(knockbackDuration > 0.0f)
        {
            direction += knockback;
        }

        movementRigidbody.velocity = direction;
    }
}
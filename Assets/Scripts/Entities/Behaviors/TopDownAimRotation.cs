using System;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRenderer;

    private TopDownController controller;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        characterRenderer.flipX = Mathf.Abs(rotz) > 90f;
        armRenderer.flipY = characterRenderer.flipX;
        armPivot.rotation = Quaternion.Euler(0, 0, rotz);
    }
}
/*
 혹시 타일맵 자르실 때 흐릿한 부분 때문에 이상하게 잘리시는 분들은 
 해당 이미지 파일의 인스펙터에서 필터를 꺼 주시면 선명해지면서 정상적으로 잘리게 될겁니다. (방금 경험함.)
 */
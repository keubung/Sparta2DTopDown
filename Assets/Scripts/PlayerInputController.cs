using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownController
{
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;   // mainCamera 태그가 붙어있는 카메라를 가져온다
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
        // 실제 움직이는 처리는 여기서 하는게 아니라 PlayerrMovement에서 함
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();  // 마우스 위치를 받아오는 것이기 때문에 normalized를 하면 안됨
                                                // 마우스를 움직일 때마다 마우스 기준의 왼쪽 오른쪽이 달라지기 때문
        Vector2 worldPos = camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        CallLookEvent(newAim);
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}

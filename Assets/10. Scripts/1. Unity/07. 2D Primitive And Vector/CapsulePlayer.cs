using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.PrimitiveAndVector
{
    public class CapsulePlayer : MonoBehaviour
    {
        // 캡슐 플레이어
        // 1. 화살표(좌, 우)를 이용한 이동 및 표현
        // 2. Space버튼 이용한 점프
        // 3. Platform이라는 지형 위에서 움직여야 합니다.

        public enum State
        {
            Idle = 0, // 대기 상태
            Left, // 왼쪽으로 가는 상태
            Right // 오른쪽으로 가는 상태
        }

        public GameObject[] SunGlasses;
        private State currentState = State.Idle;

        private void Update()
        {
            if(Keyboard.current.leftArrowKey.isPressed)
            {
                SetSunglassState(State.Left);
                Move(Vector3.left);
            }
            else if (Keyboard.current.rightArrowKey.isPressed)
            {
                SetSunglassState(State.Right);
                Move(Vector3.right);
            }
            else
            {
                SetSunglassState(State.Idle);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
        }

        private void SetSunglassState(State state)
        {
            if (currentState == state) return;
            // 상태가 전환 될때만 아래 로직이 실행되도록
            // 예외 처리를 합니다.

            SunGlasses[(int)currentState].SetActive(false);
            SunGlasses[(int)state].SetActive(true);
            currentState = state;
        }

        public float speed = 2.0f;

        private Rigidbody2D rBody;
        private Collider2D col;

        private void Awake()
        {
            rBody = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
        }
        private void Move(Vector3 dir)
        {
            //transform.Translate(dir * speed * Time.deltaTime);
            //transform.position += (dir * (speed * Time.deltaTime));
            //Vector3              Vector3 * (float: distance);

            // 이번 프레임에 움직일 벡터의 크기 : 이동량
            Vector3 moveVector = dir * (speed * Time.deltaTime);

            //내 위치와 이동량을 더해줍니다.
            rBody.MovePosition(transform.position + moveVector);
        }
        public float jumpPower = 3;

        private void Jump()
        {
            // AddForce를 사용하지 않고 점프를 구현해 보세요.
            // moveVector에 수평적 움직임과 점프의 수직적 움직임을 통합하면 됩니다.

            //점프 시간, 올라가는 높이를 정하고 해당 시간동안 높이까지 올린다.
            Vector3 jumpVector = Vector3.one * (jumpPower * Time.deltaTime);
            rBody.MovePosition(transform.position + jumpVector);
            //rBody.AddForceY(jumpPower, ForceMode2D.Impulse);
        }
    }
}

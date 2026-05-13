using Study_Mono;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.Input //클래스의 중복 방지를 위해 namespace를 사용합니다.
{
    public class Study_Input : MonoBehaviour
    {
        private void Update()
        {
            SpaceInputTest();
            ArrowInputTest();
        }

        private void SpaceInputTest()
        {
            // 키보드에 Space Input을 받아봅시다
            //bool isPressed = Keyboard.current.spaceKey.isPressed;
            //Debug.Log($"isPressed = {isPressed}");

            //wasPressedThisFrame
            //- 해당 키가 눌려있지 않은 상태에서 눌린 상태가 되었는지 체크합니다
            //- 키보드의 입력이 시작되었을때 True를 반환합니다.
            if (Keyboard.current.spaceKey.wasPressedThisFrame == true)
            {
                Debug.Log($"스페이스 입력이 시작되었습니다");
            }
            //isPressed
            //- 해당 키가 눌려있는지 체크합니다.
            //- 눌려있다면 True, 아니라면 False
            //키보드의 Space키가 눌려있다면
            else if (Keyboard.current.spaceKey.isPressed == true)
            {
                //Debug.Log($"스페이스 키가 눌려있습니다");
            }
            //wasReleasedThisFrame
            //- 해당 키가 눌려있는 상태에서 해제되었는지 체크합니다.
            //- 키보드입력이 종료되었을때 True를 반환합니다.
            else if (Keyboard.current.spaceKey.wasReleasedThisFrame == true)
            {
                Debug.Log($"스페이스 입력이 종료되었습니다");
            }
        }

        //유니티에서는 public 접근제한자를 이용해서 다른 객체에 대한
        //의존성을 Inspecctor에서 부여할 수 있습니다.
        //-의존성주입(DI)이 아님을 유의하십시오
        public GameObject Target;

        private void ArrowInputTest()
        {
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame == true)
            {
                Target.transform.position += new Vector3(-1, 0, 0);
            }
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame == true)
            {
                Target.transform.position += new Vector3(1, 0, 0);
            }
            if (Keyboard.current.upArrowKey.wasPressedThisFrame == true)
            {
                Target.transform.position += new Vector3(0, 1, 0);
            }
            if (Keyboard.current.downArrowKey.wasPressedThisFrame == true)
            {
                Target.transform.position += new Vector3(0, -1, 0);
            }
            //왼쪽 이동 코드 : Target.transform.position += new Vector3(-1, 0, 0);
            //오른쪽 이동 코드 : Target.transform.position += new Vector3(1, 0, 0);
            //위쪽 이동 코드 : Target.transform.position += new Vector3(0, 1, 0);
            //아래쪽 이동 코드 : Target.transform.position += new Vector3(0, -1, 0);
        }
    }
}
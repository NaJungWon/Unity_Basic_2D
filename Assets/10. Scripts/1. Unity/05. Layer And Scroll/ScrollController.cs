using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Study.LayerAndScroll
{
    // ScrollController라는 녀석을 통해서
    // Layer의 속도를 조절해 볼겁니다
    public class ScrollController : MonoBehaviour
    {
        public enum ScrollDirection { Left, Right, Up, Down}

        [Header("Scroll Settings")]
        public float speed = 1.0f; //이동 속도
        public ScrollDirection direction = ScrollDirection.Left;

        [Header("Resources")]
        public GameObject[] layerPrefabs;

        [Header("Ref Objects")]
        public GameObject startLayer;
        public Transform endPivot;
        public Transform spawnPivot;

        private List<GameObject> enableLayerList = new List<GameObject>();
        public bool isObstacle = false;

        private void Start()
        {
            if (isObstacle)
            {
                enableLayerList.Add(startLayer);
            }
            else
            {
                enableLayerList.Add(startLayer);
            }

        }

        private void Update()
        {
            MoveLayerList();
            CheckDestroyAbleLayer();
            CheckInstantiateLayer();
        }

        private Vector3 GetMoveDirection(ScrollDirection dir)
        {
            switch(dir)
            {
                case ScrollDirection.Left:
                    return Vector3.left;
                case ScrollDirection.Right:
                    return Vector3.right;
                case ScrollDirection.Up:
                    return Vector3.up;
                case ScrollDirection.Down:
                    return Vector3.down;
                default:
                    return Vector3.left;
            }
        }

        private void MoveLayerList()
        {
            // (speed * Time.deltaTime) = 초당 speed의 속도로 뭔가를 하겠다는 표현
            Vector3 dir = GetMoveDirection(direction);
            Vector3 moveVector = dir * (speed * Time.deltaTime);

            // 1. 활성화된 모든 레이어를 moveVector만큼 옮겨준다
            for (int i = 0; i < enableLayerList.Count; ++i)
            {
                enableLayerList[i].transform.Translate(moveVector);
            }
        }

        private void CheckDestroyAbleLayer()
        {
            // 2. 가장 첫번째 Layer(enableLayerList[0]가
            // EndPivot의 경계를 넘어간다면(x값보다 작아진다면)
            // 삭제한다

            GameObject headLayer = enableLayerList[0];
            // 가장 앞에있는 Layer오브젝트를 가져옵니다

            bool check = false;

            switch(direction)
            {
                case ScrollDirection.Left:
                    // headLayer의 x가 endPivot보다 작다면
                    check = headLayer.transform.position.x <= endPivot.position.x;
                    break;
                case ScrollDirection.Right:
                    check = headLayer.transform.position.x <= endPivot.position.x;
                    break;
                case ScrollDirection.Up:
                    check = headLayer.transform.position.y <= endPivot.position.y;
                    break;
                case ScrollDirection.Down:
                    check = headLayer.transform.position.y <= endPivot.position.y;
                    break;
            }


            if (check)
            {
                enableLayerList.RemoveAt(0);
                Destroy(headLayer);
            }
        }

        private void CheckInstantiateLayer()
        {
            while (enableLayerList.Count < 3)
            {
                if(isObstacle)
                {
                    MakeRandomObstacle();
                }
                else
                {
                    GameObject instance = Instantiate(layerPrefabs[0], // layerPrefabs[0]개체의 사본을 전달합니다.
                    spawnPivot.position, spawnPivot.rotation);
                    // spawnPivot의 위치, spawnPivot의 회전값이라는 말.
                    enableLayerList.Add(instance);
                }
            }
        }

        public void MakeRandomObstacle()
        {
            int randomNum = Random.Range(0, layerPrefabs.Length); //랜덤 객체
            int randomYPos = Random.Range(-5, 5);                 //랜덤 좌표 생성
            float randomScale = Random.Range((float)-0.5, (float)1.0); //랜덤 크기

            Vector3 spawnPoint = spawnPivot.transform.position;
            spawnPoint.y += randomYPos;

            GameObject instance = Instantiate(layerPrefabs[randomNum], spawnPoint, spawnPivot.rotation);
            Vector3 ObstacleScale = instance.transform.localScale;
            {
                ObstacleScale.x += randomScale;
                ObstacleScale.y += randomScale;
                ObstacleScale.z += randomScale;
            }
            instance.transform.localScale = ObstacleScale;
            enableLayerList.Add(instance);
        }
    }
}

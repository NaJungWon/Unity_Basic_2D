using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Study.LayerAndScroll
{
    // ScrollController라는 녀석을 통해서
    // Layer의 속도를 조절해 볼겁니다.
    public class ScrollController : MonoBehaviour
    {
        [Header("Scroll Settings")]
        public float speed = 1.0f;

        [Header("Resources")]
        public GameObject[] layerPrefabs;

        [Header("Ref Object")]
        public GameObject startLayer;
        public Transform endPivot;
        public Transform spawnPivot;

        public List<GameObject> enableLayerList = new List<GameObject>();

        private void Update()
        {
            // (speed * Time.deltaTime) = 초당 speed 만큼 이동시키겠다는 표현
            Vector3 moveVector = Vector3.left * (speed * Time.deltaTime);

            for(int i = 0; i < enableLayerList.Count; i++)
            {
                enableLayerList[i].transform.Translate(moveVector);
            }
        }
    }
}
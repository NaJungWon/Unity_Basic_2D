using System;
using UnityEngine;
using DG.Tweening;
using Study.Utilities;
using UnityEngine.InputSystem;

public class note : MonoBehaviour
{
    public Ease Ease;
    public float Factor = 1.0f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SimpleInput.GetKeyDown(Key.RightArrow))
        {
            transform
                .DOMove(transform.position + Vector3.right*Factor, 1.0f)
                .SetEase(Ease);
        }
        if (SimpleInput.GetKeyDown(Key.LeftArrow))
        {
            transform
                .DOMove(transform.position + Vector3.left*Factor, 1.0f)
                .SetEase(Ease);
        }
        
        if (SimpleInput.GetKeyDown(Key.Digit1))
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMove(transform.position + Vector3.up*Factor, 1.0f)); //이동
            seq.Join(spriteRenderer.DOFade(0f,1f)); //동시에 페이드
            seq.AppendInterval(0.5f); //0.5f간 대기
            seq.AppendCallback(()=> Debug.Log("연출 끝"));

        }

        if (SimpleInput.GetKeyDown(Key.Digit2))
        {
            transform.DOMoveY(2.0f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .OnComplete(() => Debug.Log("왕복 완료"));
        }
    }
}
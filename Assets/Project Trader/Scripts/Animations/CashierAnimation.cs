﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierAnimation : AnimationBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDealAnimation(Action callback)
    {
        Play(DealAnimationCoroutine(callback));
    }

    private IEnumerator DealAnimationCoroutine(Action callback)
    {
        // TODO:애니메이션 코드
        yield return null;
        // 완료시 정해진 작업 실행
        callback();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Test_RewardBtnClick : MonoBehaviour
{
    [SerializeField]
    private int rewardIndex;
    [SerializeField]
    private DungeonRewardHandler rewardHandler;

    public void OnClick()
    {
        rewardHandler.SelectReward(rewardIndex);
        Debug.Log($"[��ư]���� ���õ�: {rewardIndex}");
    }


}

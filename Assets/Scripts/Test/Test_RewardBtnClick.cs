using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_RewardBtnClick : MonoBehaviour
{
    [SerializeField] private int rewardIndex;

    public void OnClick()
    {
        DungeonManager.Instance.SelectReward(rewardIndex);
        Debug.Log($"[��ư]���� ���õ�: {rewardIndex}");
    }
}

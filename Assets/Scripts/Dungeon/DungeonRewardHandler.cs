using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonRewardHandler : Singleton<DungeonRewardHandler>
{

    
    [SerializeField] 
    private List<RewardData> allRewards;

    [SerializeField] 
    private DungeonUI dungeonUI;

    [SerializeField]
    private RewardData[] currentRewards;

    private int selectedRewardIndex = -1;
    public bool RewardSelected => selectedRewardIndex != -1;

    public void ShowRewardOptions()
    {
        selectedRewardIndex = -1;
        currentRewards = allRewards.OrderBy(x => Random.value).Take(3).ToArray();
        dungeonUI.ShowRewards(currentRewards);
    }


    public void SelectReward(int index)
    {
        // Tag 값으로 구분하는지 Layer값으로 구분하는지 헷갈리네요
        var player = GameObject.FindWithTag("Player").GetComponent<StatController>();
        if (player == null)
            return;
        // ########## 이 위치에다가 스탯 컨트롤러의 스탯변경 로직 호출 ##########


        var weapon = GameObject.FindWithTag("Player").GetComponentInChildren<RangeWeapon>();
        weapon.RewardData = currentRewards[index];
        weapon.SettingStat();

        selectedRewardIndex = index;
        dungeonUI.HidePanel();
        Debug.Log($"[보상 선택됨] {currentRewards[index].title}");
    }

    public int GetSelectedReward()
    {
        return selectedRewardIndex;
    }
}

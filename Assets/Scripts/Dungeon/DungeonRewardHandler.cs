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
        // Tag ������ �����ϴ��� Layer������ �����ϴ��� �򰥸��׿�
        var player = GameObject.FindWithTag("Player").GetComponent<StatController>();
        if (player == null)
            return;
        // ########## �� ��ġ���ٰ� ���� ��Ʈ�ѷ��� ���Ⱥ��� ���� ȣ�� ##########


        var weapon = GameObject.FindWithTag("Player").GetComponentInChildren<RangeWeapon>();
        weapon.RewardData = currentRewards[index];
        weapon.SettingStat();

        selectedRewardIndex = index;
        dungeonUI.HidePanel();
        Debug.Log($"[���� ���õ�] {currentRewards[index].title}");
    }

    public int GetSelectedReward()
    {
        return selectedRewardIndex;
    }
}

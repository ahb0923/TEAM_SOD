using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DungeonUI : BaseInterAction
{
    [SerializeField] 
    private Transform[] rewardPanels;
    private DungeonRewardHandler rewardHandler;

    private RangeWeapon weapon; //플레이어 정보 
    private GameObject Player;

    //Awake로 해도...?(해야?)
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        weapon = Player.GetComponentInChildren<RangeWeapon>();

        rewardHandler = FindObjectOfType<DungeonRewardHandler>();
        transform.gameObject.SetActive(false);
    }
    public void ShowRewards(RewardData[] rewards)
    {
        transform.gameObject.SetActive(true);

        for (int i = 0; i < rewardPanels.Length; i++)
        {
            var reward = rewards[i];
            var panel = rewardPanels[i];
            Debug.Log($" title : {reward.title} / image : {reward.image} / text : {reward.text}");

            panel.Find("Title").GetComponent<TextMeshProUGUI>().text = reward.title;
            panel.Find("Icon_Bg").GetComponent<Image>().sprite = reward.image;
            panel.Find("Text").GetComponent<TextMeshProUGUI>().text = reward.text;

            var btn = panel.Find("Button").GetComponent<Button>();
            int index = i;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {
                rewardHandler.SelectReward(index);
                transform.gameObject.SetActive(false);
            });
        }
    }
    public void HidePanel()
    {
        transform.gameObject.SetActive(false);
    }
}

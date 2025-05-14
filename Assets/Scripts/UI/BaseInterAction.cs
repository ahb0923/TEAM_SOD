using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

[Serializable]
public struct SpriteEntry
{
    public string key;    // “Dungeon”, “Shop” 같은 이름
    public Sprite sprite; // 드래그&드롭할 스프라이트
}
[Serializable]
public struct TextEntry
{
    public string key;    // “MessageText” 같은 이름
    public TextMeshProUGUI textPro; // 드래그&드롭할 텍스트
}


public class BaseInterAction : MonoBehaviour
{
    
 
    
    protected bool _playerInRange;
    

    
    [SerializeField] protected GameObject selectPanel;
    [SerializeField] protected List<SpriteEntry> spriteEntries;
    [SerializeField] protected List<TextEntry> textEntries;


    protected Dictionary<string, Sprite> sprites;
    protected Dictionary<string, TextMeshProUGUI> texts;

    



    //플레이어불러오기



    protected virtual void Awake()
    {
        // 1) 리스트 → 딕셔너리 변환
        sprites = spriteEntries
            .Where(e => !string.IsNullOrEmpty(e.key) && e.sprite != null)
            .ToDictionary(e => e.key, e => e.sprite);
       
        
        texts = textEntries
               .Where(e => !string.IsNullOrEmpty(e.key) && e.textPro != null)
               .ToDictionary(e => e.key, e => e.textPro);


        Canvas canvas = FindObjectOfType<Canvas>();
    }




    protected void Update()
    {
        // 트리거 안에 있는 상태에서 E 키 누르면
        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            

            OpenPanel();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }


    public virtual void OpenPanel() 
    { 
    
    
    }

}

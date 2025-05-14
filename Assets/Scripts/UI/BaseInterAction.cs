using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

[Serializable]
public struct SpriteEntry
{
    public string key;    // ��Dungeon��, ��Shop�� ���� �̸�
    public Sprite sprite; // �巡��&����� ��������Ʈ
}
[Serializable]
public struct TextEntry
{
    public string key;    // ��MessageText�� ���� �̸�
    public TextMeshProUGUI textPro; // �巡��&����� �ؽ�Ʈ
}


public class BaseInterAction : MonoBehaviour
{
    
 
    
    protected bool _playerInRange;
    

    
    [SerializeField] protected GameObject selectPanel;
    [SerializeField] protected List<SpriteEntry> spriteEntries;
    [SerializeField] protected List<TextEntry> textEntries;


    protected Dictionary<string, Sprite> sprites;
    protected Dictionary<string, TextMeshProUGUI> texts;

    



    //�÷��̾�ҷ�����



    protected virtual void Awake()
    {
        // 1) ����Ʈ �� ��ųʸ� ��ȯ
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
        // Ʈ���� �ȿ� �ִ� ���¿��� E Ű ������
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

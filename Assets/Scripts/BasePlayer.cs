using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    // 충돌판정을 위한 rigidbody
    protected Rigidbody2D player_rigidbody;
    
    // 플레이어, 무기 스프라이트 출력방향을 위한 필드 선언
    [SerializeField] protected SpriteRenderer playerSprite;
    // [SerializeField] protected Weapon currentWeapon; 
    // 무기 스프라이트는 Weapon 클래스에서 받아오고 weaponPivot 오브젝트를 회전할 때 사용
  
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }
    // 캡슐화. 추후 Weapon 클래스에서 발사체 방향값 받아갈 때 사용
    protected Vector2 movementDirection = Vector2.zero;
    // public Vector2 MovemetDirection { get { return movementDirection; } }
    // 혹시 몬스터의 장판 패턴같은 게 유저의 이동방향을 고려한다면 필요할지도?
    // 넉벡 관련해서는 플레이어는 넉벡되지 않기로 했으니 weapon 클래스에서 만들어도?

    //protected AnimationController animationHander;
    protected StatController player_Stat;
    // 스탯변화 및 애니매이션을 넣어야 할 때 불러올 외부 인스턴스

    protected enum PLAYER_STATE
    {
    WAIT,
    IDLE,
    MOVE,
    DAMAGE,
    DIE
    }

    protected void Awake()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();
    }

    


}

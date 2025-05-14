using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct DamageResult
{
    public float final_Damage;
    public bool is_Critical;

    public DamageResult(float Final_Damage, bool Is_Critical)
    {
        final_Damage = Final_Damage;
        is_Critical = Is_Critical;
    }
}
public class StatController : MonoBehaviour
{
    public float Hp { get; private set; }
    public float MaxHp { get; private set; }
    public float Atk { get; private set; }
    public float Def { get; private set; }
    public float MoveSpeed { get; private set; }
    public int Gold { get; private set; }
    // 유저의 경우 소지한 골드, 몬스터의 경우 죽으면 떨굴 골드라고 생각하면 될 듯!
    public float Crit_Chance { get; private set; }
    public float Crit_Multiply { get; private set; }
    // 크리티컬을 위한 수치. chance는 0~1 사이, multiply는 크리 터질 시 딜 배수
    public float Invinsible_Duration { get; private set; }
    public bool Is_Invinsible { get; private set; }
    // 추후 무적시간을 위한 값. 몬스터는 0 넣고 유저만 duration 조금 넣으면 될 듯?

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private MonsterData monsterData;
    
    //test용 임시
    [SerializeField]
    public string testkey;


    public void InitStat_Player()
    {
        Hp = playerData.maxHp;
        MaxHp = playerData.maxHp;
        Atk = playerData.atk;
        Def = playerData.def;
        MoveSpeed = playerData.moveSpeed;
        Gold = playerData.gold;
        Crit_Chance = playerData._crit_Chance;
        Crit_Multiply = playerData._crit_Multiply;
        Invinsible_Duration = playerData._in_invinsible_duration;
        Is_Invinsible = playerData._is_invinsible;
    }

    public void InitStat_Monster()
    {
        Hp = monsterData.maxHp;
        MaxHp = monsterData.maxHp;
        Atk = monsterData.atk;
        Def = monsterData.def;
        MoveSpeed = monsterData.moveSpeed;
        //Gold = monsterData.gold;
        Crit_Chance = monsterData._crit_Chance;
        Crit_Multiply = monsterData._crit_Multiply;
        Invinsible_Duration = monsterData._in_invinsible_duration;
        Is_Invinsible = monsterData._is_invinsible;
    }
    // 초기화를 위한 InitStat. 몬스터나 유저 쪽에서 사용할 경우 awake 때 불러오자. 추후 ScriptableObjcet를 사용하게 되면 수정해야 할 수 있음.


    // 데미지 계산 결과와 크리티컬 여부를 동시에 반환받기 위해서 구조체로 데이터타입 선언
    // 데미지 몇 떴는지 영수증을 띄우고, 이 때 크리티컬의 경우 더 화려한 이펙트를 주고 싶다면
    // 최종 데미지와, 크리티컬 여부가 동시에 필요해서 이렇게 번거롭게...
    // 한 프레임에 여러 투사체에 맞을 경우에 필드에서 bool is_Crit, finalDmg 이렇게 하면 문제가 생기지 않나?해서.

    public DamageResult FinalDamageCalculator(float finalAtkFromWeapon, float finalCritCFromWeapon, float finalCritMFromWeapon)
    {
        float random = Random.value; // 0이상 1이하 float값 랸듐
        bool _is_Crit = (random <= finalCritCFromWeapon) ? true : false; // 크리 여부 판정
        float final_dmg = finalAtkFromWeapon - Def;
        final_dmg = (final_dmg > 0) ? final_dmg : 0; // 음수처리 방지
        if (_is_Crit) final_dmg *= finalCritMFromWeapon;
        // 크리여부에 따른 최종뎀 계산식. 방어력을 지금은 단순 뺄셈해뒀는데, %로 적용할 거면 여기서 변경
        return new DamageResult(final_dmg, _is_Crit);
    }

    public void HpReductionApply(DamageResult result) // 몹 쪽이나 유저 쪽 양쪽 모두에서 사용하려면 딜넣는 쪽의 데미지를 매개변수로 받는 게 나을 듯?
    {
        float final_Dmg_Applied = result.final_Damage;
        final_Dmg_Applied = (final_Dmg_Applied > 0) ? final_Dmg_Applied : 0; // 최종뎀 음수 방지
        Hp -= final_Dmg_Applied;
        Hp = Hp > 0 ? Hp : 0;
        // 사망판정 시에 음수 hp도 사용이 가능하긴 하지만, hp바를 몹이나 플레이어 위에 띄울 경우를 생각해서 바로 0으로 보정
    }
    public void HpHealApply(float heal)
    {
        Hp = Mathf.Max(Hp + heal, MaxHp); // 최대 체력 초과 방지. 로비로 돌아올 때 힐해줄 경우에도 활용 가능
    }

    public void MaxHpChangeApply(float maxhp_Change_Amount)
    {
        MaxHp += maxhp_Change_Amount;
        if (maxhp_Change_Amount > 0) Hp = Mathf.Max(Hp + maxhp_Change_Amount, MaxHp);
        // 최대체력이 늘어난 경우 늘어난 만큼 힐까지 해 주기
        else if (maxhp_Change_Amount < 0) Hp = Mathf.Max(Hp, MaxHp);
        // 최대체력이 줄어든 경우 그만큼 피를 깎지는 말되 최대체력보다 체력이 많아지지 않도록 조정
    }

    // 체력을 체외하고 나머진 단순연산 변화만 있을 것 같아서 각각 메서드를 추가해서 필요할 때마다 호출
    public void GoldChangeApply(int gold_Change_Amount)
    {
        Gold += gold_Change_Amount;
    }
    public void AttackChangeApply(float attack_Change_Amount)
    {
        Atk += attack_Change_Amount;
    }
    public void DefChangeApply(float def_Change_Amount)
    {
        Def += def_Change_Amount;
    }
    public void MoveSpeedChangeApply(float movespeed_Change_Amount)
    {
        MoveSpeed += movespeed_Change_Amount;
    }
    public void Crit_ChanceChangeApply(float crit_Chance_Change_Amount)
    {
        Crit_Chance += crit_Chance_Change_Amount;
    }
    public void Crit_MultiplyChangeApply(float crit_Multiply_Change_Amount)
    {
        Crit_Multiply += crit_Multiply_Change_Amount;
    }
    public void Invincible_DurationChangeApply(float invincible_Duration_Change_Amount)
    {
        Invinsible_Duration += invincible_Duration_Change_Amount;
    }
    public void Is_Invincible_ChangeApply(bool is_Invincible_TF)
    {
        Is_Invinsible = is_Invincible_TF;
    }

}

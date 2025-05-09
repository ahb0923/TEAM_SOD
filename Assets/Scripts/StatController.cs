using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public float hp { get; private set; }
    public float maxHp { get; private set; }
    public float atk { get; private set; }
    public float def { get; private set; }
    public float moveSpeed { get; private set; }
    public int gold { get; private set; }
    // 유저의 경우 소지한 골드, 몬스터의 경우 죽으면 떨굴 골드라고 생각하면 될 듯!
    public float crit_Chance { get; private set; }
    public float crit_Multiply { get; private set; }
    // 크리티컬을 위한 수치. chance는 0~1 사이, multiply는 크리 터질 시 딜 배수
    public bool is_invinsible {  get; private set; }
    public float invinsible_duration {  get; private set; }
    // 추후 무적시간을 위한 값. 몬스터는 false, 0 넣고 유저만 duration 조금 넣으면 될 듯?
    
    public StatController(float _hp, float _maxHp, float _atk, float _def, float _moveSpeed, int _gold, float _crit_Chance, float _crit_Multiply, bool _is_invinsible, float _invinsible_duration)
    {
        hp = _hp;
        maxHp = _maxHp;
        atk = _atk;
        def = _def;
        moveSpeed = _moveSpeed;
        gold = _gold;
        crit_Chance = _crit_Chance;
        crit_Multiply = _crit_Multiply;
        is_invinsible = _is_invinsible;
        invinsible_duration = _invinsible_duration;
    }
    // 생성자 정의! 몬스터나 유저 쪽에서 사용할 경우 awake 때 불러오자.

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
    // 데미지 계산 결과와 크리티컬 여부를 동시에 반환받기 위해서 구조체로 데이터타입 선언
    // 데미지 몇 떴는지 영수증을 띄우고, 이 때 크리티컬의 경우 더 화려한 이펙트를 주고 싶다면
    // 최종 데미지와, 크리티컬 여부가 동시에 필요해서 이렇게 번거롭게...
    // 한 프레임에 여러 투사체에 맞을 경우에 필드에서 bool is_Crit, finalDmg 이렇게 하면 문제가 생기지 않나?해서.

    public DamageResult FinalDamageCalculator(float rawDamage)
    {
        float random = Random.value; // 0이상 1이하 float값 랸듐
        bool _is_Crit = (random <= crit_Chance) ? true : false; // 크리 여부 판정

        float final_dmg = rawDamage - def;
        if (_is_Crit) final_dmg *= crit_Multiply;
        // 크리여부에 따른 최종뎀 계산식. 방어력을 지금은 단순 뺄셈해뒀는데, %로 적용할 거면 여기서 변경
        return new DamageResult(final_dmg, _is_Crit);
    }

    
    public void Damaged(DamageResult result) // 몹 쪽이나 유저 쪽 양쪽 모두에서 사용하려면 딜넣는 쪽의 데미지를 매개변수로 받는 게 나을 듯?
    {
        float final_Dmg_Applied = result.final_Damage - def; // 방어도 계산식을 단순 뺄셈으로 할지 롤처럼 %로 할지 논의 필요
        final_Dmg_Applied = (final_Dmg_Applied > 0) ? final_Dmg_Applied : 0; // 최종뎀 음수 방지
        hp -= final_Dmg_Applied;
        hp = hp > 0 ? hp : 0;
        // 사망판정 시에 음수 hp도 사용이 가능하긴 하지만, hp바를 몹이나 플레이어 위에 띄울 경우를 생각해서 바로 0으로 보정
    }
    public void Healed(float heal)
    {
        hp = Mathf.Max(hp+heal, maxHp); // 최대 체력 초과 방지. 로비로 돌아올 때 힐해줄 경우에도 활용 가능
    }


}

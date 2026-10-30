using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public int defense;
    public int maxMP;
    public int currentMP;

    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int dmg)
    {
        currentHP = DamageReduction(dmg);
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public int DamageReduction(int damageAmount)
    {
        return (damageAmount * (255 - defense) / 256) + 1;
    }

    public void Restore(int amount)
    {
        currentMP += amount;
        if (currentMP > maxMP)
        {
            currentMP = maxMP;
        }
    }

    public bool TakeMagicDamage(int mp)
    {
        currentHP -= mp/5;
        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool UpdateMP(int mp)
    {
        currentMP -= mp;
        if(currentMP < 0)
        {
            currentMP = 0;
            return false;
        }
        else
        {
            return true;
        }
    }
}
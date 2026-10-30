using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider healthSlider;
    public Slider magicPointsSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        healthSlider.maxValue = unit.maxHP;
        healthSlider.value = unit.currentHP;
        magicPointsSlider.maxValue = unit.maxMP;
        magicPointsSlider.value = unit.currentMP;
    }

    public void SetHP(int hp)
    {
        healthSlider.value = hp;
    }

    public void SetMP(int mp)
    {
        magicPointsSlider.value = mp;
    }
}

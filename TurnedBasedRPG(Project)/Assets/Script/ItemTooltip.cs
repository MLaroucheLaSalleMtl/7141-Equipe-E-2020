using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();
    public void ShowTooltip(EquippableItem item)
    {
        ItemNameText.text = item.ItemName;

        ItemSlotText.text = item.EquipementType.ToString();

        sb.Length = 0;
        AddStat(item.StrengthBonus, "Strength");
        AddStat(item.StaminaBonus, "Stamina");
        AddStat(item.SpeedBonus, "Speed");
        AddStat(item.resistenceBonus, "resistence");

        AddStat(item.StrengthPercentBonus, "Stregth", isPercent: true);
        AddStat(item.StaminaPercentBonus, "Stamina", isPercent: true);
        AddStat(item.SpeedPercentBonus, "Speed", isPercent: true);
        AddStat(item.resistencePercentBonus, "resistence", isPercent: true);

        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statname, bool isPercent = false)
    {

        if(value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {

                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statname);
        }
        
    }


}

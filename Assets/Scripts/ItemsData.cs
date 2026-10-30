using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "item")]
public class ItemsData : ScriptableObject
{
    public string itemName;
    public int itemCount;
    public int restoreHP;
    public int restoreMP;
    public int damage;

    public void ResetItemCount()
    {
        itemCount = 0;
    }

    public void AddItem(int count)
    {
        itemCount += count;
    }

    public void UseItem()
    {
        if (itemCount > 0)
        {
            itemCount--;
        }
        else
        {
            Debug.Log("No items left!");
        }
    }
}

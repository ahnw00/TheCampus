using UnityEngine;
using System.Collections.Generic;

public class CraftResultSlotManager : MonoBehaviour
{
    public CraftSlot craftSlot1; // 첫 번째 CraftSlot
    public CraftSlot craftSlot2; // 두 번째 CraftSlot
    public Dictionary<List<Item>, Item> recipes; // 조합식

    private Item resultItem; // 현재 생성된 결과 아이템

    // Update is called once per frame
    void Update()
    {
        CheckRecipe(); // 조합식 체크 및 생성
    }

    void CheckRecipe()
    {
        Item item1 = craftSlot1.GetItem();
        Item item2 = craftSlot2.GetItem();

        if (item1 == null || item2 == null)
        {
            RemoveResultItem(); // CraftSlot에서 아이템 제거시 결과 아이템 삭제
            return;
        }

        List<Item> currentItems = new List<Item> { item1, item2 };
        foreach (var recipe in recipes)
        {
            if (AreItemsEqual(recipe.Key, currentItems))
            {
                SetResultItem(recipe.Value);
                return;
            }
        }

        RemoveResultItem();
    }

    bool AreItemsEqual(List<Item> recipeItems, List<Item> currentItems)
    {
        return new HashSet<Item>(recipeItems).SetEquals(currentItems);
    }
    void SetResultItem(Item newItem)
    {
        if (resultItem == newItem) return;
        resultItem = newItem;
        GetComponent<Icon>().SetItem(resultItem);
    }
    void RemoveResultItem()
    {
        if (resultItem == null) return;
        resultItem = null;
        GetComponent<Icon>().SetItem(null);
    }
    public void OnResultItemDraggedToItemSlot()
    {
        craftSlot1.RemoveItem();
        craftSlot2.RemoveItem();
        RemoveResultItem();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public UIItemSlot[] slots;
    public RectTransform highlight;
    public Player player;
    public int slotIndex = 0;

    private void Start()
    {
        byte index = 1;
        foreach (UIItemSlot s in slots)
        {
            ItemStack stack = new ItemStack(index, Random.Range(2, 65));
            ItemSlot slot = new ItemSlot(s, stack);
            index++;
        }
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            if (scroll > 0)
                slotIndex--;
            else
                slotIndex++;

            if (slotIndex > slots.Length - 1)
                slotIndex = 0;
            if (slotIndex < 0)
                slotIndex = slots.Length - 1;

            highlight.position = slots[slotIndex].slotIcon.transform.position;
        }

        // Check for number key presses 1-9
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                // Check if there's a corresponding slot for the pressed number
                if (i < slots.Length)
                {
                    slotIndex = i; // Update slot index
                    highlight.position = slots[slotIndex].slotIcon.transform.position; // Update highlight position
                    break; // Exit the loop once the key press is handled
                }
            }
        }
    }
}

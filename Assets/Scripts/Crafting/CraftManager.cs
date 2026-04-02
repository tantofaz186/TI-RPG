using System.Linq;
using Controllers;

namespace Crafting
{
    public class CraftManager : Singleton<CraftManager>
    {
        public SlotInv[] inventorySlots = new SlotInv[3];
        private ItemInv resultItem;

        public SlotInv addItem(ItemInv item)
        {
            return inventorySlots.FirstOrDefault(slot => slot.addItem(item));
        }

        public SlotInv removeItem(int slotIndex)
        {
            return inventorySlots[slotIndex].removeItem()? inventorySlots[slotIndex] : null;
        }
        public SlotInv Craft()
        {

            return inventorySlots[0];
        }

    }
}
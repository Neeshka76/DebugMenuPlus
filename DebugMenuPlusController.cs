using UnityEngine;

namespace DebugMenuPlus
{
    public class DebugMenuPlusData
    {
        // Height of player
        public float ChangeHeightGetSet { get; set; }
        // Set clean Bodies
        public bool CleanBodiesGetSet { get; set; }
        // Set Bodies in level
        public bool BodiesInLevelGetSet { get; set; }
        // Set clean Items
        public bool CleanItemsGetSet { get; set; }
        // Set Items in level
        public bool ItemsInLevelGetSet { get; set; }
        // Set Test Info Object
        public bool TestInfoObjectGetSet { get; set; }
        // Set if Player has pressed the button NbBodiesLimitValue
        public bool NbBodiesLimitValueButtonPressedGetSet { get; set; }
        // Set if Player has pressed the button NbItemsLimitValue
        public bool NbItemsLimitValueButtonPressedGetSet { get; set; }
        // Set if Keyboard has pressed enter button finish
        public bool KeyboardFinishEnterButtonPressedGetSet { get; set; }
        // Set if the value to assign is a int
        public bool ValueToAssignIsInt { get; set; }
        // Set if the value to assign is a float
        public bool ValueToAssignIsFloat { get; set; }
        // Set if the value to assign is uint
        public bool ValueToAssignIsUint { get; set; }
        // Number of Bodies in level
        public int NbBodiesInLevelGetSet { get; set; }
        // Number of Items in level
        public int NbItemsInLevelGetSet { get; set; }
        // Limit of number of Bodies in level
        public uint NbBodiesLimitValueInLevelGetSet { get; set; }
        // Limit of number of Items in level
        public uint NbItemsLimitValueInLevelGetSet { get; set; }
        // Value to pass from the Keyboard in Uint
        public uint ValueToAssignedUintGetSet { get; set; }
        // Value to pass from the Keyboard in Int
        public int ValueToAssignedIntGetSet { get; set; }
        // Value to pass from the Keyboard in float
        public float ValueToAssignedFloatGetSet { get; set; }
        // Set if value Kick Enabled
        public bool KickEnabledGetSet { get; set; }
        // Set if value Jump Enabled
        public bool JumpEnabledGetSet { get; set; }
        // Set if Player has pressed the button KickWidthAreaValue
        public bool KickWidthAreaValueButtonPressedGetSet { get; set; }
        // Set if Player has pressed the button KickLengthValue
        public bool KickLengthValueButtonPressedGetSet { get; set; }
        // Value of the Kick Width Area
        public float KickWidthAreaValueGetSet { get; set; }
        // Value of the Kick Length
        public float KickLengthValueGetSet { get; set; }
        // Set remove Imbuements
        public bool RemoveImbuementsGetSet { get; set; }
        // Set Imbuements in level
        public bool ImbuementInLevelGetSet { get; set; }
        // Number of Imbuements in level
        public int NbImbuementsInLevelGetSet { get; set; }

    }

    public class DebugMenuPlusController : MonoBehaviour
    {
        public DebugMenuPlusData data = new DebugMenuPlusData();
    }
}

using ThunderRoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DebugMenuPlus
{
    public class DebugMenuPlusMenuModule : MenuModule
    {
        public DebugMenuPlusController debugMenuPlusController;
        public DebugMenuPlusHook debugMenuPlusHook;
        private Text txtNbBodies;
        private Button btnCleanBodies;
        private Button btnLimitValueBodies;
        private Text txtNbItems;
        private Button btnCleanItems;
        private Button btnLimitValueItems;
        private Button btnTestInfoObject;
        private Button btnToggleKick;
        private Button btnToggleJump;
        private Button btnKickWidthAreaValue;
        private Button btnKickLengthValue;
        public override void Init(MenuData menuData, Menu menu)
        {
            base.Init(menuData, menu);
            // Grab the value from Unity
            btnCleanBodies = menu.GetCustomReference("btn_CleanBodies").GetComponent<Button>();
            btnCleanItems = menu.GetCustomReference("btn_CleanItems").GetComponent<Button>();
            txtNbBodies = menu.GetCustomReference("txt_NbBodies").GetComponent<Text>();
            txtNbItems = menu.GetCustomReference("txt_NbItems").GetComponent<Text>();
            btnTestInfoObject = menu.GetCustomReference("btn_TestInfoObject").GetComponent<Button>();
            btnLimitValueBodies = menu.GetCustomReference("btn_LimitValueBodies").GetComponent<Button>();
            btnLimitValueItems = menu.GetCustomReference("btn_LimitValueItems").GetComponent<Button>();
            btnToggleKick = menu.GetCustomReference("btn_ToggleKick").GetComponent<Button>();
            btnToggleJump = menu.GetCustomReference("btn_ToggleJump").GetComponent<Button>();
            btnKickWidthAreaValue = menu.GetCustomReference("btn_KickWidthAreaValue").GetComponent<Button>();
            btnKickLengthValue = menu.GetCustomReference("btn_KickLengthValue").GetComponent<Button>();

            // Add an event listener for buttons
            btnCleanBodies.onClick.AddListener(ClickCleanBodies);
            btnCleanItems.onClick.AddListener(ClickCleanItems);
            btnTestInfoObject.onClick.AddListener(ClickTestInfoObject);
            btnLimitValueBodies.onClick.AddListener(ClickLimitValueBodies);
            btnLimitValueItems.onClick.AddListener(ClickLimitValueItems);
            btnToggleKick.onClick.AddListener(ClickToggleKick);
            btnToggleJump.onClick.AddListener(ClickToggleJump);
            btnKickWidthAreaValue.onClick.AddListener(ClickKickWidthAreaValue);
            btnKickLengthValue.onClick.AddListener(ClickKickLengthValue);
            // Initialization of datas
            debugMenuPlusController = GameManager.local.gameObject.AddComponent<DebugMenuPlusController>();
            debugMenuPlusController.data.NbBodiesInLevelGetSet = -1;
            debugMenuPlusController.data.NbItemsInLevelGetSet = -1;
            debugMenuPlusController.data.NbBodiesLimitValueInLevelGetSet = 30;
            debugMenuPlusController.data.NbItemsLimitValueInLevelGetSet = 100;
            debugMenuPlusController.data.KickEnabledGetSet = true;
            debugMenuPlusController.data.JumpEnabledGetSet = true;
            debugMenuPlusController.data.KickWidthAreaValueGetSet = 0.2f;
            debugMenuPlusController.data.KickLengthValueGetSet = 2f;


            debugMenuPlusHook = menu.gameObject.AddComponent<DebugMenuPlusHook>();
            debugMenuPlusHook.menu = this;
            // Update all the Data for left page (text, visibility of buttons etc...)
            UpdateDataPageLeft1();
            // Update all the Data for right page (text, visibility of buttons etc...)
            UpdateDataPageRight1();
        }

        // When selector is click change display to Error or Clean if no bodies
        public void ClickCleanBodies()
        {
            debugMenuPlusController.data.CleanBodiesGetSet = true;
            btnCleanBodies.GetComponentInChildren<Text>().text = debugMenuPlusController.data.BodiesInLevelGetSet ? "Clean" : "Error";
            UpdateDataPageLeft1();
        }
        // When selector is click change display to Error or Clean if no items
        public void ClickCleanItems()
        {
            debugMenuPlusController.data.CleanItemsGetSet = true;
            btnCleanItems.GetComponentInChildren<Text>().text = debugMenuPlusController.data.ItemsInLevelGetSet ? "Clean" : "Error";
            UpdateDataPageLeft1();
        }
        // When selector is click display infos of items
        public void ClickTestInfoObject()
        {
            debugMenuPlusController.data.TestInfoObjectGetSet = true;
            btnCleanItems.GetComponentInChildren<Text>().text = debugMenuPlusController.data.ItemsInLevelGetSet ? "Processing" : "Error";
            UpdateDataPageRight1();
        }
        // When selector is click display keyboard to enter the value to limit bodies
        public void ClickLimitValueBodies()
        {
            debugMenuPlusController.data.NbBodiesLimitValueButtonPressedGetSet = true;
            debugMenuPlusController.data.ValueToAssignIsUint = true;
            UpdateDataPageRight1();
        }
        // When selector is click display keyboard to enter the value to limit items
        public void ClickLimitValueItems()
        {
            debugMenuPlusController.data.NbItemsLimitValueButtonPressedGetSet = true;
            debugMenuPlusController.data.ValueToAssignIsUint = true;
            UpdateDataPageRight1();
        }
        // When button is click disable/enable Kicks
        public void ClickToggleKick()
        {
            debugMenuPlusController.data.KickEnabledGetSet ^= true;
            btnToggleKick.GetComponentInChildren<Text>().text = debugMenuPlusController.data.KickEnabledGetSet ? "Enabled" : "Disabled";
        }
        // When button is click disable/enable Jumps
        public void ClickToggleJump()
        {
            debugMenuPlusController.data.JumpEnabledGetSet ^= true;
            btnToggleJump.GetComponentInChildren<Text>().text = debugMenuPlusController.data.JumpEnabledGetSet ? "Enabled" : "Disabled";
        }
        // When selector is click display keyboard to enter the value to Kick width area
        public void ClickKickWidthAreaValue()
        {
            debugMenuPlusController.data.KickWidthAreaValueButtonPressedGetSet = true;
            debugMenuPlusController.data.ValueToAssignIsFloat = true;
            UpdateDataPageRight1();
        }
        // When selector is click display keyboard to enter the value to Kick length
        public void ClickKickLengthValue()
        {
            debugMenuPlusController.data.KickLengthValueButtonPressedGetSet = true;
            debugMenuPlusController.data.ValueToAssignIsFloat = true;
            UpdateDataPageRight1();
        }

        public void UpdateDataPageLeft1()
        {
            // Change the text to Confirm ? there is bodies in level
            if (debugMenuPlusController.data.BodiesInLevelGetSet == true)
            {
                btnCleanBodies.GetComponentInChildren<Text>().text = "Confirm ?";
            }
            txtNbBodies.text = debugMenuPlusController.data.NbBodiesInLevelGetSet.ToString();
            

            // Change the text to Confirm ? there is items in level
            if (debugMenuPlusController.data.ItemsInLevelGetSet == true)
            {
                btnCleanItems.GetComponentInChildren<Text>().text = "Confirm ?";
            }
            txtNbItems.text = debugMenuPlusController.data.NbItemsInLevelGetSet.ToString();

            ValueToAssign();
            
            btnLimitValueBodies.GetComponentInChildren<Text>().text = debugMenuPlusController.data.NbBodiesLimitValueInLevelGetSet.ToString();
            btnLimitValueItems.GetComponentInChildren<Text>().text = debugMenuPlusController.data.NbItemsLimitValueInLevelGetSet.ToString();
            btnToggleKick.GetComponentInChildren<Text>().text = debugMenuPlusController.data.KickEnabledGetSet ? "Enabled" : "Disabled";
            btnToggleJump.GetComponentInChildren<Text>().text = debugMenuPlusController.data.JumpEnabledGetSet ? "Enabled" : "Disabled";

        }

        public void UpdateDataPageRight1()
        {
            if(btnTestInfoObject.gameObject.activeSelf == true)
            {
                btnTestInfoObject.gameObject.SetActive(true);
            }

            ValueToAssign();

            btnKickWidthAreaValue.GetComponentInChildren<Text>().text = debugMenuPlusController.data.KickWidthAreaValueGetSet.ToString();
            btnKickLengthValue.GetComponentInChildren<Text>().text = debugMenuPlusController.data.KickLengthValueGetSet.ToString();

        }

        public void ValueToAssign()
        {
            //Assign a value when enter keyboard is pressed
            if (debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet == true)
            {
                // Assign the limit value of bodies before despawning all bodies
                if (debugMenuPlusController.data.NbBodiesLimitValueButtonPressedGetSet == true)
                {
                    debugMenuPlusController.data.NbItemsLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KickWidthAreaValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KickLengthValueButtonPressedGetSet = false;

                    debugMenuPlusController.data.NbBodiesLimitValueInLevelGetSet = debugMenuPlusController.data.ValueToAssignedUintGetSet;
                    debugMenuPlusController.data.NbBodiesLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
                // Assign the limit value of items before despawning all items
                if (debugMenuPlusController.data.NbItemsLimitValueButtonPressedGetSet == true)
                {
                    debugMenuPlusController.data.NbBodiesLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KickWidthAreaValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KickLengthValueButtonPressedGetSet = false;

                    debugMenuPlusController.data.NbItemsLimitValueInLevelGetSet = debugMenuPlusController.data.ValueToAssignedUintGetSet;
                    debugMenuPlusController.data.NbItemsLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
                // Assign the value of Kick width area
                if (debugMenuPlusController.data.KickWidthAreaValueButtonPressedGetSet == true)
                {
                    debugMenuPlusController.data.NbItemsLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.NbBodiesLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KickLengthValueButtonPressedGetSet = false;

                    debugMenuPlusController.data.KickWidthAreaValueGetSet = debugMenuPlusController.data.ValueToAssignedFloatGetSet;
                    debugMenuPlusController.data.KickWidthAreaValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
                // Assign the value of Kick Length
                if (debugMenuPlusController.data.KickLengthValueButtonPressedGetSet == true)
                {
                    debugMenuPlusController.data.NbItemsLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.NbBodiesLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KickWidthAreaValueButtonPressedGetSet = false;

                    debugMenuPlusController.data.KickLengthValueGetSet = debugMenuPlusController.data.ValueToAssignedFloatGetSet;
                    debugMenuPlusController.data.KickLengthValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
            }
        }

        // Refresh the menu each frame (need optimization)
        public class DebugMenuPlusHook : MonoBehaviour
        {
            public DebugMenuPlusMenuModule menu;
            void Update()
            {
                menu.UpdateDataPageLeft1();
                menu.UpdateDataPageRight1();
            }
        }

    }
}

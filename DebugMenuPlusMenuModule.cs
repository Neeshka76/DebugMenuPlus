using ThunderRoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

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
        private Button btnRemoveImbuements;
        private Text txtNbImbuements;
        private Text txtColorblindness;
        private Button btnRightArrowColorblindness;
        private Button btnLeftArrowColorblindness;
        public Text txtSliderRedRedColor;
        public Text txtSliderRedGreenColor;
        public Text txtSliderRedBlueColor;
        public Text txtSliderGreenRedColor;
        public Text txtSliderGreenGreenColor;
        public Text txtSliderGreenBlueColor;
        public Text txtSliderBlueRedColor;
        public Text txtSliderBlueGreenColor;
        public Text txtSliderBlueBlueColor;
        public Slider sliderRedRedColor;
        public Slider sliderRedGreenColor;
        public Slider sliderRedBlueColor;
        public Slider sliderGreenRedColor;
        public Slider sliderGreenGreenColor;
        public Slider sliderGreenBlueColor;
        public Slider sliderBlueRedColor;
        public Slider sliderBlueGreenColor;
        public Slider sliderBlueBlueColor;
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
            btnRemoveImbuements = menu.GetCustomReference("btn_RemoveImbuements").GetComponent<Button>();
            txtNbImbuements = menu.GetCustomReference("txt_NbImbuements").GetComponent<Text>();
            txtColorblindness = menu.GetCustomReference("txt_Colorblindness").GetComponent<Text>();
            btnRightArrowColorblindness = menu.GetCustomReference("btn_RightArrowColorblindness").GetComponent<Button>();
            btnLeftArrowColorblindness = menu.GetCustomReference("btn_LeftArrowColorblindness").GetComponent<Button>();
            txtSliderRedRedColor = menu.GetCustomReference("txt_SliderRedRedColor").GetComponent<Text>();
            txtSliderRedGreenColor = menu.GetCustomReference("txt_SliderRedGreenColor").GetComponent<Text>();
            txtSliderRedBlueColor = menu.GetCustomReference("txt_SliderRedBlueColor").GetComponent<Text>();
            txtSliderGreenRedColor = menu.GetCustomReference("txt_SliderGreenRedColor").GetComponent<Text>();
            txtSliderGreenGreenColor = menu.GetCustomReference("txt_SliderGreenGreenColor").GetComponent<Text>();
            txtSliderGreenBlueColor = menu.GetCustomReference("txt_SliderGreenBlueColor").GetComponent<Text>();
            txtSliderBlueRedColor = menu.GetCustomReference("txt_SliderBlueRedColor").GetComponent<Text>();
            txtSliderBlueGreenColor = menu.GetCustomReference("txt_SliderBlueGreenColor").GetComponent<Text>();
            txtSliderBlueBlueColor = menu.GetCustomReference("txt_SliderBlueBlueColor").GetComponent<Text>();
            sliderRedRedColor = menu.GetCustomReference("slider_RedRedColor").GetComponent<Slider>();
            sliderRedGreenColor = menu.GetCustomReference("slider_RedGreenColor").GetComponent<Slider>();
            sliderRedBlueColor = menu.GetCustomReference("slider_RedBlueColor").GetComponent<Slider>();
            sliderGreenRedColor = menu.GetCustomReference("slider_GreenRedColor").GetComponent<Slider>();
            sliderGreenGreenColor = menu.GetCustomReference("slider_GreenGreenColor").GetComponent<Slider>();
            sliderGreenBlueColor = menu.GetCustomReference("slider_GreenBlueColor").GetComponent<Slider>();
            sliderBlueRedColor = menu.GetCustomReference("slider_BlueRedColor").GetComponent<Slider>();
            sliderBlueGreenColor = menu.GetCustomReference("slider_BlueGreenColor").GetComponent<Slider>();
            sliderBlueBlueColor = menu.GetCustomReference("slider_BlueBlueColor").GetComponent<Slider>();

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
            btnRemoveImbuements.onClick.AddListener(ClickRemoveImbuements);
            btnLeftArrowColorblindness.onClick.AddListener(ClickDecreaseColorblindess);
            btnRightArrowColorblindness.onClick.AddListener(ClickIncreaseColorblindess);


            sliderRedRedColor.wholeNumbers = false;
            sliderRedRedColor.minValue = 0;
            sliderRedRedColor.maxValue = 100f;
            sliderRedRedColor.value = sliderRedRedColor.maxValue;
            sliderRedGreenColor.wholeNumbers = false;
            sliderRedGreenColor.minValue = 0;
            sliderRedGreenColor.maxValue = 100f;
            sliderRedGreenColor.value = sliderRedGreenColor.maxValue;
            sliderRedBlueColor.wholeNumbers = false;
            sliderRedBlueColor.minValue = 0;
            sliderRedBlueColor.maxValue = 100f;
            sliderRedBlueColor.value = sliderRedBlueColor.maxValue;

            sliderGreenRedColor.wholeNumbers = false;
            sliderGreenRedColor.minValue = 0;
            sliderGreenRedColor.maxValue = 100f;
            sliderGreenRedColor.value = sliderGreenRedColor.maxValue;
            sliderGreenGreenColor.wholeNumbers = false;
            sliderGreenGreenColor.minValue = 0;
            sliderGreenGreenColor.maxValue = 100f;
            sliderGreenGreenColor.value = sliderGreenGreenColor.maxValue;
            sliderGreenBlueColor.wholeNumbers = false;
            sliderGreenBlueColor.minValue = 0;
            sliderGreenBlueColor.maxValue = 100f;
            sliderGreenBlueColor.value = sliderGreenBlueColor.maxValue;

            sliderBlueRedColor.wholeNumbers = false;
            sliderBlueRedColor.minValue = 0;
            sliderBlueRedColor.maxValue = 100f;
            sliderBlueRedColor.value = sliderBlueRedColor.maxValue;
            sliderBlueGreenColor.wholeNumbers = false;
            sliderBlueGreenColor.minValue = 0;
            sliderBlueGreenColor.maxValue = 100f;
            sliderBlueGreenColor.value = sliderBlueGreenColor.maxValue;
            sliderBlueBlueColor.wholeNumbers = false;
            sliderBlueBlueColor.minValue = 0;
            sliderBlueBlueColor.maxValue = 100f;
            sliderBlueBlueColor.value = sliderBlueBlueColor.maxValue;

            // Add an event listener for sliders

            sliderRedRedColor.onValueChanged.AddListener(delegate { ValueChangedSliderRedRedColor(); });
            sliderRedGreenColor.onValueChanged.AddListener(delegate { ValueChangedSliderRedGreenColor(); });
            sliderRedBlueColor.onValueChanged.AddListener(delegate { ValueChangedSliderRedBlueColor(); });

            sliderGreenRedColor.onValueChanged.AddListener(delegate { ValueChangedSliderGreenRedColor(); });
            sliderGreenGreenColor.onValueChanged.AddListener(delegate { ValueChangedSliderGreenGreenColor(); });
            sliderGreenBlueColor.onValueChanged.AddListener(delegate { ValueChangedSliderGreenBlueColor(); });

            sliderBlueRedColor.onValueChanged.AddListener(delegate { ValueChangedSliderBlueRedColor(); });
            sliderBlueGreenColor.onValueChanged.AddListener(delegate { ValueChangedSliderBlueGreenColor(); });
            sliderBlueBlueColor.onValueChanged.AddListener(delegate { ValueChangedSliderBlueBlueColor(); });

            // Initialization of datas
            debugMenuPlusController = GameManager.local.gameObject.AddComponent<DebugMenuPlusController>();
            debugMenuPlusController.data.NbBodiesInLevelGetSet = -1;
            debugMenuPlusController.data.NbItemsInLevelGetSet = -1;
            debugMenuPlusController.data.NbImbuementsInLevelGetSet = -1;
            debugMenuPlusController.data.NbBodiesLimitValueInLevelGetSet = 30;
            debugMenuPlusController.data.NbItemsLimitValueInLevelGetSet = 100;
            debugMenuPlusController.data.KickEnabledGetSet = true;
            debugMenuPlusController.data.JumpEnabledGetSet = true;
            debugMenuPlusController.data.KickWidthAreaValueGetSet = 0.2f;
            debugMenuPlusController.data.KickLengthValueGetSet = 2f;
            debugMenuPlusController.data.indexColorblindnessGetSet = 0;
            debugMenuPlusController.data.nbMaxColorblindnessGetSet = 8;
            debugMenuPlusController.data.colorblindnessNameGetSet = "Default";
            debugMenuPlusController.data.changecolorblindnessSliderGetSet = false;
            debugMenuPlusController.data.sliderChangedGetSet = false;
            debugMenuPlusController.data.changeSlidersGetSet = false;


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
            btnTestInfoObject.GetComponentInChildren<Text>().text = debugMenuPlusController.data.ItemsInLevelGetSet ? "Test" : "Error";
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

        // When selector is click change display to Error or Clean if no items
        public void ClickRemoveImbuements()
        {
            debugMenuPlusController.data.RemoveImbuementsGetSet = true;
            btnRemoveImbuements.GetComponentInChildren<Text>().text = debugMenuPlusController.data.ImbuementInLevelGetSet ? "Remove" : "Error";
            UpdateDataPageRight1();
        }

        public void ClickIncreaseColorblindess()
        {
            if (debugMenuPlusController.data.indexColorblindnessGetSet <= 7)
            {
                debugMenuPlusController.data.indexColorblindnessGetSet++;
                UpdateDataPageLeft1();
            }
        }
        public void ClickDecreaseColorblindess()
        {
            if (debugMenuPlusController.data.indexColorblindnessGetSet >= 1)
            {
                debugMenuPlusController.data.indexColorblindnessGetSet--;
                UpdateDataPageLeft1();
            }
        }

        public void ValueChangedSliderRedRedColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            {
                debugMenuPlusController.data.colorblindnessRedOutRedInValueGetSet = sliderRedRedColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
        }
        public void ValueChangedSliderRedGreenColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            { 
                debugMenuPlusController.data.colorblindnessRedOutGreenInValueGetSet = sliderRedGreenColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
        }
        public void ValueChangedSliderRedBlueColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            {
                debugMenuPlusController.data.colorblindnessRedOutBlueInValueGetSet = sliderRedBlueColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
        }
        public void ValueChangedSliderGreenRedColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            {
                debugMenuPlusController.data.colorblindnessGreenOutRedInValueGetSet = sliderGreenRedColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
        }
        public void ValueChangedSliderGreenGreenColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            {
                debugMenuPlusController.data.colorblindnessGreenOutGreenInValueGetSet = sliderGreenGreenColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
        }
        public void ValueChangedSliderGreenBlueColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            {
                debugMenuPlusController.data.colorblindnessGreenOutBlueInValueGetSet = sliderGreenBlueColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
        }
        public void ValueChangedSliderBlueRedColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            {
                debugMenuPlusController.data.colorblindnessBlueOutRedInValueGetSet = sliderBlueRedColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
        }
        public void ValueChangedSliderBlueGreenColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            {
                debugMenuPlusController.data.colorblindnessBlueOutGreenInValueGetSet = sliderBlueGreenColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
        }
        public void ValueChangedSliderBlueBlueColor()
        {
            if (!debugMenuPlusController.data.changeSlidersGetSet)
            {
                debugMenuPlusController.data.colorblindnessBlueOutBlueInValueGetSet = sliderBlueBlueColor.value;
                debugMenuPlusController.data.sliderChangedGetSet = true;
            }
            UpdateDataPageLeft1();
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

            if (debugMenuPlusController.data.indexColorblindnessGetSet == 0)
            {
                if (btnLeftArrowColorblindness.IsActive())
                    btnLeftArrowColorblindness.enabled = false;
            }
            else
            {
                btnLeftArrowColorblindness.enabled = true;
            }
            if (debugMenuPlusController.data.indexColorblindnessGetSet == debugMenuPlusController.data.nbMaxColorblindnessGetSet)
            {
                if (btnRightArrowColorblindness.IsActive())
                    btnRightArrowColorblindness.enabled = false;
            }
            else
            {
                btnRightArrowColorblindness.enabled = true;
            }
            txtSliderRedRedColor.text = debugMenuPlusController.data.colorblindnessRedOutRedInValueGetSet.ToString("0.000");
            txtSliderRedGreenColor.text = debugMenuPlusController.data.colorblindnessRedOutGreenInValueGetSet.ToString("0.000");
            txtSliderRedBlueColor.text = debugMenuPlusController.data.colorblindnessRedOutBlueInValueGetSet.ToString("0.000");
            txtSliderGreenRedColor.text = debugMenuPlusController.data.colorblindnessGreenOutRedInValueGetSet.ToString("0.000");
            txtSliderGreenGreenColor.text = debugMenuPlusController.data.colorblindnessGreenOutGreenInValueGetSet.ToString("0.000");
            txtSliderGreenBlueColor.text = debugMenuPlusController.data.colorblindnessGreenOutBlueInValueGetSet.ToString("0.000");
            txtSliderBlueRedColor.text = debugMenuPlusController.data.colorblindnessBlueOutRedInValueGetSet.ToString("0.000");
            txtSliderBlueGreenColor.text = debugMenuPlusController.data.colorblindnessBlueOutGreenInValueGetSet.ToString("0.000");
            txtSliderBlueBlueColor.text = debugMenuPlusController.data.colorblindnessBlueOutBlueInValueGetSet.ToString("0.000");
            if(debugMenuPlusController.data.changeSlidersGetSet)
            {
                sliderRedRedColor.value = debugMenuPlusController.data.colorblindnessRedOutRedInValueGetSet;
                sliderRedGreenColor.value = debugMenuPlusController.data.colorblindnessRedOutGreenInValueGetSet;
                sliderRedBlueColor.value = debugMenuPlusController.data.colorblindnessRedOutBlueInValueGetSet;
                sliderGreenRedColor.value = debugMenuPlusController.data.colorblindnessGreenOutRedInValueGetSet;
                sliderGreenGreenColor.value = debugMenuPlusController.data.colorblindnessGreenOutGreenInValueGetSet;
                sliderGreenBlueColor.value = debugMenuPlusController.data.colorblindnessGreenOutBlueInValueGetSet;
                sliderBlueRedColor.value = debugMenuPlusController.data.colorblindnessBlueOutRedInValueGetSet;
                sliderBlueGreenColor.value = debugMenuPlusController.data.colorblindnessBlueOutGreenInValueGetSet;
                sliderBlueBlueColor.value = debugMenuPlusController.data.colorblindnessBlueOutBlueInValueGetSet;
                debugMenuPlusController.data.changeSlidersGetSet = false;
            }
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

            // Change the text to Confirm ? there is imbuements in level
            if (debugMenuPlusController.data.ImbuementInLevelGetSet == true)
            {
                btnRemoveImbuements.GetComponentInChildren<Text>().text = "Confirm ?";
            }
            txtNbImbuements.text = debugMenuPlusController.data.NbImbuementsInLevelGetSet.ToString();
            txtColorblindness.text = debugMenuPlusController.data.colorblindnessNameGetSet.ToString();
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

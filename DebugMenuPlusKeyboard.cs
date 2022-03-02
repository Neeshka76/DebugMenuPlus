using System;
using System.Collections;
using ThunderRoad;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace DebugMenuPlus
{
    public class DebugMenuPlusKeyboard : LevelModule
    {
        private GameObject keyboard;
        AsyncOperationHandle<GameObject> handleKeyboard;
        public Transform transKeyboard;
        private bool previousStateKeyboard = false;
        private bool currentStateKeyboard = false;
        private bool errorInput = false;
        private Button button0;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button buttonDot;
        private Button buttonEnter;
        private Button buttonPlusMinus;
        private Button buttonDelete;
        private Button buttonClear;
        private float valueToAssignFloat = 0.0f;
        private int valueToAssignInt = 0;
        private uint valueToAssignUint = 0;
        GameObject keyboardCanvas;
        private Text txtInput;
        private DebugMenuPlusController debugMenuPlusController;

        public override IEnumerator OnLoadCoroutine()
        {
            debugMenuPlusController = GameManager.local.gameObject.GetComponent<DebugMenuPlusController>();
            return base.OnLoadCoroutine();
        }
        public override void Update()
        {
            if (debugMenuPlusController == null)
            {
                debugMenuPlusController = GameManager.local.gameObject.GetComponent<DebugMenuPlusController>();
                return;
            }
            else
            {
                if (keyboard == null && Player.currentCreature != null)
                {
                    // Creates the Keyboard
                    handleKeyboard = Addressables.LoadAssetAsync<GameObject>("Neeshka.DebugMenuPlus.Keyboard");
                    keyboard = handleKeyboard.WaitForCompletion();
                    keyboard = UnityEngine.Object.Instantiate(keyboard);
                    keyboard.SetActive(false);
                    keyboard.transform.position = (Player.currentCreature.transform.position + new Vector3(0.0f, 2.0f, 0.0f));
                    keyboardCanvas = keyboard.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
                    txtInput = keyboard.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
                    /*
                    //first I'll create a string array containing all of the different strings which will be fed into the loops and from there into the AddListener stuff via a lambda function
                    string[,] keypadKeys = {    { "7", "8", "9" }, //this is row 0
                                                { "4", "5", "6" }, //row 1
                                                { "1", "2", "3" }, //row 2
                                                { "-", "0", "." }, //row 3
                                                { "Enter", "Clear", "Delete" } }; //row 4
                    Debug.Log("DebugMenuPlus : Length : " + keypadKeys.Length);
                    Debug.Log("DebugMenuPlus : Row : " + keypadKeys.GetLength(0));
                    Debug.Log("DebugMenuPlus : Column : " + keypadKeys.GetLength(1));
                    
                    for (int r = 0; r < keypadKeys.GetLength(0); r++) //for loop over the length of keypadKeys, which defaults to the number of rows. useful if you want to add more rows
                    {
                        for (int c = 0; c < keypadKeys.GetLength(1); c++) //for loop which does three iterations of its loop, which works for the length of each row
                        {
                            Button button = keyboardCanvas.transform.GetChild(r).gameObject.transform.GetChild(c).gameObject.GetComponent<Button>(); //gets the button for the row (r) and column (c)
                            button.onClick.AddListener(() => ButtonClick(keypadKeys[r, c])); //adds an anonymous function which calls your ButtonClick function using the correct string for the button as defined above. this means that no matter which button this is, as long as the above keypadKeys array is correct, it'll perform the right action
                            Debug.Log("DebugMenuPlus : Row : " + r);
                            Debug.Log("DebugMenuPlus : Column : " + c);
                            Debug.Log("DebugMenuPlus : String : \"" + keypadKeys[r, c] + "\"");
                        }
                    }*/
                    
                    button7 = keyboardCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
                    button8 = keyboardCanvas.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
                    button9 = keyboardCanvas.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
                    button4 = keyboardCanvas.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
                    button5 = keyboardCanvas.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
                    button6 = keyboardCanvas.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
                    button1 = keyboardCanvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
                    button2 = keyboardCanvas.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
                    button3 = keyboardCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
                    buttonPlusMinus = keyboardCanvas.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
                    button0 = keyboardCanvas.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
                    buttonDot = keyboardCanvas.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
                    buttonEnter = keyboardCanvas.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
                    buttonClear = keyboardCanvas.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
                    buttonDelete = keyboardCanvas.transform.GetChild(4).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
                    // Add an event listener for buttons
                    button7.onClick.AddListener(ClickButton7);
                    button8.onClick.AddListener(ClickButton8);
                    button9.onClick.AddListener(ClickButton9);
                    button4.onClick.AddListener(ClickButton4);
                    button5.onClick.AddListener(ClickButton5);
                    button6.onClick.AddListener(ClickButton6);
                    button1.onClick.AddListener(ClickButton1);
                    button2.onClick.AddListener(ClickButton2);
                    button3.onClick.AddListener(ClickButton3);
                    button0.onClick.AddListener(ClickButton0);
                    buttonPlusMinus.onClick.AddListener(ClickButtonPlusMinus);
                    buttonDot.onClick.AddListener(ClickButtonDot);
                    buttonEnter.onClick.AddListener(ClickButtonEnter);
                    buttonClear.onClick.AddListener(ClickButtonClear);
                    buttonDelete.onClick.AddListener(ClickButtonDelete);
                    // Initialization of datas
                    debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                    debugMenuPlusController.data.NbBodiesLimitValueButtonPressedGetSet = false;
                    debugMenuPlusController.data.NbItemsLimitValueButtonPressedGetSet = false;
                }

                if (Player.currentCreature != null)
                {
                    currentStateKeyboard = (debugMenuPlusController.data.NbBodiesLimitValueButtonPressedGetSet
                        ^ debugMenuPlusController.data.NbItemsLimitValueButtonPressedGetSet
                        ^ debugMenuPlusController.data.KickWidthAreaValueButtonPressedGetSet
                        ^ debugMenuPlusController.data.KickLengthValueButtonPressedGetSet) && !keyboard.activeSelf;
                    // Rising edge
                    if (keyboard != null && currentStateKeyboard != previousStateKeyboard && previousStateKeyboard == false)
                    {
                        keyboard.SetActive(true);
                        transKeyboard = keyboard.transform;
                        keyboard.layer = 5;
                    }
                    // Deactivate the keyboard
                    if (debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet == true && keyboard.activeSelf)
                    {
                        keyboard.SetActive(false);
                    }
                    // Set the new state
                    previousStateKeyboard = currentStateKeyboard;
                    if (keyboard.activeSelf)
                    {
                        transKeyboard.position = MenuBook.local.gameObject.transform.position + new Vector3(0.0f, 0.4f, 0.0f);
                        //transKeyboard.position = (transKeyboard.localPosition- MenuBook.local.gameObject.transform.position).normalized * 0.4f + MenuBook.local.gameObject.transform.position;
                        transKeyboard.rotation = MenuBook.local.gameObject.transform.rotation;
                    }
                }
            }
        }
        /*
        void ButtonClick(string text)
        {
            Debug.Log("DebugMenuPlus : TEXT BUTTON CLICK : " + text);
            switch (text)
            {
                case "Clear":
                    txtInput.text = "";
                break;
                case "Delete":
                    if (txtInput.text.Length != 0)
                    {
                        txtInput.text = txtInput.text.Remove(txtInput.text.Length - 1, 1);
                    }
                break;
                case "Enter":
                    //in this case, you may want to keep the "ClickButtonEnter()" function and call it from here
                    ClickButtonEnter();
                break;
                case "-":
                    if (txtInput.text.StartsWith("-"))
                    {
                        // Remove the - at the beginning of the string
                        txtInput.text = txtInput.text.Remove(0, 1);
                    }
                    else
                    {
                        // Add the - at the beginning of the string
                        txtInput.text = txtInput.text.Insert(0, "-");
                    }
                break;
                default:
                    if (errorInput == false)
                    {
                        txtInput.text += text;
                    }
                    else
                    {
                        txtInput.text = text;
                        errorInput = false;
                    }
                break;
            }
        }*/

        public void ClickButton7()
        {
            if (errorInput == false)
            {
                txtInput.text += "7";
            }
            else
            {
                txtInput.text = "7";
                errorInput = false;
            }
        }
        public void ClickButton8()
        {
            if (errorInput == false)
            {
                txtInput.text += "8";
            }
            else
            {
                txtInput.text = "8";
                errorInput = false;
            }
        }
        public void ClickButton9()
        {
            if (errorInput == false)
            {
                txtInput.text += "9";
            }
            else
            {
                txtInput.text = "9";
                errorInput = false;
            }
        }
        public void ClickButton4()
        {
            if (errorInput == false)
            {
                txtInput.text += "4";
            }
            else
            {
                txtInput.text = "4";
                errorInput = false;
            }
        }
        public void ClickButton5()
        {
            if (errorInput == false)
            {
                txtInput.text += "5";
            }
            else
            {
                txtInput.text = "5";
                errorInput = false;
            }
        }
        public void ClickButton6()
        {
            if (errorInput == false)
            {
                txtInput.text += "6";
            }
            else
            {
                txtInput.text = "6";
                errorInput = false;
            }
        }
        public void ClickButton1()
        {
            if (errorInput == false)
            {
                txtInput.text += "1";
            }
            else
            {
                txtInput.text = "1";
                errorInput = false;
            }
        }
        public void ClickButton2()
        {
            if (errorInput == false)
            {
                txtInput.text += "2";
            }
            else
            {
                txtInput.text = "2";
                errorInput = false;
            }
        }
        public void ClickButton3()
        {
            if (errorInput == false)
            {
                txtInput.text += "3";
            }
            else
            {
                txtInput.text = "3";
                errorInput = false;
            }
        }
        public void ClickButtonPlusMinus()
        {
            if (txtInput.text.StartsWith("-"))
            {
                // Remove the - at the beginning of the string
                txtInput.text = txtInput.text.Remove(0, 1);
            }
            else
            {
                // Add the - at the beginning of the string
                txtInput.text = txtInput.text.Insert(0, "-");
            }
        }
        public void ClickButton0()
        {
            if (errorInput == false)
            {
                txtInput.text += "0";
            }
            else
            {
                txtInput.text = "0";
                errorInput = false;
            }
        }
        public void ClickButtonDot()
        {
            if (errorInput == false)
            {
                txtInput.text += ".";
            }
            else
            {
                txtInput.text = ".";
                errorInput = false;
            }
        }
        public void ClickButtonEnter()
        {
            // Try if it's a float else display a message
            if(debugMenuPlusController.data.ValueToAssignIsFloat == true)
            {
                if (float.TryParse(txtInput.text, out valueToAssignFloat))
                {
                    debugMenuPlusController.data.ValueToAssignedFloatGetSet = valueToAssignFloat;
                    debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet = true;
                    debugMenuPlusController.data.ValueToAssignIsFloat = false;
                    txtInput.text = "";
                }
                else
                {
                    txtInput.text = "Error, wrong format entered";
                }
            }
            if (debugMenuPlusController.data.ValueToAssignIsInt == true)
            {
                if (int.TryParse(txtInput.text, out valueToAssignInt))
                {
                    debugMenuPlusController.data.ValueToAssignedIntGetSet = valueToAssignInt;
                    debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet = true;
                    debugMenuPlusController.data.ValueToAssignIsInt = false;
                    txtInput.text = "";
                }
                else
                {
                    txtInput.text = "Error, wrong format entered enter a value without decimals";
                }
            }
            if (debugMenuPlusController.data.ValueToAssignIsUint == true)
            {
                if (uint.TryParse(txtInput.text, out valueToAssignUint))
                {
                    debugMenuPlusController.data.ValueToAssignedUintGetSet = valueToAssignUint;
                    debugMenuPlusController.data.KeyboardFinishEnterButtonPressedGetSet = true;
                    debugMenuPlusController.data.ValueToAssignIsUint = false;
                    txtInput.text = "";
                }
                else
                {
                    txtInput.text = "Error, wrong format entered, enter a positive value without decimals";
                }
            }
        }
        public void ClickButtonClear()
        {
            // Clear the string
            txtInput.text = "";
        }
        public void ClickButtonDelete()
        {
            // Remove the last element of the string
            if (txtInput.text.Length != 0)
            {
                txtInput.text = txtInput.text.Remove(txtInput.text.Length - 1, 1);
            }
        }
    }
}

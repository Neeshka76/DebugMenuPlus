using ThunderRoad;
using UnityEngine;
using System.Collections;
using System;

namespace DebugMenuPlus
{
    class DebugMenuPlusLevelModule : LevelModule
    {
        private DebugMenuPlusController debugMenuPlusController;
        float timeToDespawnBodies = 4.0f;
        float timer = 0.0f;
        // When a level is loaded
        public override IEnumerator OnLoadCoroutine()
        {
            debugMenuPlusController = GameManager.local.gameObject.GetComponent<DebugMenuPlusController>();
            return base.OnLoadCoroutine();
        }
        // Update the location of the player
        public override void Update()
        {
            if (debugMenuPlusController == null)
            {
                debugMenuPlusController = GameManager.local.gameObject.GetComponent<DebugMenuPlusController>();
                return;
            }
            else
            {
                // Count Bodies & Items & Imbuements
                GameManager.local.StartCoroutine(CountBodies());
                GameManager.local.StartCoroutine(CountItems());
                GameManager.local.StartCoroutine(CountImbuements());
                // Set a timer before the bodies disappear
                if(debugMenuPlusController.data.NbBodiesInLevelGetSet >= debugMenuPlusController.data.NbBodiesLimitValueInLevelGetSet)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0.0f;
                }
                // Clean Items past a certain amount or manually
                if (debugMenuPlusController.data.CleanBodiesGetSet == true || (debugMenuPlusController.data.NbBodiesInLevelGetSet >= debugMenuPlusController.data.NbBodiesLimitValueInLevelGetSet && timeToDespawnBodies <= timer))
                {
                    GameManager.local.StartCoroutine(CleanBodies());
                }
                // Clear Items past a certain amount or manually
                if (debugMenuPlusController.data.CleanItemsGetSet == true || debugMenuPlusController.data.NbItemsInLevelGetSet >= debugMenuPlusController.data.NbItemsLimitValueInLevelGetSet)
                {
                    GameManager.local.StartCoroutine(CleanItems());
                }
                // Clear Items past a certain amount or manually
                if (debugMenuPlusController.data.RemoveImbuementsGetSet == true)
                {
                    GameManager.local.StartCoroutine(RemoveImbuements());
                }

                if (debugMenuPlusController.data.TestInfoObjectGetSet == true)
                {
                    GameManager.local.StartCoroutine(ScanItemsInfos());
                }


                PlayerControl.local.kickEnabled = debugMenuPlusController.data.KickEnabledGetSet;
                PlayerControl.local.jumpEnabled = debugMenuPlusController.data.JumpEnabledGetSet;
                PlayerControl.local.kickRayRadius = debugMenuPlusController.data.KickWidthAreaValueGetSet;
                PlayerControl.local.kickRayLenght = debugMenuPlusController.data.KickLengthValueGetSet;
            }

            IEnumerator CountBodies()
            {
                debugMenuPlusController.data.NbBodiesInLevelGetSet = 0;
                // Clean Bodies
                for (int index = Creature.allActive.Count - 1; index >= 0; --index)
                {
                    if (Creature.allActive[index].state == Creature.State.Dead)
                    {
                        debugMenuPlusController.data.BodiesInLevelGetSet = true;
                        debugMenuPlusController.data.NbBodiesInLevelGetSet++;
                    }
                }
                yield return null;
            }

            IEnumerator CleanBodies()
            {
                for (int index = Creature.allActive.Count - 1; index >= 0; --index)
                {
                    if (Creature.allActive[index].state == Creature.State.Dead)
                    {
                        Creature.allActive[index].Despawn();
                    }
                    else
                    {
                        debugMenuPlusController.data.BodiesInLevelGetSet = false;
                    }
                }
                debugMenuPlusController.data.CleanBodiesGetSet = false;
                yield return null;
            }

            IEnumerator CountItems()
            {
                debugMenuPlusController.data.NbItemsInLevelGetSet = 0;
                for (int index = Item.allActive.Count - 1; index >= 0; --index)
                {
                    //if (Item.list.Count - 1 != -1)
                    if ((double)Item.allActive[index].spawnTime != 0.0
                                && !(bool)Item.allActive[index].holder
                                && (!Item.allActive[index].isTelekinesisGrabbed
                                && !Item.allActive[index].isThrowed)
                                && (!Item.allActive[index].IsHanded()
                                && !Item.allActive[index].disallowDespawn
                                && !Item.allActive[index].isGripped)
                                || (Item.allActive[index].itemId == "ModularTile" && Item.allActive[index].isThrowed == true)
                                || (Item.allActive[index].itemId == "DaggerCommon" && !Item.allActive[index].IsHanded() && !Item.allActive[index].isGripped && !Item.allActive[index].disallowDespawn && !(bool)Item.allActive[index].holder)
                                )
                    {
                        debugMenuPlusController.data.ItemsInLevelGetSet = true;
                        debugMenuPlusController.data.NbItemsInLevelGetSet++;
                    }
                }
                yield return null;
            }

            IEnumerator CleanItems()
            {
                for (int index = Item.allActive.Count - 1; index >= 0; --index)
                {
                    //if (Item.list.Count - 1 != -1)
                    if ((double)Item.allActive[index].spawnTime != 0.0
                            && !(bool)Item.allActive[index].holder
                            && (!Item.allActive[index].isTelekinesisGrabbed
                            && !Item.allActive[index].isThrowed)
                            && (!Item.allActive[index].IsHanded()
                            && !Item.allActive[index].disallowDespawn
                            && !Item.allActive[index].isGripped)
                            || (Item.allActive[index].itemId == "ModularTile" && Item.allActive[index].isThrowed == true)
                            || (Item.allActive[index].itemId == "DaggerCommon" && !Item.allActive[index].IsHanded() && !Item.allActive[index].isGripped && !Item.allActive[index].disallowDespawn && !(bool)Item.allActive[index].holder)
                            || (Item.allActive[index].itemId == "ModularPotion")
                            )
                    {
                        Item.allActive[index].Despawn();
                    }
                    else
                    {
                        debugMenuPlusController.data.ItemsInLevelGetSet = false;
                    }
                }
                debugMenuPlusController.data.CleanItemsGetSet = false;
                yield return null;
            }

            IEnumerator CountImbuements()
            {
                debugMenuPlusController.data.NbImbuementsInLevelGetSet = 0;
                for (int index = Item.allActive.Count - 1; index >= 0; --index)
                {
                    if (Item.allActive[index].imbues.Count != 0)
                    {
                        foreach (Imbue imbue in Item.allActive[index].imbues)
                        {
                            if (imbue.energy != 0.0f)
                            {
                                debugMenuPlusController.data.ImbuementInLevelGetSet = true;
                                debugMenuPlusController.data.NbImbuementsInLevelGetSet++;
                            }
                        }
                    }
                }
                yield return null;
            }

            IEnumerator RemoveImbuements()
            {
                for (int index = Item.allActive.Count - 1; index >= 0; --index)
                {
                    foreach (Imbue imbue in Item.allActive[index].imbues)
                    {
                        if (imbue.energy != 0.0f)
                        {
                            imbue.energy = 0.0f;
                        }
                        else
                        {
                            debugMenuPlusController.data.ImbuementInLevelGetSet = false;
                        }
                    }
                }
                debugMenuPlusController.data.RemoveImbuementsGetSet = false;
                yield return null;
            }

            IEnumerator ScanItemsInfos()
            {
                for (int index = Item.allActive.Count - 1; index >= 0; --index)
                {
                    if (Item.allActive.Count - 1 != -1)
                    {
                        Debug.Log("DebugMenuPlus : ----------------------------------- : ");
                        Debug.Log("DebugMenuPlus : INDEX Number : " + index);
                        Debug.Log("DebugMenuPlus : Item Data Id : " + Item.allActive[index].data.id.ToString());
                        Debug.Log("DebugMenuPlus : Item Id : " + Item.allActive[index].itemId.ToString());
                        Debug.Log("DebugMenuPlus : Item Position : " + Item.allActive[index].transform.position.ToString());
                        Debug.Log("DebugMenuPlus : Item SpawnTime : " + Item.allActive[index].spawnTime.ToString());
                        Debug.Log("DebugMenuPlus : Item Author : " + Item.allActive[index].data.author.ToString());
                        Debug.Log("DebugMenuPlus : Item DisplayName : " + Item.allActive[index].data.displayName.ToString());
                        Debug.Log("DebugMenuPlus : Item handlerArmGrabbed : " + Item.allActive[index].handlerArmGrabbed.ToString());
                        //Debug.Log("DebugMenuPlus : Item holder : " + Item.allActive[index].holder.ToString());
                        Debug.Log("DebugMenuPlus : Item is TK Grabbed : " + Item.allActive[index].isTelekinesisGrabbed.ToString());
                        Debug.Log("DebugMenuPlus : Item is Throwned : " + Item.allActive[index].isThrowed.ToString());
                        Debug.Log("DebugMenuPlus : Item is handed : " + Item.allActive[index].IsHanded().ToString());
                        Debug.Log("DebugMenuPlus : Item is Disallow Despawn : " + Item.allActive[index].disallowDespawn.ToString());
                        Debug.Log("DebugMenuPlus : Item is gripped : " + Item.allActive[index].isGripped.ToString());
                        Debug.Log("DebugMenuPlus : Item is Flying : " + Item.allActive[index].isFlying.ToString());
                        Debug.Log("DebugMenuPlus : Item is Pooled : " + Item.allActive[index].isPooled.ToString());
                        Debug.Log("DebugMenuPlus : Item is Penetrating : " + Item.allActive[index].isPenetrating.ToString());
                        Debug.Log("DebugMenuPlus : Item is twoHanded : " + Item.allActive[index].IsTwoHanded().ToString());
                        Debug.Log("DebugMenuPlus : Item distant grab safe distance : " + Item.allActive[index].distantGrabSafeDistance.ToString());
                        Debug.Log("DebugMenuPlus : Item fly rotation speed : " + Item.allActive[index].flyRotationSpeed.ToString());
                        Debug.Log("DebugMenuPlus : Item fly ThrowAngle : " + Item.allActive[index].flyThrowAngle.ToString());
                        //Debug.Log("DebugMenuPlus : Item Last Handler : " + Item.allActive[index].lastHandler.ToString());
                        Debug.Log("DebugMenuPlus : Item Last interation time : " + Item.allActive[index].lastInteractionTime.ToString());
                        Debug.Log("DebugMenuPlus : Item Name : " + Item.allActive[index].name.ToString());
                        Debug.Log("DebugMenuPlus : Item Org Mass : " + Item.allActive[index].orgMass.ToString());
                        Debug.Log("DebugMenuPlus : Item ParryPoint : " + Item.allActive[index].parryPoint.ToString());
                        Debug.Log("DebugMenuPlus : Item Snap Pitch Range : " + Item.allActive[index].snapPitchRange.ToString());
                        Debug.Log("DebugMenuPlus : Item use Custom Center of Mass : " + Item.allActive[index].useCustomCenterOfMass.ToString());
                        Debug.Log("DebugMenuPlus : Item Data Mass : " + Item.allActive[index].data.mass.ToString());
                        //Debug.Log("DebugMenuPlus : Item Data Prefab Address : " + Item.allActive[index].data.prefabAddress.ToString());
                        //Debug.Log("DebugMenuPlus : Item Data Prefab Location : " + Item.allActive[index].data.prefabLocation.ToString());
                        Debug.Log("DebugMenuPlus : Item Data Save Folder : " + Item.allActive[index].data.saveFolder.ToString());
                        Debug.Log("DebugMenuPlus : Item Data Slot : " + Item.allActive[index].data.slot.ToString());
                        Debug.Log("DebugMenuPlus : Item Data  : " + Item.allActive[index].GetInstanceID().ToString());
                        //Debug.Log("DebugMenuPlus : Item Data snap Audio Container Address : " + Item.allActive[index].data.snapAudioContainerAddress.ToString());
                        //Debug.Log("DebugMenuPlus : Item Data snap Audio Container Location : " + Item.allActive[index].data.snapAudioContainerLocation.ToString());
                    }
                }
                debugMenuPlusController.data.TestInfoObjectGetSet = false;
                yield return null;
            }
        }
        
    }
}

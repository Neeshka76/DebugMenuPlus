using ThunderRoad;
using UnityEngine;
using System.Collections;
using System;

namespace DebugMenuPlus
{
    class DebugMenuPlusLevelModule : LevelModule
    {
        private DebugMenuPlusController debugMenuPlusController;
        private Coroutine CoroutineCountBodies;
        private Coroutine CoroutineCountItems;
        private Coroutine CoroutineCleanBodies;
        private Coroutine CoroutineCleanItems;
        private Coroutine CoroutineScanItemsInfos;
        float timeToDespawnBodies = 4.0f;
        float timer = 0.0f;
        // When a level is loaded
        public override IEnumerator OnLoadCoroutine(Level level)
        {
            debugMenuPlusController = GameManager.local.gameObject.GetComponent<DebugMenuPlusController>();
            return base.OnLoadCoroutine(level);
        }
        // Update the location of the player
        public override void Update(Level level)
        {
            if (debugMenuPlusController == null)
            {
                debugMenuPlusController = GameManager.local.gameObject.GetComponent<DebugMenuPlusController>();
                return;
            }
            else
            {
                // Count Bodies & Items
                CoroutineCountBodies = GameManager.local.StartCoroutine(CountBodies());
                CoroutineCountItems = GameManager.local.StartCoroutine(CountItems());

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
                    CoroutineCleanBodies = GameManager.local.StartCoroutine(CleanBodies());
                }
                // Clear Items past a certain amount or manually
                if (debugMenuPlusController.data.CleanItemsGetSet == true || debugMenuPlusController.data.NbItemsInLevelGetSet >= debugMenuPlusController.data.NbItemsLimitValueInLevelGetSet)
                {
                    CoroutineCleanItems = GameManager.local.StartCoroutine(CleanItems());
                }

                if (debugMenuPlusController.data.TestInfoObjectGetSet == true)
                {
                    CoroutineScanItemsInfos = GameManager.local.StartCoroutine(ScanItemsInfos());
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
                for (int index = Creature.list.Count - 1; index >= 0; --index)
                {
                    if (Creature.list[index].state == Creature.State.Dead)
                    {
                        debugMenuPlusController.data.BodiesInLevelGetSet = true;
                        debugMenuPlusController.data.NbBodiesInLevelGetSet++;
                    }
                }
                yield return null;
            }

            IEnumerator CleanBodies()
            {
                for (int index = Creature.list.Count - 1; index >= 0; --index)
                {
                    if (Creature.list[index].state == Creature.State.Dead)
                    {
                        Creature.list[index].Despawn();
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
                for (int index = Item.list.Count - 1; index >= 0; --index)
                {
                    //if (Item.list.Count - 1 != -1)
                    if ((double)Item.list[index].spawnTime != 0.0
                                && !(bool)Item.list[index].holder
                                && (!Item.list[index].isTelekinesisGrabbed
                                && !Item.list[index].isThrowed)
                                && (!Item.list[index].IsHanded()
                                && !Item.list[index].disallowDespawn
                                && !Item.list[index].isGripped)
                                || (Item.list[index].itemId == "ModularTile" && Item.list[index].isThrowed == true)
                                || (Item.list[index].itemId == "DaggerCommon" && !Item.list[index].IsHanded() && !Item.list[index].isGripped && !Item.list[index].disallowDespawn && !(bool)Item.list[index].holder)
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
                for (int index = Item.list.Count - 1; index >= 0; --index)
                {
                    //if (Item.list.Count - 1 != -1)
                    if ((double)Item.list[index].spawnTime != 0.0
                            && !(bool)Item.list[index].holder
                            && (!Item.list[index].isTelekinesisGrabbed
                            && !Item.list[index].isThrowed)
                            && (!Item.list[index].IsHanded()
                            && !Item.list[index].disallowDespawn
                            && !Item.list[index].isGripped)
                            || (Item.list[index].itemId == "ModularTile" && Item.list[index].isThrowed == true)
                            || (Item.list[index].itemId == "DaggerCommon" && !Item.list[index].IsHanded() && !Item.list[index].isGripped && !Item.list[index].disallowDespawn && !(bool)Item.list[index].holder)
                            || (Item.list[index].itemId == "ModularPotion")
                            )
                    {
                        Item.list[index].Despawn();
                    }
                    else
                    {
                        debugMenuPlusController.data.ItemsInLevelGetSet = false;
                    }
                }
                debugMenuPlusController.data.CleanItemsGetSet = false;
                yield return null;
            }

            IEnumerator ScanItemsInfos()
            {
                for (int index = Item.list.Count - 1; index >= 0; --index)
                {
                    if (Item.list.Count - 1 != -1)
                    {
                        Debug.Log("DebugMenuPlus : ----------------------------------- : ");
                        Debug.Log("DebugMenuPlus : INDEX Number : " + index);
                        Debug.Log("DebugMenuPlus : Item Data Id : " + Item.list[index].data.id.ToString());
                        Debug.Log("DebugMenuPlus : Item Id : " + Item.list[index].itemId.ToString());
                        Debug.Log("DebugMenuPlus : Item Position : " + Item.list[index].transform.position.ToString());
                        Debug.Log("DebugMenuPlus : Item SpawnTime : " + Item.list[index].spawnTime.ToString());
                        Debug.Log("DebugMenuPlus : Item Author : " + Item.list[index].data.author.ToString());
                        Debug.Log("DebugMenuPlus : Item DisplayName : " + Item.list[index].data.displayName.ToString());
                        Debug.Log("DebugMenuPlus : Item handlerArmGrabbed : " + Item.list[index].handlerArmGrabbed.ToString());
                        //Debug.Log("DebugMenuPlus : Item holder : " + Item.list[index].holder.ToString());
                        Debug.Log("DebugMenuPlus : Item is TK Grabbed : " + Item.list[index].isTelekinesisGrabbed.ToString());
                        Debug.Log("DebugMenuPlus : Item is Throwned : " + Item.list[index].isThrowed.ToString());
                        Debug.Log("DebugMenuPlus : Item is handed : " + Item.list[index].IsHanded().ToString());
                        Debug.Log("DebugMenuPlus : Item is Disallow Despawn : " + Item.list[index].disallowDespawn.ToString());
                        Debug.Log("DebugMenuPlus : Item is gripped : " + Item.list[index].isGripped.ToString());
                        Debug.Log("DebugMenuPlus : Item is Flying : " + Item.list[index].isFlying.ToString());
                        Debug.Log("DebugMenuPlus : Item is Pooled : " + Item.list[index].isPooled.ToString());
                        Debug.Log("DebugMenuPlus : Item is Penetrating : " + Item.list[index].isPenetrating.ToString());
                        Debug.Log("DebugMenuPlus : Item is twoHanded : " + Item.list[index].IsTwoHanded().ToString());
                        Debug.Log("DebugMenuPlus : Item distant grab safe distance : " + Item.list[index].distantGrabSafeDistance.ToString());
                        Debug.Log("DebugMenuPlus : Item fly rotation speed : " + Item.list[index].flyRotationSpeed.ToString());
                        Debug.Log("DebugMenuPlus : Item fly ThrowAngle : " + Item.list[index].flyThrowAngle.ToString());
                        //Debug.Log("DebugMenuPlus : Item Last Handler : " + Item.list[index].lastHandler.ToString());
                        Debug.Log("DebugMenuPlus : Item Last interation time : " + Item.list[index].lastInteractionTime.ToString());
                        Debug.Log("DebugMenuPlus : Item Name : " + Item.list[index].name.ToString());
                        Debug.Log("DebugMenuPlus : Item Org Com : " + Item.list[index].orgCom.ToString());
                        Debug.Log("DebugMenuPlus : Item Org Mass : " + Item.list[index].orgMass.ToString());
                        Debug.Log("DebugMenuPlus : Item ParryPoint : " + Item.list[index].parryPoint.ToString());
                        Debug.Log("DebugMenuPlus : Item Snap Pitch Range : " + Item.list[index].snapPitchRange.ToString());
                        Debug.Log("DebugMenuPlus : Item use Custom Center of Mass : " + Item.list[index].useCustomCenterOfMass.ToString());
                        Debug.Log("DebugMenuPlus : Item Data Mass : " + Item.list[index].data.mass.ToString());
                        //Debug.Log("DebugMenuPlus : Item Data Prefab Address : " + Item.list[index].data.prefabAddress.ToString());
                        //Debug.Log("DebugMenuPlus : Item Data Prefab Location : " + Item.list[index].data.prefabLocation.ToString());
                        Debug.Log("DebugMenuPlus : Item Data Save Folder : " + Item.list[index].data.saveFolder.ToString());
                        Debug.Log("DebugMenuPlus : Item Data Slot : " + Item.list[index].data.slot.ToString());
                        Debug.Log("DebugMenuPlus : Item Data  : " + Item.list[index].GetInstanceID().ToString());
                        //Debug.Log("DebugMenuPlus : Item Data snap Audio Container Address : " + Item.list[index].data.snapAudioContainerAddress.ToString());
                        //Debug.Log("DebugMenuPlus : Item Data snap Audio Container Location : " + Item.list[index].data.snapAudioContainerLocation.ToString());
                    }
                }
                debugMenuPlusController.data.TestInfoObjectGetSet = false;
                yield return null;
            }
        }
        
    }
}

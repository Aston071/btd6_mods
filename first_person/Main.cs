﻿using MelonLoader;
using Harmony;
using Assets.Scripts.Unity.UI_New.InGame.Races;
using Assets.Scripts.Simulation.Towers.Weapons;
using Assets.Scripts.Simulation;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.Main;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Models.Towers;

using Assets.Scripts.Unity;



using Assets.Scripts.Simulation.Towers;

using Assets.Scripts.Utils;

using Il2CppSystem.Collections;
using Assets.Scripts.Unity.UI_New.Popups;
using System.Reflection;
using Assets.Scripts.Models;
using System.Collections.Generic;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.GenericBehaviors;
using System;
using System.Linq;
using Assets.Scripts.Models.ServerEvents;
using UnityEngine;
using Assets.Scripts.Unity.UI_New;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Unity.Bridge;
using Assets.Main.Scenes;

namespace first_person
{
    public class Main : MelonMod
    {



        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Console.WriteLine("first person loaded");
        }

        static GameObject cam;
        static bool following = false;
        static TowerToSimulation lastSelected = null;
        static float offset = 40;
        static float Yoffset = 0;

        [HarmonyPatch(typeof(TitleScreen), "Start")]
        public class Awake_Patch
        {

            [HarmonyPostfix]
            public static void Postfix()
            {
                foreach (var tower in Game.instance.model.towers)
                {
                    var n = tower.name.ToLower();
                    if (n.Contains("spike") || n.Contains("alch") || n.Contains("sniper"))
                        continue;
                    foreach (var bev in tower.behaviors)
                    {
                        try
                        {
                            var at = bev.Cast<AttackModel>();
                            at.fireWithoutTarget = true;
                            //at.AddBehaviour();
                        }
                        catch
                        {

                        }
                    }


                }


            }
        }



        public override void OnUpdate()
        {
            base.OnUpdate();

            //Input.GetAxis("Mouse X");
            //Console.WriteLine(Input.GetAxis("Mouse X"));
            //Console.WriteLine(Input.GetAxis("Horizontal"));

            bool inAGame = InGame.instance != null && InGame.instance.bridge != null;
            if (inAGame)
            {





                //var towers = InGame.instance.bridge.GetAllTowers();
                //if (towers.Count == 0) return;
                //if (towerID == towers.Count) towerID = 0;
                //var tower = towers[towerID];
                //InGame.instance.sceneCamera.transform.position = new UnityEngine.Vector3(tower.position.x, 5, tower.tower.Node.position.Y);
                //InGame.instance.sceneCamera.transform.position -= InGame.instance.sceneCamera.transform.forward * 0.1f;
                //var old = tower.tower.Node.rotationQuat.EulerAngles;
                //var eu = new Vector3(old.x, old.y, old.z);
                //InGame.instance.sceneCamera.transform.rotation.SetEulerAngles(eu);


                //DartlingMuzzleFlash
                if (following)
                {
                    if (InGame.instance.inputManager.SelectedTower != null)
                        lastSelected = InGame.instance.inputManager.SelectedTower;

                    if (lastSelected != null && lastSelected.tower != null)
                    {
                        //cam.transform.position = new Vector3(lastSelected.tower.Node.position.X, offset, lastSelected.tower.getPos().y+ Yoffset);
                        cam.transform.position = new Vector3(lastSelected.tower.Node.position.X, offset, (lastSelected.tower.Node.position.Y * -1));// + Yoffset
                        float eulerlol = lastSelected.tower.Rotation;
                        if (eulerlol >= 0)
                        {
                            eulerlol = 180 - eulerlol;
                        }
                        else
                        {
                            eulerlol = -180 - eulerlol;
                        }

                        cam.transform.rotation = Quaternion.Euler(0, eulerlol, 0);
                    }
                    else
                    {
                        cam.transform.position = new Vector3(0, 130, -90);
                        cam.transform.LookAt(new Vector3(0, 0, 0));
                    }

                }


                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F3))
                {
                    following = true;
                    GameObject.Find("Scene").GetComponent<Camera>().enabled = false;
                    if (cam == null)
                    {
                        cam = new GameObject();//GameObject.Instantiate(new GameObject(), new Vector3(5, 5, 5), Quaternion.identity);
                        cam.transform.position = new Vector3(0, 130, -90);
                        cam.transform.LookAt(new Vector3(0, 0, 0));
                        cam.AddComponent<Camera>();
                        cam.GetComponent<Camera>().orthographic = false;
                        cam.GetComponent<Camera>().fieldOfView = 110;
                        cam.name = "newcam";

                        //cam.tag = "MainCamera";
                        //InGame.instance.sceneCamera = cam.GetComponent<Camera>();
                        //GameObject.Find("Scene").GetComponent<Camera>().enabled = false;
                    }

                }
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F2))
                {
                    following = false;
                    //cam.transform.position = new Vector3(0, 130, -90);
                    //cam.transform.LookAt(new Vector3(0, 0, 0));
                    GameObject.Find("Scene").GetComponent<Camera>().enabled = true;
                }


                if (Input.GetKey(KeyCode.Alpha4))
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        offset += 0.12f;
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        Yoffset += 0.12f;
                    }
                    else
                    {
                        lastSelected.tower.RotateTower(0.02f, true);
                    }
                }

                if (Input.GetKey(KeyCode.Alpha5))
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        offset -= 0.12f;
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        Yoffset -= 0.12f;
                    }
                    else
                    {
                        lastSelected.tower.RotateTower(-0.02f, true);
                    }
                }

            }
        }









    }

}
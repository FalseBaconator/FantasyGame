using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{

    //GameManager
    int XP;
    //GameManager.Dungeon currentDungeon;

    //Map
    int currentMap;

    //UpgradeManager
    //Healer
    int healerHP;
    float healerCooldown;
    int healerAttack;
    int healerHeal;
    //Warrior
    int warriorHP;
    float warriorCooldown;
    int warriorShield;
    int warriorAttack;
    //Mage
    int mageHP;
    float mageCooldown;
    int mageFire;
    float mageSplash;
    int mageIce;
    float mageStun;
    //UpgradeStages
    public List<int> stages;


    //Distributes Data accross GameManager and UpgradeManager
    public void GetDataFromFile(GameManager gManager, UpgradeManager uManager)
    {
        gManager.XP = XP;
        gManager.mapGenerator.currentMap = currentMap;
        uManager.ReceiveData(healerHP, healerCooldown, healerAttack, healerHeal, warriorHP, warriorCooldown, warriorShield, warriorAttack, mageHP, mageCooldown, mageFire, mageIce, mageSplash, mageStun, stages.ToArray());
    }

    //Gets Data from GameManager and UpgradeManager
    public void GetDataFromGame(GameManager gManager, UpgradeManager uManager)
    {
        XP = gManager.XP;
        currentMap = gManager.mapGenerator.currentMap;

        healerHP = uManager.healerHP;
        healerCooldown = uManager.healerCooldown;
        healerAttack = uManager.healerAttack;
        healerHeal = uManager.healerHeal;

        warriorHP = uManager.warriorHP;
        warriorCooldown = uManager.warriorCooldown;
        warriorShield = uManager.warriorShield;
        warriorAttack = uManager.warriorAttack;

        mageHP = uManager.mageHP;
        mageCooldown = uManager.mageCooldown;
        mageFire = uManager.mageFire;
        mageSplash = uManager.mageSplash;
        mageIce = uManager.mageIce;
        mageStun = uManager.mageCool;

        stages = new List<int>();
        foreach (int item in uManager.upgradeStages)
        {
            stages.Add(item);
        }
    }


}

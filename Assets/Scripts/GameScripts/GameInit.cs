using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

    public MasterReferences master;

    public GameObject tutorialLevel;
    public List<GameObject> levels = new List<GameObject>();

    int level = 0;



    [Header("REFERENCES")]
    
    public GameObject IntroSelectionPrefab;
    public GameObject PlayerPrefab;
    public GameObject scoreGUI;

    [Header("INTERNAL REFERENCES")]

    GameObject player;
    MoveMotor playerMotor;





    void Start ()
    {
        // PlayerPrefs.SetInt("SkipTutorial", 1); // <<<<<<<<<<<<< !!!!!
      //  PlayerPrefs.DeleteKey("SkipTutorial"); // <<<<<<<<<<<<< !!!!!

        InitLevel();
    }

    public void InitLevel () {
        level = master.saveHandler.GetLevel();
        LoadMods();


        if (PlayerPrefs.HasKey("SkipTutorial"))
        {
            LoadSelection();
        }
        else
        {
            StartGame();
        }
	}

    void LoadMods ()
    {
        master.saveHandler.LoadMods();
    }

    void LoadSelection ()
    {
        GameObject introSelectionObj = Instantiate(IntroSelectionPrefab);
        SelectionHandler selectionHandler = introSelectionObj.GetComponent<SelectionHandler>();
        selectionHandler.modifiers = master.modifiers;
        selectionHandler.gameInit = this;
        selectionHandler.Init(level);
    }

    public void StartGame () //call this to start the game
    {
        LevelData levelData = LoadLevel();
        SpawnGame(levelData);

        SetCameraTarget(player.transform);
        playerMotor.On();
        master.countdown.StartCount();
        master.camMod.ModSettings();
        master.objectMod.ModSettings();

        scoreGUI.active = true;
    }

    LevelData LoadLevel()
    {


        GameObject levelObj;

        Debug.Log(level + " level!!");

        if (PlayerPrefs.HasKey("SkipTutorial"))
        {
            if (level == 1)
            {
                levelObj = Instantiate(levels[0]);
            }
            else
            {

                levelObj = Instantiate(levels[GetRandomLevel()]);
            }

        }
        else
        {
            Debug.Log("start tutorial");
            levelObj = Instantiate(tutorialLevel);
            PlayerPrefs.SetInt("SkipTutorial", 1);
            master.controls.isTutorial = true;
            master.countdown.gotWinPts = true;
        }

        levelObj.GetComponent<LevelMod>().master = master;
        levelObj.GetComponent<LevelMod>().RunMod();


        ModLevel(levelObj);

        return levelObj.GetComponent<LevelData>();
    }


    int GetRandomLevel ()
    {
        int choice = Random.Range(1, levels.Count);
        string levelsPlayed = PlayerPrefs.GetString("LevelsPlayed"); 
        if (levelsPlayed.Contains(""+choice))
        {
            choice = GetRandomLevel();
        }
        PlayerPrefs.SetString("LevelsPlayed", PlayerPrefs.GetString("LevelsPlayed") + choice);
        return choice;
    }

    void SpawnGame(LevelData levelData)
    {

        SetupEnemySpawn(levelData);
        SpawnPlayer(levelData);
        SpawnNPC(levelData);
        SpawnBuildings(levelData);
        SpawnSceneObjects(levelData);
    }

    void SpawnPlayer (LevelData levelData)
    {
        player = Instantiate(PlayerPrefab, levelData.playerSpawn);
        playerMotor = player.GetComponent<MoveMotor>();
        playerMotor.master = master;

        player.transform.parent = levelData.transform;

        master.player = player;

        Destroy(levelData.playerSpawn.gameObject);
    }

    void SpawnNPC (LevelData levelData)
    {
        
        int maxAllies = master.saveHandler.GetAllies();
        int maxNonCon = master.controls.NPCCount - maxAllies;

        Debug.Log(maxNonCon + " max");

        int allyCount = 0;
        int nonConCount = 0;


        if (levelData.isTutorial) { maxAllies = 0; maxNonCon = 22; } 

        while (allyCount < maxAllies || nonConCount < maxNonCon) //keep looping until all allies and noncons are in places
        {
            foreach (LevelData.NPCSpawn npcSpawn in levelData.npcSpawn)
            {
                foreach (Transform child in levelData.transform)
                {
                    if (child.name.Contains(npcSpawn.keyTerm))
                    {
                        NPCHandler.NPCMode npcMode = npcSpawn.mode;

                        if (npcMode == NPCHandler.NPCMode.NONCON)
                        {
                          //  Debug.Log("checking: " + nonConCount + " .... " + maxNonCon);
                        }

                        if (npcMode == NPCHandler.NPCMode.ALLY && allyCount < maxAllies ||
                            npcMode == NPCHandler.NPCMode.NONCON && nonConCount < maxNonCon)
                        {
                            GameObject npc = Instantiate(npcSpawn.NPCPrefab, child);

                            npc.transform.parent = levelData.transform;
                            npc.name = "NPC [" + npcSpawn.keyTerm.Replace("Spawn", "") + "]";

                            NPCHandler npcHandler = npc.GetComponent<NPCHandler>();
                            npcHandler.master = master;
                            npcHandler.SetMode(npcSpawn.mode);

                            MoveMotor motor = npc.GetComponent<MoveMotor>();
                            motor.master = master;

                            if (npcMode == NPCHandler.NPCMode.ALLY)
                            {
                                allyCount++;
                               // Debug.Log("add ally111");
                            }
                            else if (npcMode == NPCHandler.NPCMode.NONCON)
                            {
                                nonConCount++;
                            }
                        }

                    }
                }
            }
            

        }



        foreach (LevelData.NPCSpawn npcSpawn in levelData.npcSpawn)
        {
            foreach (Transform child in levelData.transform)
            {
                if (child.name.Contains(npcSpawn.keyTerm))
                {
                    Destroy(child.gameObject);
                }
            }
        }
     }

    void SpawnBuildings(LevelData levelData)
    {
        foreach (LevelData.Building building in levelData.buildings)
        {
            foreach (Transform child in levelData.transform)
            {
                if (child.name.Contains(building.keyTerm))
                {
                    GameObject buildingObj = Instantiate(building.buildingPrefab, child);
                    buildingObj.transform.parent = levelData.transform;
                    Destroy(child.gameObject);
                }
            }

        }

    }
    
    void SpawnSceneObjects(LevelData levelData)
    {
        foreach (LevelData.SceneObjects sceneObject in levelData.sceneObjects)
        {
            foreach (Transform child in levelData.transform)
            {
                if (child.name.Contains(sceneObject.keyTerm))
                {
                    GameObject sceneObj = Instantiate(sceneObject.sceneObjectPrefab, child);
                    sceneObj.transform.parent = levelData.transform;
                    Destroy(child.gameObject);
                }
            }
        }
    }


    void SetupEnemySpawn (LevelData levelData)
    {
        List<Transform> enemySpawnPoints = new List<Transform>();
        foreach (Transform child in levelData.transform)
        {
            if (child.name.Contains(levelData.enemySpawnKey))
            {
                enemySpawnPoints.Add(child);
            }
        }
        master.spawnEnemies.levelData = levelData;
        master.spawnEnemies.spawnPoints = enemySpawnPoints;
        master.spawnEnemies.difficulty = levelData.difficultyCurve;
        master.spawnEnemies.enemyTimeIncrease = levelData.enemyTimeIncrease;
        master.spawnEnemies.gameTimeEnemyMax = levelData.enemyTimeMax;
        master.spawnEnemies.spawnInterval = levelData.spawnInterval;
        master.spawnEnemies.StartSpawn();
    }

    void SetCameraTarget (Transform target)
    {
        master.camFollow.SetTarget(target);
    }



    void ModLevel (GameObject levelObj)
    {
        foreach (Modifiers.Modifier mod in master.modifiers.mods)
        {
            ModSettings(mod, levelObj);
        }
    }

    void ModSettings(Modifiers.Modifier mod, GameObject levelObj)
    {
        switch (mod)
        {
            case Modifiers.Modifier.SLIPPERY:
                Mod_Slippery(levelObj);
                break;
        }
    }

    void Mod_Slippery(GameObject levelObj)
    {
        Renderer[] renders = levelObj.GetComponentsInChildren<Renderer>();
        foreach (Renderer render in renders)
        {
            for (int i = 0; i < render.materials.Length; i++)
            {
                Color renderCol = render.materials[i].color;
             //   renderCol.r -= 0.1f;
                renderCol.b += 0.6f;
                renderCol.g += 0.2f;
                render.materials[i].color = renderCol;

            }
        }
    }

}

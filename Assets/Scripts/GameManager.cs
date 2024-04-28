using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UserFarm
{
    public int id;
    public int uid;
    public int type;
    public int level;
    public int size;
    public string farm_name;
}

[Serializable]
public class FarmData
{
    public string type;
    public int id;
    public int size;
    public List<ZoneData> zones;
}

[Serializable]
public class ZoneData
{
    public string type;
    public int id;
    public int biome;
    public List<PlantData> plants;
}

[Serializable]
public class PlantData
{
    public string type;
    public int id;
    public string plantName;
    public int plantType;
    public int position;
    public int growthStage;
    public int growthPoint;
    public int waterPoint;
}


public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public List<GameObject> userFarmList;
    public Transform farmParent;
    public List<GameObject> farmPrefabs;
    public List<GameObject> plantPrefabs;
    public List<Material> biomeMaterials;

    private void Start()
    {
        userFarmList = new List<GameObject>();
    }

    public void ShowFarmList(int userId)
    {
        StartCoroutine(NetworkManager.Instance.Get(EndPoint.GetFarm, null, (response) => {
            ResponseBody<List<UserFarm>> userFarms = JsonUtility.FromJson<ResponseBody<List<UserFarm>>>(response);

            foreach (UserFarm userFarm in userFarms.data)
            {
                LoadFarm(userFarm.id);
            }
        }));
    }

    public void LoadFarm(int farmId)
    {
        StartCoroutine(NetworkManager.Instance.Get(EndPoint.LoadFarm, "farmId="+ farmId, (response) => {
            ResponseBody<FarmData> farmData = JsonUtility.FromJson<ResponseBody<FarmData>>(response);

            GameObject playerFarm = Instantiate(farmPrefabs[farmData.data.size], farmParent);
            userFarmList.Add(playerFarm);

            playerFarm.transform.position = new Vector3(100f * userFarmList.Count - 1, 0f, 100f * userFarmList.Count - 1);

            playerFarm.GetComponent<Farm>().id = farmData.data.id;
            playerFarm.GetComponent<Farm>().size = farmData.data.size;

            for(int i = 0; i < 3; i++)
            {
                playerFarm.GetComponent<Farm>().zones[i].biome = (Biome)farmData.data.zones[i].biome;
                playerFarm.GetComponent<Farm>().zones[i].SetBiome();

                List<PlantData> plantDataList = farmData.data.zones[i].plants;

                foreach(PlantData plantData in plantDataList)
                {
                    GameObject newPlant = Instantiate(plantPrefabs[plantData.plantType], playerFarm.GetComponent<Farm>().zones[i].cropCubes[plantData.position]);
                    playerFarm.GetComponent<Farm>().zones[i].cropCubes[plantData.position].GetComponent<CropCube>().plant = newPlant;

                    newPlant.GetComponent<Plant>().id = plantData.id;
                    newPlant.GetComponent<Plant>().plantType = plantData.plantType;
                    newPlant.GetComponent<Plant>().plantName = plantData.plantName;
                    newPlant.GetComponent<Plant>().growthStage = plantData.growthStage;
                    newPlant.GetComponent<Plant>().growthPoint = plantData.growthPoint;
                    newPlant.GetComponent<Plant>().waterPoint = plantData.waterPoint;
                }
            }

            playerFarm.GetComponent<Farm>().SpawnFarm();
        }));
    }
}

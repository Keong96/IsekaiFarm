using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Farm : MonoBehaviour
{
    public int id;
    public int size;
    public List<Zone> zones;
    public GameObject baseZone;
    
    public void SpawnFarm()
    {
        zones[0].transform.position += new Vector3(0f, 40f, 0f);
        zones[1].transform.position += new Vector3(0f, 40f, 0f);
        zones[2].transform.position += new Vector3(0f, 40f, 0f);
        zones[3].transform.position += new Vector3(0f, 40f, 0f);

        baseZone.transform.localScale = Vector3.zero;
        baseZone.transform.DOScale(1f, 1.25f);

        for (int i = 0; i < 4; i++)
        {
            float random = Random.Range(1.25f, 2.5f);
            zones[i].transform.DOLocalMoveY(0f, random);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public Biome biome;
    public List<Transform> cropCubes;

    void Start()
    {

    }

    public void SetBiome()
    {
        List<Material> mats = GameManager.Instance.biomeMaterials;

        foreach (Transform t in cropCubes)
        {
            t.GetComponent<CropCube>().top.GetComponent<MeshRenderer>().material = mats[(int)biome];
            t.GetComponent<CropCube>().bottom.GetComponent<MeshRenderer>().material = mats[(int)biome];
        }
    }
    /*

    public void SetProp()
    {
        List<Transform> temp = new List<Transform>();
        temp.AddRange(cropCubes);

        int randomTree = Random.Range(0, temp.Count);
        temp.RemoveAt(randomTree);
        Instantiate(Farm.Instance.props[0], cropCubes[randomTree].GetComponent<CropCube>().top.transform);

        int randomRock = Random.Range(0, temp.Count);
        temp.RemoveAt(randomRock);
        Instantiate(Farm.Instance.props[1], cropCubes[randomRock].GetComponent<CropCube>().top.transform);

        int randomMineCave = Random.Range(0, temp.Count);
        temp.RemoveAt(randomMineCave);
        Instantiate(Farm.Instance.props[2], cropCubes[randomMineCave].GetComponent<CropCube>().top.transform);

        int randomWell = Random.Range(0, temp.Count);
        temp.RemoveAt(randomWell);
        Instantiate(Farm.Instance.props[3], cropCubes[randomWell].GetComponent<CropCube>().top.transform);
    }*/
}

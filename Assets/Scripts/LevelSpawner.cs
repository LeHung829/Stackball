using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] model;
    [HideInInspector]
    public GameObject[] modelPerfab = new GameObject[4];
    public GameObject winPerfab;

    private GameObject temp1, temp2;

    public int level = 1, addOn = 7;
    float i = 0;

    public Material plateMat, baseMat;
    public MeshRenderer ballMesh;

    void Awake()
    {
        plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
        baseMat.color = plateMat.color * Color.gray;
        ballMesh.material.color = plateMat.color;

        level = PlayerPrefs.GetInt("Level", 1);

        if (level > 9)
            addOn = 0;

        modelSelection();
        float random = Random.value;
        for (i = 0; i > - level - addOn; i -= 0.5f)
        {
            if (level <= 20)
            {
                temp1 = Instantiate(modelPerfab[Random.Range(0, 2)]);
            }
            if (level > 20 && level <= 50)
            {
                temp1 = Instantiate(modelPerfab[Random.Range(1, 3)]);
            }
            if (level > 50 && level <= 100)
            {
                temp1 = Instantiate(modelPerfab[Random.Range(2, 4)]);
            }
            if (level > 100)
            {
                temp1 = Instantiate(modelPerfab[Random.Range(3, 4)]);
            }

            temp1.transform.position = new Vector3(0, i - 0.01f, 0);
            temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);

            if (Mathf.Abs(i) >= level * 0.3f && Mathf.Abs(i) <= level * 0.6f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                temp1.transform.eulerAngles += Vector3.up * 100;
            }
            else if (Mathf.Abs(i) >= level * 0.8f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);

                if (random > 0.75f)
                    temp1.transform.eulerAngles += Vector3.up * 100;
            }

            temp1.transform.parent = FindObjectOfType<Rotator>().transform;
        }

        temp2 = Instantiate(winPerfab);
        temp2.transform.position = new Vector3(0, i - 0.01f, 0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            baseMat.color = plateMat.color * Color.gray;
            ballMesh.material.color = plateMat.color;
        }
    }

    void modelSelection()
    {
        int randomModel = Random.Range(0, 5);

        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    modelPerfab[i] = model[i];
                }
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    modelPerfab[i] = model[i + 4];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    modelPerfab[i] = model[i + 8];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    modelPerfab[i] = model[i + 12];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    modelPerfab[i] = model[i + 16];
                }
                break;
        }
    }

    public void nextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }
}

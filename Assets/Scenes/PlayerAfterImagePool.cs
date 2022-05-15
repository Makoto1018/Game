using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    public static PlayerAfterImagePool Instance;

    public GameObject AfterImagePrefab;

    public int AfterImageCount=5;

    private Queue<GameObject> AvailableObjects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        //初始化對象池
        FillPool();

    }

 
    private void FillPool()//填充對象池
    {
        for(int i = 0; i < AfterImageCount; i++)
        {
            var NewAfterImage = Instantiate(AfterImagePrefab);
            NewAfterImage.transform.SetParent(transform);

            ReturnPool(NewAfterImage);
        }
    }
    public void ReturnPool(GameObject gameObject)//返回對象池
    {
        gameObject.SetActive(false);

        AvailableObjects.Enqueue(gameObject);
    }
    public GameObject GetFromPool()
    {
        if (AvailableObjects.Count == 0)
        {
            FillPool();
        }
        var OutAfterImage = AvailableObjects.Dequeue();

        OutAfterImage.SetActive(true);

        return OutAfterImage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterimageSprite : MonoBehaviour
{
    //取得玩家數據
    private Transform Player;
    //取得圖像
    private SpriteRenderer ThisSR;
    private SpriteRenderer PlayerSR;
    //顏色參數
    private Color color;
    //時間控制
    public float ActiveTime;
    private float StartTime;
    //顏色控制
    private float Alpha;
    public float AlphaSet;//初始值
    public float AlphaMultiplier;

    private void OnEnable()
    {
        //獲得物件
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        ThisSR = GetComponent<SpriteRenderer>();
        PlayerSR = Player.GetComponent<SpriteRenderer>();
        //設置透明度
        Alpha = AlphaSet;
        //
        ThisSR.sprite = PlayerSR.sprite;
        //獲得玩家位置等資訊
        transform.position = Player.position;
        transform.localScale = Player.localScale;
        transform.rotation = Player.rotation;
        //設定時間參數
        StartTime = Time.time;

    }

    void Update()
    {
        Alpha *= AlphaMultiplier;

        color = new Color(0, 0, 1, Alpha);

        ThisSR.color = color;

        if (Time.time > StartTime + ActiveTime)
        {
            //返回對象池
            PlayerAfterImagePool.Instance.ReturnPool(this.gameObject);
        }
    }
}

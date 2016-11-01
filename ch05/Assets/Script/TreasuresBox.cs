using UnityEngine;
using System.Collections;

public class TreasuresBox : MonoBehaviour {

	public GameObject Prompt;
	public SpriteRenderer sprite;
	public Sprite Open;
	public bool Up_Active = false;
	public float Alpha = 0.0f;
	float a = -0.01f;

	public bool isOpen;
	void Awake()
	{
		//尋找名為"Up"的物件並儲存在變數
		Prompt = GameObject.Find("Up");
		//將Up物件的激活狀態改變為關閉
		Prompt.gameObject.SetActive(Up_Active);
		//抓取Up物件的SpriteRenderer元件
		sprite = Prompt.GetComponent<SpriteRenderer>();

	}

	void Update () {

		if(isOpen)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = Open;
			Prompt.gameObject.SetActive(false);
		}
		else
		{
			//如果Up物件激活的話執行
			if(Up_Active)
			{
				//讓Alpha值不斷改變,並取代材質原本的Color
				Alpha += a;
				sprite.material.color = new Color(1f,1f,1f,Alpha);
				
				//若Alpha大於1.0f就將a變為負的,若小於0.0f就變為正的
				if(Alpha >= 1.0f)
				{
					a = -0.01f;
				}
				if(Alpha <= 0.0f)
				{
					a = 0.01f;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.name == "Player")
		{
			//改變Up的激活狀態為開啟
			Up_Active = true;
			Prompt.gameObject.SetActive(Up_Active);

		}
	}
	void OnTriggerExit2D(Collider2D c)
	{
		if(c.gameObject.name == "Player")
		{
			//改變Up的激活狀態為關閉,並將Alpha歸零
			Up_Active = false;
			Prompt.gameObject.SetActive(Up_Active);
			Alpha = 0.0f;
		}
	}

}

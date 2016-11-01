using UnityEngine;
using System.Collections;

public class Player_Control : MonoBehaviour {

	Animator Main_Animator;
	Transform Player;

	public bool Walk,Jump,Injured;

	public float MoveSpeed;

	void Start () {
		//取得玩家物件本身
		Player = gameObject.transform;
		//取得動畫控制元件
		Main_Animator = Player.GetComponent<Animator>();
		//設定移動速度值為10
		MoveSpeed = 10f;
	}

	void Update () {
		//跟動畫控制器的變數做連結
		Main_Animator.SetBool("Walk",Walk);
		Main_Animator.SetBool("Jump",Jump);
		Main_Animator.SetBool("Injured",Injured);

		//按鍵控制(左)
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			//物件水平旋轉180度
			Player.eulerAngles =  Vector3.zero;
			//物件右邊方向移動
			Player.Translate(-MoveSpeed * Time.deltaTime,0,0);
			//開啟移動動畫
			Walk = true;
		}
		//按鍵控制(右)
		if(Input.GetKey(KeyCode.RightArrow))
		{
			//物件旋轉值歸零
			Player.eulerAngles =new Vector3(0,180,0);
			//物件右邊方向移動
			Player.Translate(-MoveSpeed * Time.deltaTime,0,0);
			//開啟移動動畫
			Walk = true;
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow )|| Input.GetKeyUp(KeyCode.RightArrow))
		{
			//向左跟向右的動畫都是走路,兩個任一個放開按鍵後都會關閉動畫
			Walk = false;
		}
		//按鍵控制(空白键)
		if(Input.GetKeyDown(KeyCode.Space))
		{
			//先判斷有沒有跳躍過,防止連續跳躍
			if(Jump == false)
			{
				//使用Rigidbody增加力的函式,向上
				transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
				//開啟跳躍動畫
				Jump = true;
			}
		}
	}


	void OnCollisionEnter2D(Collision2D c)
	{
		//判斷有沒有碰到地板
		if(c.gameObject.name == "Floor")
		{
			//跳躍條件關閉,就可以再進行跳躍
			Jump = false;
		}
	}

	void OnCollisionExit2D()
	{
	}


	void OnTriggerStay2D(Collider2D c)
	{
		if(c.gameObject.tag == "Trap")
		{
			//取得現在的時間
			float time = Time.time;
			//每次時間的絕對值除以2等於0時執行
			if(Mathf.Abs(time) % 2 == 0)
			{
				//開啟受傷動畫
				print ("好痛!我受傷了!!");
				Injured = true;
			}

		}
		if(c.gameObject.name == "TreasuresBox")
		{
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				Debug.Log("恭喜你獲得寶藏!!!!~~~");
				c.gameObject.GetComponent<TreasuresBox>().isOpen = true;
			}
		}
	}

	void OnTriggerExit2D()
	{
		Injured = false;
	}
}

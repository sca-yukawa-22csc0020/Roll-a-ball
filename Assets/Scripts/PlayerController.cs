using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed=20;//動く速さ
    public Text scoreText;//スコアの
    public Text WinText;//リザルトのUI

    private Rigidbody rb;//Rididbody
    private int score;//スコア

    private int upForce;
    private bool isGround;

    void Start()
    {
      //Rigidbodyを取得
      rb=GetComponent<Rigidbody>();
        upForce = 200;

        //UIを初期化
        score =0;
        SetCountText();
        WinText.text="";
    }

    // Update is called once per frame
    void Update()
    {
     //カーソルキーの入力を取得
     var moveHorizontal=Input.GetAxis("Horizontal");
     var moveVertical=Input.GetAxis("Vertical");
        if (Input.GetKey("space") && isGround)
            rb.AddForce(new Vector3(0, upForce, 0));

        //カーソルキーの入力に合わせて移動方向を設定
        var movement=new Vector3(moveHorizontal,2,moveVertical);

     //Ridigbodyに力を与えて球を動かす
     rb.AddForce(movement*speed);

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    //球がほかのオブジェクトにぶつかった時に呼び出される
     void OnTriggerEnter(Collider other)
    {
        //ぶつかったオブジェクトが収穫アイテムだった場合
        if(other.gameObject.CompareTag("Pick Up"))
        {
            //その収穫アイテムを非表示にします
            other.gameObject.SetActive(false);

            //スコアの加算
            score=score+1;

            //UIの表示を更新
            SetCountText();
        }
    }
    //UIの表示を更新
    void SetCountText()
    {
        //スコアの表示を更新
        scoreText.text="Count:"+score.ToString();

        //すべての収集アイテムを獲得した場合
        if (score >= 5)
        {
            //リザルトの表示を更新
            WinText.text="You Win!";
            Time.timeScale = 0.0f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Plane")
            isGround = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Plane")
            isGround = false;
    }
}

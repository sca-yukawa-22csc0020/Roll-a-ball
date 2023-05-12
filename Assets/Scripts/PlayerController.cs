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
    [SerializeField] float rotSpeed;

    void Start()
    {
      //Rigidbodyを取得
      rb=GetComponent<Rigidbody>();
        upForce = 20;

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
        var forwardDir = Camera.main.transform.forward * moveVertical;
        var rightDir = Camera.main.transform.right * moveHorizontal;
        var movement = (forwardDir + rightDir).normalized;
        if (Input.GetKey(KeyCode.Space) && isGround)
            {
                rb.AddForce(new Vector3(0, upForce, 0));
            }

        var rot = 0.0f;
        if (Input.GetKey(KeyCode.J))
        {
            rot = -1.0f;
        }else if (Input.GetKey(KeyCode.K))
        {
            rot = 1.0f;
        }
        transform.Rotate(new Vector3(0,rot,0) * rotSpeed * Time.deltaTime);

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
        if (score >= 7)
        {
            //リザルトの表示を更新
            WinText.text="You Win!";
            Time.timeScale = 0.0f;
       }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGround = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGround = false;
    }
}

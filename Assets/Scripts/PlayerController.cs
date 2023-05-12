using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed=20;//��������
    public Text scoreText;//�X�R�A��
    public Text WinText;//���U���g��UI

    private Rigidbody rb;//Rididbody
    private int score;//�X�R�A

    private int upForce;
    private bool isGround;
    [SerializeField] float rotSpeed;

    void Start()
    {
      //Rigidbody���擾
      rb=GetComponent<Rigidbody>();
        upForce = 20;

        //UI��������
        score =0;
        SetCountText();
        WinText.text="";
    }

    // Update is called once per frame
    void Update()
    {
        //�J�[�\���L�[�̓��͂��擾
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

         //Ridigbody�ɗ͂�^���ċ��𓮂���
        rb.AddForce(movement*speed);

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }



    //�����ق��̃I�u�W�F�N�g�ɂԂ��������ɌĂяo�����
     void OnTriggerEnter(Collider other)
    {
        //�Ԃ������I�u�W�F�N�g�����n�A�C�e���������ꍇ
        if(other.gameObject.CompareTag("Pick Up"))
        {
            //���̎��n�A�C�e�����\���ɂ��܂�
            other.gameObject.SetActive(false);

            //�X�R�A�̉��Z
            score=score+1;

            //UI�̕\�����X�V
            SetCountText();
        }
    }
    //UI�̕\�����X�V
    void SetCountText()
    {
        //�X�R�A�̕\�����X�V
        scoreText.text="Count:"+score.ToString();

        //���ׂĂ̎��W�A�C�e�����l�������ꍇ
        if (score >= 7)
        {
            //���U���g�̕\�����X�V
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

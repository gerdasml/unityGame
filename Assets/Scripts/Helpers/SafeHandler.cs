using UnityEngine;
using System.Collections;

public class SafeHandler : MonoBehaviour
{

    public string input;
    public string code = "1212";
    public GameObject gameObj;
    public int maxString = 4;
    public UnityEngine.UI.Text Text;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject button0;
    public Transform handle;
    public Transform handleTarget;
    public Transform door;
    public Transform doorTarget;
    public AudioClip myClip;

    void Start()
    {
        code = Config.Instance.Puzzle.BoatBoard.Answer;
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {


            input = input + "1";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button1.GetComponent<Renderer>().material.color = Color.red;
            de1();
        }
        if (Input.GetKeyDown("3"))
        {


            input = input + "3";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button3.GetComponent<Renderer>().material.color = Color.red;
            de3();

        }
        if (Input.GetKeyDown("4"))
        {


            input = input + "4";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button4.GetComponent<Renderer>().material.color = Color.red;
            de4();

        }
        if (Input.GetKeyDown("5"))
        {


            input = input + "5";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button5.GetComponent<Renderer>().material.color = Color.red;
            de5();

        }
        if (Input.GetKeyDown("6"))
        {


            input = input + "6";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button6.GetComponent<Renderer>().material.color = Color.red;
            de6();

        }
        if (Input.GetKeyDown("7"))
        {


            input = input + "7";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button7.GetComponent<Renderer>().material.color = Color.red;
            de7();

        }
        if (Input.GetKeyDown("8"))
        {


            input = input + "8";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button8.GetComponent<Renderer>().material.color = Color.red;
            de8();

        }
        if (Input.GetKeyDown("9"))
        {


            input = input + "9";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button9.GetComponent<Renderer>().material.color = Color.red;
            de9();

        }
        if (Input.GetKeyDown("0"))
        {


            input = input + "0";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button0.GetComponent<Renderer>().material.color = Color.red;
            de0();

        }
        if (Input.GetKeyDown("2"))
        {

            input = input + "2";
            GetComponent<AudioSource>().PlayOneShot(myClip);
            button2.GetComponent<Renderer>().material.color = Color.red;
            de2();

        }
    }
    void FixedUpdate()
    {

        if (input == code)
        {


            gameObj.SetActive(true);
            Text.color = Color.green;
            open();

        }
        if (input.Length > maxString)
        {


            input = "";

        }
        Text.text = input;
    }
    IEnumerator de1()
    {

        yield return new WaitForSeconds(0.1f);
        button1.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de2()
    {

        yield return new WaitForSeconds(0.1f);
        button2.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de3()
    {

        yield return new WaitForSeconds(0.1f);
        button3.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de4()
    {

        yield return new WaitForSeconds(0.1f);
        button4.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de5()
    {

        yield return new WaitForSeconds(0.1f);
        button5.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de6()
    {

        yield return new WaitForSeconds(0.1f);
        button6.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de7()
    {

        yield return new WaitForSeconds(0.1f);
        button7.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de8()
    {

        yield return new WaitForSeconds(0.1f);
        button8.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de9()
    {

        yield return new WaitForSeconds(0.1f);
        button9.GetComponent<Renderer>().material.color = Color.white;

    }
    IEnumerator de0()
    {

        yield return new WaitForSeconds(0.1f);
        button0.GetComponent<Renderer>().material.color = Color.white;

    }
    void open()
    {
        handle.transform.rotation = new Quaternion(
            handle.transform.rotation.x,
            handle.transform.rotation.y,
            handleTarget.transform.rotation.z,
            handle.transform.rotation.w
        );

        doorOpen();

    }
    void doorOpen()
    {

        //yield return new WaitForSeconds(1);
        door.transform.rotation = new Quaternion(
            door.transform.rotation.x,
            doorTarget.transform.rotation.y,
            door.transform.rotation.z,
            door.transform.rotation.w
        );
        Destroy(Text);

    }




}
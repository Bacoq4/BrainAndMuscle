using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    MeshFilter meshFilter;
    public TextMeshProUGUI LevelText;
    Camera camera;

    public GameObject[] Objects2Spawn;
    int SpawnIndex = 0;

    Rigidbody Rb;

    public static bool isStruck = false;

    public Transform PyramidTarget;

    public static bool isFinish;
    public GameObject FinishCube;

    public int Health;
    public HealthBar healthBar;

    bool b = false;
    bool b2 = false;
    bool b3 = false;
    bool b4 = false;

    string[] names = {"Skeleton" ,"Mummy" ,"Pharaoh" };

    public int GatheredBandageNumber = 0;
    int LastBandageNumber = 0;

    public Transform BandageParticle;

    bool isGameOver = false;
    public GameObject RetryButton;
    

// skeleton mummy pharaoh

    public void OnGameOver()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Awake() {
        Rb = GetComponent<Rigidbody>();

        meshFilter = GetComponent<MeshFilter>();

        LevelText.text = names[0];

        isFinish = false;

        Health = 0;
        healthBar.SetMaxHealth(100);
        healthBar.SetHealth(0);

        camera = Camera.main;
    }

    private void Update() {
        if (isFinish)
        {
            ClimbPyramid();
            if (Vector3.Distance(transform.position, PyramidTarget.position) < 0.1f && !b)
            {
                b = true;
                camera.GetComponent<CameraFollow>().diff += new Vector3(-10, -5, 5);            camera.transform.eulerAngles += new Vector3(0, -40, 0);
                camera.transform.eulerAngles += new Vector3(0, 80, 0);
                GetComponent<Runner>().animator.SetTrigger("dance");
            }
        }
        if (GatheredBandageNumber <= -5)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("bandage"))
        {
            Instantiate(BandageParticle,transform.position+new Vector3(0,5,0),Quaternion.identity);
            Health += 10;
            GatheredBandageNumber++;
            healthBar.SetHealth(Health);
            if (Health > 100)
            {
                Health = 100;
            }

            if (SpawnIndex != 2 && Health >= 100)
            {
                Health = 0;
                healthBar.SetHealth(0);
                SpawnIndex++;
                LevelText.text = names[SpawnIndex];
                SetActiveOneModel(SpawnIndex);
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("trap") || other.CompareTag("fire"))
        {
            if (other.CompareTag("fire"))
            {
                GatheredBandageNumber -= 1;
                Health -= 10;
                Destroy(other.gameObject);
            }
            else
            {
                GatheredBandageNumber -= 3;
                Health -= 30;
            }
            
            if (Health < 0)
            {
                Health = 0;
            }
            healthBar.SetHealth(Health);
            if (SpawnIndex != 0 && Health <= 0)
            {
                Health = 100;
                healthBar.SetHealth(100);
                SpawnIndex--;
                LevelText.text = names[SpawnIndex];
                SetActiveOneModel(SpawnIndex);
            }
            
        }

        if (other.CompareTag("finish"))
        {
            SetActiveOneModel(0);
            GetComponent<Runner>().animator.SetTrigger("climb");

            isFinish = true;
            StartCoroutine(SpawnCubes());
        }
        
    }

    private void GameOver()
    {
        isGameOver = true;
        RetryButton.SetActive(true);
        Runner runner = GetComponent<Runner>();
        runner.Zspeed = 0;
        runner.Xspeed = 0;
        if (!b4)
        {
            b4 = true;
            runner.animator.SetTrigger("idle");
        }
        
    }

    void ClimbPyramid()
    {
        if (!b2)
        {
            b2 = true;
            camera.GetComponent<CameraFollow>().diff += new Vector3(10, 5, -5);
            camera.transform.eulerAngles += new Vector3(0, -40, 0);
            GatheredBandageNumber+=10;
            LastBandageNumber = GatheredBandageNumber;
        }
        if (SpawnIndex != 2 && Health >= 100)
        {
            Health = 0;
            healthBar.SetHealth(0);
            SpawnIndex++;
            LevelText.text = names[SpawnIndex];
            //SetActiveOneModel(SpawnIndex);
        }
        if (SpawnIndex != 0 && Health <= 0)
        {
            Health = 100;
            healthBar.SetHealth(100);
            SpawnIndex--;
            LevelText.text = names[SpawnIndex];
            //SetActiveOneModel(SpawnIndex);
        }
        if (GatheredBandageNumber > 0 || LastBandageNumber >= 40)
        {
            float step = Time.deltaTime * 15f;
            transform.position = Vector3.MoveTowards(transform.position, PyramidTarget.position, step);
        }
        else
        {
            if (!b3)
            {
                b3 = true;
                GetComponent<Runner>().animator.SetTrigger("dance");
            }
        }
        
    }

    IEnumerator SpawnCubes(){
        while (Vector3.Distance(transform.position , PyramidTarget.position) > 0.1f && GatheredBandageNumber > 0 || LastBandageNumber >= 40)
        {
            GatheredBandageNumber--;
            Health-=2;
            
            healthBar.SetHealth(Health);
            Instantiate(FinishCube, transform.position - new Vector3(0, 0.1f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        } 
    }
    private void ChangeSharedMesh()
    {
        MeshFilter LoadedMeshFilter = Objects2Spawn[SpawnIndex].GetComponent<MeshFilter>();

        meshFilter.sharedMesh = LoadedMeshFilter.sharedMesh;
    }

    private void SetActiveOneModel(int x)
    {
        for (int i = 0; i < Objects2Spawn.Length; i++)
        {
            Objects2Spawn[i].SetActive(false);
            Objects2Spawn[x].SetActive(true);
        }
    }
}

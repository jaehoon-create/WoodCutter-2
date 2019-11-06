using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public enum TYPE
    { // 아이템 종류.
        NONE = -1, // 없음.
        IRON = 0,
        APPLE,
        PLANT,
        NUM, // 아이템이 몇 종류인가 나타낸다
    };
};

public class ItemRoot : MonoBehaviour
{


    float time = 0f;
    private Slider gage;
    private Slider toilet;
    public GameObject mineral = null;
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    // 아이템의 종류를 Item.TYPE형으로 반환하는 메소드.
    public GameObject ironPrefab = null; // Prefab 'Iron'
    public GameObject plantPrefab = null; // Prefab 'Plant'
    public GameObject applePrefab = null; // Prefab 'Apple'
    protected List<Vector3> respawn_points; // 출현 지점 List.
    public float step_timer = 0.0f;
    public float Image_timer = 0.0f;
    public static float RESPAWN_TIME_APPLE = 7.5f;
    public static float RESPAWN_TIME_IRON = 8.0f;
    public static float RESPAWN_TIME_PLANT = 100.0f;
    private float respawn_timer_apple = 0.0f; // 사과의 출현 시간.
    private float respawn_timer_iron = 0.0f; // 열매의 출현 시간.
    private float respawn_timer_plant = 0.0f; // 식물의 출현 시간.

    public Item.TYPE getItemType(GameObject item_go)
    {
        Item.TYPE type = Item.TYPE.NONE;
        if (item_go != null)
        { // 인수로 받은 GameObject가 비어있지 않으면.
            switch (item_go.tag)
            { // 태그로 분기.
                case "Iron": type = Item.TYPE.IRON; break;
                case "Apple": type = Item.TYPE.APPLE; break;
                case "Plant": type = Item.TYPE.PLANT; break;
                    //case "Zombie": type = Item.TYPE.MONSTER; break;
            }
        }
        return (type);
    }


    // Use this for initialization
    // 초기화 작업을 시행한다.
    void Start()
    {
        
        gage = GameObject.Find("uesr stat").GetComponent<Slider>();
        toilet = GameObject.Find("toilet").GetComponent<Slider>();
        // 메모리 영역 확보.
        this.respawn_points = new List<Vector3>();
        // "PlantRespawn" 태그가 붙은 모든 오브젝트를 배열에 저장.
        GameObject[] respawns =
        GameObject.FindGameObjectsWithTag("PlantRespawn");
        foreach (GameObject go in respawns)
        {
            // 렌더러 획득.
            MeshRenderer renderer = go.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            { // 렌더러가 존재하면.
                renderer.enabled = false; // 그 렌더러를 보이지 않게.
            }
            // 출현 포인트 List에 위치 정보를 추가.
            this.respawn_points.Add(go.transform.position);
        }
        // 사과의 출현 포인트를 취득하고, 렌더러를 보이지 않게.
        GameObject applerespawn = GameObject.Find("AppleRespawn");
        applerespawn.GetComponent<MeshRenderer>().enabled = false;
        // 열매의 출현 포인트를 취득하고, 렌더러를 보이지 않게.
        GameObject ironrespawn = GameObject.Find("IronRespawn");
        ironrespawn.GetComponent<MeshRenderer>().enabled = false;
        this.respawnIron(); // 열매을 하나 생성.
        respawnPlant();
        //Image1.SetActive(true);
        TimeControl.instance.GameActive = true;
        //this.respawnPlant(); 
        
    }

    // Update is called once per frame
    // 각 아이템의 타이머 값이 출현 시간을 초과하면 해당 아이템을 출현.
    void Update()
    {
        var SeT = GameObject.Find("Player").GetComponent<Stage1Event>();
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    Image_timer += 0.5f;
        //    if (Image1.activeSelf)
        //    {
        //        Image1.SetActive(false);
        //        TimeControl.instance.GameActive = true;
        //    }
        //    else if(Image2.activeSelf)
        //    {
        //        Image_timer += 0.5f;
        //        Image2.SetActive(false);
        //        TimeControl.instance.GameActive = true;
        //    }
        //    else if(Image3.activeSelf)
        //    {
        //        Image_timer += 0.5f;
        //        Image3.SetActive(false);
        //        TimeControl.instance.GameActive = true;
        //    }
        //}
        if (SeT.Time_set == true)
        {


            if (TimeControl.instance.GameActive)
            {
                time += Time.deltaTime;
                respawn_timer_apple += Time.deltaTime;
                respawn_timer_iron += Time.deltaTime;
                respawn_timer_plant += Time.deltaTime;
                Image_timer += Time.deltaTime;
                //respawn_timer_monster += Time.deltaTime;
                if (respawn_timer_apple > RESPAWN_TIME_APPLE)
                {
                    respawn_timer_apple = 0.0f;
                    this.respawnApple(); // 사과를 출현시킨다.
                }
                if (respawn_timer_iron > RESPAWN_TIME_IRON)
                {
                    respawn_timer_iron = 0.0f;
                    this.respawnIron(); // 열매를 출현시킨다.
                }
                if (respawn_timer_plant > RESPAWN_TIME_PLANT)
                {
                    respawn_timer_plant = 0.0f;
                    this.respawnPlant(); // 광물을 출현시킨다.

                }
                //if (Image_timer >= 8.0f && Image_timer <= 8.2f)
                //{
                //    Image2.SetActive(true);
                //    TimeControl.instance.GameActive = false;
                //}
                //if(Image_timer >= 15.0f && Image_timer <= 15.2f)
                //{
                //    Image3.SetActive(true);
                //    TimeControl.instance.GameActive = false;
                //}
            }
        }
    }

    // 열매를 출현시킨다.
    public void respawnIron()
    {
        // 열매 프리팹을 인스턴스화.
        GameObject go = GameObject.Instantiate(this.ironPrefab) as GameObject;
        // 열매의 출현 포인트를 취득.
        Vector3 pos = GameObject.Find("IronRespawn").transform.position;
        // 출현 위치를 조정.
        pos.y = -1.5f;
        pos.x += Random.Range(-5.0f, 5.0f);
        pos.z += Random.Range(-4.0f, 4.0f);
        // 열매의 위치를 이동.
        go.transform.position = pos;
    }
    // 사과를 출현시킨다.
    public void respawnApple()
    {
        // 사과 프리팹을 인스턴스화.
        GameObject go = GameObject.Instantiate(this.applePrefab) as GameObject;
        // 사과의 출현 포인트를 취득.
        Vector3 pos = GameObject.Find("AppleRespawn").transform.position;
        // 출현 위치를 조정.
        pos.y = -1.0f;
        pos.x += Random.Range(1.0f, 1.0f);
        pos.z += Random.Range(1.0f, 1.0f);
        // 사과의 위치를 이동.
        go.transform.position = pos;
    }
    // 광석을 출현시킨다.
    public void respawnPlant()
    {
       
        if (this.respawn_points.Count > 0)
        { // List가 비어있지 않으면.
          // 프리팹을 인스턴스화.
            GameObject go = Instantiate(plantPrefab) as GameObject;
            // 출현 포인트를 랜덤하게 취득.
            int n = Random.Range(0, this.respawn_points.Count);
            Vector3 pos = this.respawn_points[n];
            // 출현 위치를 조정.
            pos.y = -1.4f;
            pos.x += Random.Range(-1.0f, 1.0f);
            pos.z += Random.Range(-1.0f, 1.0f);
            // 위치를 이동.
            go.transform.position = pos;
        }
    }



    public float getGainRepairment(GameObject item_go)
    {
        float gain = 0.0f;
        if (item_go == null)
        {
            gain = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.IRON:
                    gain = GameStatus.GAIN_REPAIRMENT_IRON;
                    toilet.value += 0.15f;
                    break;
            }
        }
        return (gain);
    }

    public float getConsumeSatiety(GameObject item_go)
    {
        float consume = 0.0f;
        if (item_go == null)
        {
            consume = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.IRON:
                    consume = GameStatus.CONSUME_SATIETY_IRON; break;
                case Item.TYPE.APPLE:
                    consume = GameStatus.CONSUME_SATIETY_APPLE; break;
                case Item.TYPE.PLANT:
                    consume = GameStatus.CONSUME_SATIETY_PLANT; break;
            }
        }
        return (consume);
    }

    public float getRegainSatiety(GameObject item_go)
    {
        float regain = 0.0f;
        if (item_go == null)
        {
            regain = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.APPLE:
                    regain = GameStatus.REGAIN_SATIETY_APPLE;
                    gage.value += 0.22f;
                    break;
                case Item.TYPE.PLANT:
                    regain = GameStatus.REGAIN_SATIETY_PLANT; break;
            }
        }
        return (regain);
    }

}


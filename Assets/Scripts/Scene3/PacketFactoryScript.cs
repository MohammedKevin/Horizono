using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Diagnostics;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using System.Threading.Tasks;
using System.IO;

public class PacketFactoryScript : MonoBehaviour
{
    private float packagesAmount = 30.0f;

    private int _totalPacketCount = 0;
    private int _packetOnSpawnFloor = 0;
    private int _actualPoints = 0;
    private Stopwatch _stopWatch;
    public Text Timer;
    private Animator _animator;

    private readonly object packetsLock = new object();
    private readonly object listLock = new object();
    private readonly object counterLock = new object();
    
    string path = Application.dataPath + "/Resources/current.csv"; //Currentscorefile

    // Packet Prefabs
    public GameObject BluePacketPrefab;
    public GameObject GreenPacketPrefab;
    public GameObject RedPacketPrefab;
    // Spawning Areas
    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;

    #region slider
    private Slider _slider;
    public GameObject ProgressBar;

    public float _progessSpeed = 0.1f;
    #endregion
    private List<GameObject> prefabs;
    private bool isFinished = false;
    private bool startGame = false;

    // Start is called before the first frame update
    void Start()
    {
        /*_stopWatch = new Stopwatch();
        _stopWatch.Start();
        prefabs = new List<GameObject> { BluePacketPrefab, RedPacketPrefab, GreenPacketPrefab, RedPacketPrefab, GreenPacketPrefab, BluePacketPrefab, BluePacketPrefab, BluePacketPrefab,
            RedPacketPrefab, GreenPacketPrefab, BluePacketPrefab, GreenPacketPrefab, BluePacketPrefab, RedPacketPrefab, RedPacketPrefab, BluePacketPrefab, BluePacketPrefab, GreenPacketPrefab,
            RedPacketPrefab, GreenPacketPrefab, GreenPacketPrefab, BluePacketPrefab, RedPacketPrefab, GreenPacketPrefab, RedPacketPrefab, GreenPacketPrefab, BluePacketPrefab, RedPacketPrefab,
            RedPacketPrefab, GreenPacketPrefab};

        // Insantiating Packets
        InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn1);
        InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn2);
        InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn3);*/

        _progessSpeed = (float)(1 / packagesAmount);
        _slider = ProgressBar.GetComponent<Slider>();
        _animator = GetComponentInParent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            Timer.text = $"{_stopWatch.Elapsed.Minutes:d2}:{_stopWatch.Elapsed.Seconds:d2}:{_stopWatch.Elapsed.Milliseconds:d3}";
            if (_actualPoints >= packagesAmount) //pls change
            {
                if (_stopWatch.IsRunning)
                {
                    _stopWatch.Stop();
                    TimeSpan ts = _stopWatch.Elapsed;
                    GameObject.Find("WatchTime").GetComponent<Text>().text = String.Format("needed time: {0}h:{1}min:{2}sec:{3}ms", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                    string content = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")},{ts.TotalSeconds}";
                    File.WriteAllText(path, content);
                    Invoke("Update", 5);
                    //Wait(3000);
                    /*Task.Run(async () =>
                    {
                        await Task.Delay(5000);
                        if (_animator != null)
                            this._animator.SetBool("GameEndedBool", true);
                        GameObject.Find("SceneThreeState").GetComponent<SceneThreeState>().ExitState();
                        GameObject.Find("EndSceneScript").GetComponent<EndSceneScript>().EndSceneEvent();
                    });*/

                    this._animator.SetBool("GameEndedBool", true);
                    //Task.Delay(4000).Wait();
                    this._animator.SetBool("FadeOutBool", true);
                    this.isFinished = true;
                }
            }

            if (_packetOnSpawnFloor < 3)
            {
                StartCoroutine(WaitAndInstantiate());
            }
        }
    }

    /*private IEnumerator Wait(int v)
    {
        yield return WaitForSeconds(3000);
        int i = 0;
    }*/

    #region Trigger Methods

    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.Contains("PharusTrack"))
        {
            _packetOnSpawnFloor++;
            Debug.Log(_packetOnSpawnFloor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.name.Contains("PharusTrack"))
        {
            _packetOnSpawnFloor--;
            Debug.Log(_packetOnSpawnFloor);
        }
    }

    #endregion

    public void StartGame()
    {
        startGame = true;
        _stopWatch = new Stopwatch();
        _stopWatch.Start();
        prefabs = new List<GameObject> { BluePacketPrefab, RedPacketPrefab, GreenPacketPrefab, RedPacketPrefab, GreenPacketPrefab, BluePacketPrefab, BluePacketPrefab, BluePacketPrefab,
            RedPacketPrefab, GreenPacketPrefab, BluePacketPrefab, GreenPacketPrefab, BluePacketPrefab, RedPacketPrefab, RedPacketPrefab, BluePacketPrefab, BluePacketPrefab, GreenPacketPrefab,
            RedPacketPrefab, GreenPacketPrefab, GreenPacketPrefab, BluePacketPrefab, RedPacketPrefab, GreenPacketPrefab, RedPacketPrefab, GreenPacketPrefab, BluePacketPrefab, RedPacketPrefab,
            RedPacketPrefab, GreenPacketPrefab};

        // Insantiating Packets
        InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn1);
        InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn2);
        InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn3);
    }

    private IEnumerator WaitAndInstantiate()
    {
        yield return new WaitForSeconds(3);
        if (_packetOnSpawnFloor < 3)
        {
            lock (packetsLock)
            {
                if (!(Spawn1.GetComponent<SpawnSpotScript>().HasPacketOnSpawnSpot) && _packetOnSpawnFloor < 3)
                {
                    InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn1);
                }
                if (!(Spawn2.GetComponent<SpawnSpotScript>().HasPacketOnSpawnSpot) && _packetOnSpawnFloor < 3)
                {
                    InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn2);
                }
                if (!(Spawn3.GetComponent<SpawnSpotScript>().HasPacketOnSpawnSpot) && _packetOnSpawnFloor < 3)
                {
                    InstantiatePacket(GetFirstOfListAndRemoveIt(), Spawn3);
                }
            }
        }
    }

    private void InstantiatePacket(GameObject prefab, GameObject parent)
    {
        if (prefab == null) return;
        if (_packetOnSpawnFloor < 3)
        {
            lock (counterLock)
            {
                if ( _packetOnSpawnFloor < 3)
                {
                    GameObject packet = GameObject.Instantiate(prefab, new Vector3(), Quaternion.identity);
                    packet.transform.SetParent(parent.transform, false);
                    this._totalPacketCount++;
                    Debug.Log("Total amount of packets: " + _totalPacketCount);
                }
            }            
        }
    }

    private GameObject GetFirstOfListAndRemoveIt()
    {
        if (prefabs.Count == 0) return null;
        lock (listLock)
        {
            var go = prefabs[0]; // go stands for Gameobject
            prefabs.RemoveAt(0);
            return go;
        }
    }

    public void IncreasePoints()
    { 
        _actualPoints++;
        _slider.value += _progessSpeed;
    }
}

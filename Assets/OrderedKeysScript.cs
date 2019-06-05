using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class OrderedKeysScript : MonoBehaviour {

    public KMAudio Audio;
    public KMBombInfo bomb;

    public List<KMSelectable> keys;
    public Renderer[] meter;
    public Renderer[] keyID;
    public Material[] keyColours;

    private static int[][][][] table = new int[6][][][] {
        new int[6][][] { new int[6][] { new int[6] { 1, 3, 4, 6, 2, 5},
                                        new int[6] { 4, 5, 1, 2, 6, 3},
                                        new int[6] { 6, 2, 5, 3, 1, 4},
                                        new int[6] { 2, 6, 3, 4, 5, 1},
                                        new int[6] { 3, 1, 2, 5, 4, 6},
                                        new int[6] { 5, 4, 6, 1, 3, 2} },

                        new int[6][]  { new int[6] { 4, 2, 5, 1, 3, 6},
                                        new int[6] { 3, 6, 1, 5, 4, 2},
                                        new int[6] { 2, 1, 3, 6, 5, 4},
                                        new int[6] { 5, 4, 2, 3, 6, 1},
                                        new int[6] { 1, 5, 6, 4, 2, 3},
                                        new int[6] { 6, 3, 4, 2, 1, 5} },

                        new int[6][]  { new int[6] { 3, 4, 2, 1, 5, 6},
                                        new int[6] { 5, 1, 6, 2, 3, 4},
                                        new int[6] { 6, 3, 5, 4, 1, 2},
                                        new int[6] { 4, 6, 3, 5, 2, 1},
                                        new int[6] { 2, 5, 1, 6, 4, 3},
                                        new int[6] { 1, 2, 4, 3, 6, 5} },

                        new int[6][]  { new int[6] { 2, 4, 5, 3, 6, 1},
                                        new int[6] { 4, 3, 1, 6, 5, 2},
                                        new int[6] { 1, 5, 4, 2, 3, 6},
                                        new int[6] { 6, 2, 3, 4, 1, 5},
                                        new int[6] { 3, 1, 6, 5, 2, 4},
                                        new int[6] { 5, 6, 2, 1, 4, 3} },

                        new int[6][]  { new int[6] { 6, 4, 5, 1, 2, 3},
                                        new int[6] { 1, 3, 6, 2, 5, 4},
                                        new int[6] { 5, 2, 1, 3, 4, 6},
                                        new int[6] { 3, 6, 4, 5, 1, 2},
                                        new int[6] { 4, 5, 2, 6, 3, 1},
                                        new int[6] { 2, 1, 3, 4, 6, 5} },

                        new int[6][]  { new int[6] { 5, 2, 4, 1, 3, 6},
                                        new int[6] { 3, 1, 2, 5, 6, 4},
                                        new int[6] { 1, 4, 3, 6, 2, 5},
                                        new int[6] { 2, 5, 6, 3, 4, 1},
                                        new int[6] { 6, 3, 5, 4, 1, 2},
                                        new int[6] { 4, 6, 1, 2, 5, 3} } },

        new int[6][][] {new int[6][]  { new int[6] { 4, 5, 3, 2, 6, 1},
                                        new int[6] { 3, 2, 4, 1, 5, 6},
                                        new int[6] { 6, 1, 2, 4, 3, 5},
                                        new int[6] { 5, 3, 1, 6, 4, 2},
                                        new int[6] { 2, 4, 6, 5, 1, 3},
                                        new int[6] { 1, 6, 5, 3, 2, 4} },

                        new int[6][]  { new int[6] { 5, 1, 3, 6, 4, 2},
                                        new int[6] { 6, 5, 2, 1, 3, 4},
                                        new int[6] { 3, 4, 1, 2, 5, 6},
                                        new int[6] { 2, 3, 4, 5, 6, 1},
                                        new int[6] { 1, 6, 5, 4, 2, 3},
                                        new int[6] { 4, 2, 6, 3, 1, 5} },

                        new int[6][]  { new int[6] { 1, 2, 5, 6, 4, 3},
                                        new int[6] { 3, 4, 6, 1, 5, 2},
                                        new int[6] { 6, 1, 4, 2, 3, 5},
                                        new int[6] { 4, 6, 3, 5, 2, 1},
                                        new int[6] { 5, 3, 2, 4, 1, 6},
                                        new int[6] { 2, 5, 1, 3, 6, 4} },

                        new int[6][]  { new int[6] { 3, 1, 4, 5, 2, 6},
                                        new int[6] { 6, 2, 5, 1, 4, 3},
                                        new int[6] { 1, 3, 2, 6, 5, 4},
                                        new int[6] { 4, 5, 1, 3, 6, 2},
                                        new int[6] { 2, 6, 3, 4, 1, 5},
                                        new int[6] { 5, 4, 6, 2, 3, 1} },

                        new int[6][]  { new int[6] { 6, 5, 4, 3, 2, 1},
                                        new int[6] { 3, 2, 1, 5, 6, 4},
                                        new int[6] { 1, 6, 2, 4, 3, 5},
                                        new int[6] { 5, 3, 6, 1, 4, 2},
                                        new int[6] { 2, 4, 5, 6, 1, 3},
                                        new int[6] { 4, 1, 3, 2, 5, 6} },

                        new int[6][]  { new int[6] { 2, 3, 6, 5, 4, 1},
                                        new int[6] { 3, 4, 2, 1, 5, 6},
                                        new int[6] { 4, 2, 5, 6, 1, 3},
                                        new int[6] { 6, 5, 1, 2, 3, 4},
                                        new int[6] { 5, 1, 3, 4, 6, 2},
                                        new int[6] { 1, 6, 4, 3, 2, 5} } },

        new int[6][][] {new int[6][]  { new int[6] { 4, 3, 6, 2, 5, 1},
                                        new int[6] { 5, 1, 4, 6, 3, 2},
                                        new int[6] { 6, 2, 5, 3, 1, 4},
                                        new int[6] { 3, 5, 2, 1, 4, 6},
                                        new int[6] { 2, 4, 1, 5, 6, 3},
                                        new int[6] { 1, 6, 3, 4, 2, 5} },

                        new int[6][]  { new int[6] { 2, 6, 1, 5, 3, 4},
                                        new int[6] { 5, 3, 4, 1, 2, 6},
                                        new int[6] { 6, 4, 3, 2, 1, 5},
                                        new int[6] { 3, 1, 5, 6, 4, 2},
                                        new int[6] { 1, 2, 6, 4, 5, 3},
                                        new int[6] { 4, 5, 2, 3, 6, 1} },

                        new int[6][]  { new int[6] { 3, 6, 1, 2, 5, 4},
                                        new int[6] { 1, 4, 6, 3, 2, 5},
                                        new int[6] { 5, 1, 3, 4, 6, 2},
                                        new int[6] { 2, 5, 4, 6, 1, 3},
                                        new int[6] { 4, 2, 5, 1, 3, 6},
                                        new int[6] { 6, 3, 2, 5, 4, 1} },

                        new int[6][]  { new int[6] { 5, 2, 3, 4, 1, 6},
                                        new int[6] { 2, 4, 1, 3, 6, 5},
                                        new int[6] { 3, 5, 6, 2, 4, 1},
                                        new int[6] { 6, 1, 4, 5, 3, 2},
                                        new int[6] { 4, 6, 5, 1, 2, 3},
                                        new int[6] { 1, 3, 2, 6, 5, 4} },

                        new int[6][]  { new int[6] { 1, 5, 2, 6, 3, 4},
                                        new int[6] { 2, 3, 6, 5, 4, 1},
                                        new int[6] { 4, 1, 3, 2, 6, 5},
                                        new int[6] { 3, 6, 4, 1, 5, 2},
                                        new int[6] { 5, 4, 1, 3, 2, 6},
                                        new int[6] { 6, 2, 5, 4, 1, 3} },

                        new int[6][]  { new int[6] { 6, 5, 3, 4, 2, 1},
                                        new int[6] { 3, 6, 2, 1, 5, 4},
                                        new int[6] { 2, 4, 6, 5, 1, 3},
                                        new int[6] { 4, 1, 5, 3, 6, 2},
                                        new int[6] { 1, 2, 4, 6, 3, 5},
                                        new int[6] { 5, 3, 1, 2, 4, 6} } },

        new int[6][][]  {new int[6][] { new int[6] { 5, 1, 2, 4, 6, 3},
                                        new int[6] { 3, 2, 6, 1, 5, 4},
                                        new int[6] { 6, 3, 1, 5, 4, 2},
                                        new int[6] { 2, 4, 5, 3, 1, 6},
                                        new int[6] { 4, 5, 3, 6, 2, 1},
                                        new int[6] { 1, 6, 4, 2, 3, 5} },

                        new int[6][]  { new int[6] { 1, 3, 2, 4, 5, 6},
                                        new int[6] { 6, 2, 3, 5, 1, 4},
                                        new int[6] { 2, 4, 1, 3, 6, 5},
                                        new int[6] { 5, 6, 4, 2, 3, 1},
                                        new int[6] { 4, 1, 5, 6, 2, 3},
                                        new int[6] { 3, 5, 6, 1, 4, 2} },

                        new int[6][]  { new int[6] { 2, 3, 4, 5, 6, 1},
                                        new int[6] { 5, 1, 6, 3, 4, 2},
                                        new int[6] { 3, 4, 2, 6, 1, 5},
                                        new int[6] { 4, 2, 3, 1, 5, 6},
                                        new int[6] { 6, 5, 1, 4, 2, 3},
                                        new int[6] { 1, 6, 5, 2, 3, 4} },

                        new int[6][]  { new int[6] { 4, 3, 2, 1, 5, 6},
                                        new int[6] { 6, 4, 3, 5, 2, 1},
                                        new int[6] { 3, 1, 6, 2, 4, 5},
                                        new int[6] { 5, 6, 1, 4, 3, 2},
                                        new int[6] { 2, 5, 4, 6, 1, 3},
                                        new int[6] { 1, 2, 5, 3, 6, 4} },

                        new int[6][]  { new int[6] { 6, 3, 4, 1, 2, 5},
                                        new int[6] { 5, 4, 3, 2, 6, 1},
                                        new int[6] { 1, 2, 6, 4, 5, 3},
                                        new int[6] { 3, 6, 1, 5, 4, 2},
                                        new int[6] { 4, 5, 2, 3, 1, 6},
                                        new int[6] { 2, 1, 5, 6, 3, 4} },

                        new int[6][]  { new int[6] { 3, 4, 1, 2, 6, 5},
                                        new int[6] { 6, 2, 3, 1, 5, 4},
                                        new int[6] { 2, 5, 4, 3, 1, 6},
                                        new int[6] { 4, 1, 6, 5, 2, 3},
                                        new int[6] { 1, 6, 5, 4, 3, 2},
                                        new int[6] { 5, 3, 2, 6, 4, 1} } },

        new int[6][][] {new int[6][]  { new int[6] { 2, 4, 6, 5, 3, 1},
                                        new int[6] { 4, 3, 1, 2, 6, 5},
                                        new int[6] { 1, 5, 3, 6, 4, 2},
                                        new int[6] { 6, 1, 2, 4, 5, 3},
                                        new int[6] { 5, 2, 4, 3, 1, 6},
                                        new int[6] { 3, 6, 5, 1, 2, 4} },

                        new int[6][]  { new int[6] { 3, 5, 6, 1, 2, 4},
                                        new int[6] { 2, 3, 1, 6, 4, 5},
                                        new int[6] { 4, 1, 3, 2, 5, 6},
                                        new int[6] { 6, 2, 4, 5, 1, 3},
                                        new int[6] { 1, 4, 5, 3, 6, 2},
                                        new int[6] { 5, 6, 2, 4, 3, 1} },

                        new int[6][]  { new int[6] { 4, 3, 2, 6, 5, 1},
                                        new int[6] { 5, 1, 6, 3, 4, 2},
                                        new int[6] { 2, 6, 5, 1, 3, 4},
                                        new int[6] { 1, 4, 3, 2, 6, 5},
                                        new int[6] { 6, 2, 4, 5, 1, 3},
                                        new int[6] { 3, 5, 1, 4, 2, 6} },

                        new int[6][]  { new int[6] { 6, 4, 5, 3, 1, 2},
                                        new int[6] { 3, 2, 6, 1, 4, 5},
                                        new int[6] { 4, 5, 1, 2, 6, 3},
                                        new int[6] { 1, 3, 4, 5, 2, 6},
                                        new int[6] { 5, 6, 2, 4, 3, 1},
                                        new int[6] { 2, 1, 3, 6, 5, 4} },

                        new int[6][]  { new int[6] { 5, 4, 1, 6, 2, 3},
                                        new int[6] { 1, 3, 6, 5, 4, 2},
                                        new int[6] { 4, 1, 2, 3, 6, 5},
                                        new int[6] { 3, 2, 5, 4, 1, 6},
                                        new int[6] { 2, 6, 3, 1, 5, 4},
                                        new int[6] { 6, 5, 4, 2, 3, 1} },

                        new int[6][]  { new int[6] { 1, 5, 6, 2, 3, 4},
                                        new int[6] { 2, 4, 1, 6, 5, 3},
                                        new int[6] { 5, 6, 3, 1, 4, 2},
                                        new int[6] { 3, 2, 4, 5, 1, 6},
                                        new int[6] { 6, 3, 5, 4, 2, 1},
                                        new int[6] { 4, 1, 2, 3, 6, 5} } },

        new int[6][][] {new int[6][]  { new int[6] { 3, 5, 6, 2, 1, 4},
                                        new int[6] { 2, 4, 1, 3, 6, 5},
                                        new int[6] { 1, 2, 3, 4, 5, 6},
                                        new int[6] { 5, 6, 4, 1, 2, 3},
                                        new int[6] { 4, 1, 5, 6, 3, 2},
                                        new int[6] { 6, 3, 2, 5, 4, 1} },

                        new int[6][]  { new int[6] { 6, 1, 3, 5, 4, 2},
                                        new int[6] { 3, 5, 1, 2, 6, 4},
                                        new int[6] { 5, 2, 4, 6, 1, 3},
                                        new int[6] { 1, 4, 6, 3, 2, 5},
                                        new int[6] { 4, 3, 2, 1, 5, 6},
                                        new int[6] { 2, 6, 5, 4, 3, 1} },

                        new int[6][]  { new int[6] { 2, 3, 5, 1, 4, 6},
                                        new int[6] { 4, 1, 2, 6, 5, 3},
                                        new int[6] { 3, 6, 4, 2, 1, 5},
                                        new int[6] { 6, 5, 3, 4, 2, 1},
                                        new int[6] { 1, 4, 6, 5, 3, 2},
                                        new int[6] { 5, 2, 1, 3, 6, 4} },

                        new int[6][]  { new int[6] { 1, 4, 3, 5, 6, 2},
                                        new int[6] { 5, 2, 1, 4, 3, 6},
                                        new int[6] { 2, 6, 4, 3, 1, 5},
                                        new int[6] { 3, 1, 5, 6, 2, 4},
                                        new int[6] { 6, 5, 2, 1, 4, 3},
                                        new int[6] { 4, 3, 6, 2, 5, 1} },

                        new int[6][]  { new int[6] { 5, 3, 2, 4, 6, 1},
                                        new int[6] { 4, 2, 6, 1, 5, 3},
                                        new int[6] { 1, 4, 5, 6, 3, 2},
                                        new int[6] { 6, 1, 3, 2, 4, 5},
                                        new int[6] { 2, 5, 4, 3, 1, 6},
                                        new int[6] { 3, 6, 1, 5, 2, 4} },

                        new int[6][]  { new int[6] { 4, 2, 3, 6, 5, 1},
                                        new int[6] { 5, 1, 4, 3, 6, 2},
                                        new int[6] { 6, 3, 5, 1, 2, 4},
                                        new int[6] { 1, 6, 2, 5, 4, 3},
                                        new int[6] { 3, 4, 6, 2, 1, 5},
                                        new int[6] { 2, 5, 1, 4, 3, 6} } } };

    private static string[] colourList = new string[6] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
    private int[][] info = new int[6][] { new int[4], new int[4], new int[4], new int[4], new int[4], new int[4] };
    private int stage = 1;
    private int pressCount;
    private int resetCount;
    private IEnumerator sequence;
    private bool[] alreadypressed = new bool[6] { true, true, true, true, true, true};
    private bool pressable;
    private List<string> presses = new List<string> { };
    private List<string> answer = new List<string> { };
    private List<string> labelList = new List<string> { };

    //Logging
    static int moduleCounter = 1;
    int moduleID;
    private bool moduleSolved;

    private void Awake()
    {
        moduleID = moduleCounter++;
        sequence = Shuff();
        foreach (Renderer m in meter)
        {
            m.material = keyColours[6];
        }
        foreach (KMSelectable key in keys)
        {
            key.transform.localPosition = new Vector3(0, 0, -1f);
            key.OnInteract += delegate () { KeyPress(key); return false; };
        }
    }

    void Start () {
        Reset();
	}

    private void KeyPress(KMSelectable key)
    {
        if (alreadypressed[keys.IndexOf(key)] == false && moduleSolved == false && pressbale == true)
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            alreadypressed[keys.IndexOf(key)] = true;
            presses.Add((keys.IndexOf(key) + 1).ToString());
            key.transform.localPosition = new Vector3(0, 0, -1f);

            key.AddInteractionPunch();
            if (pressCount < 5)
            {
                pressCount++;
            }
            else
            {
                pressCount = 0;
                string[] answ = answer.ToArray();
                string[] press = presses.ToArray();
                string ans = string.Join(string.Empty, answ);
                string pr = string.Join(string.Empty, press);
                Debug.LogFormat("[Ordered Keys #{0}] After {1} reset(s), the buttons were pressed in the order: {2}", moduleID, resetCount, pr);
                if (ans == pr)
                {
                    meter[stage - 1].material = keyColours[7];
                    Audio.PlaySoundAtTransform("InputCorrect", transform);
                    if (stage < 4)
                    {
                        stage++;
                    }
                    else
                    {
                        moduleSolved = true;
                    }                    
                }
                else
                {
                    GetComponent<KMBombModule>().HandleStrike();
                }
                answer.Clear();
                presses.Clear();
                labelList.Clear();
                resetCount++;
                Reset();
            }
        }
    }

    private void Reset()
    {
        if (moduleSolved == false)
        {
	    pressable = false;
            List<int> initialList = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> finalList = new List<int> { };
            for (int i = 0; i < 6; i++)
            {
                int temp = UnityEngine.Random.Range(0, initialList.Count());
                finalList.Add(initialList[temp]);
                initialList.RemoveAt(temp);
            }
            for (int i = 0; i < 6; i++)
            {
                answer.Add((finalList.IndexOf(i + 1) + 1).ToString());
                info[i][0] = UnityEngine.Random.Range(0, 6);
                info[i][1] = UnityEngine.Random.Range(0, 6);
                info[i][2] = i + 1;
                for (int j = 0; j < 6; j++)
                {
                    if (finalList[i] == table[info[i][0]][info[i][1]][i][j])
                    {
                        info[i][3] = j;
                        break;
                    }
                }
                labelList.Add((info[i][3] + 1).ToString());
                //Debug.Log(info[i][0] + "," + info[i][1] + "," + i + "," + info[i][3] +"="+ table[info[i][0]][info[i][1]][i][info[i][3]]);
            }
           
            string[] a = new string[6];
            string[] b = new string[6];
            for (int i = 0; i < 6; i++)
            {
                a[i] = colourList[info[i][0]];
                b[i] = colourList[info[i][1]];
                if (i == 5)
                {
                    string A = String.Join(", ", a);
                    string B = String.Join(", ", b);
                    Debug.LogFormat("[Ordered Keys #{0}] After {1} reset(s), the buttons had the colours: {2}", moduleID, resetCount, A);
                    Debug.LogFormat("[Ordered Keys #{0}] After {1} reset(s), the labels had the colours: {2}", moduleID, resetCount, B);
                }
            }
            string[] label = labelList.ToArray();
            string[] answ = answer.ToArray();
            string l = string.Join(", " , label);
            string ans = string.Join(string.Empty, answ);
            Debug.LogFormat("[Ordered Keys #{0}] After {1} reset(s), the buttons were labelled: {2}", moduleID, resetCount, l);
            Debug.LogFormat("[Ordered Keys #{0}] After {1} reset(s), the pressing order was: {2}", moduleID, resetCount, ans);
        }
        StartCoroutine(sequence);
    }

    private IEnumerator Shuff()
    {
        for (int i = 0; i < 30; i++)
        {
            if (i % 5 == 4)
            {
                if (moduleSolved == true)
                {
                    alreadypressed[(i - 4) / 5] = false;
                    keyID[(i - 4) / 5].material = keyColours[8];
                    keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(0, 0, 0, 255);
                    keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().text = "0";
                    if (i == 29)
                    {
                        GetComponent<KMBombModule>().HandlePass();
                        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                    }
                }
                else
                {
                    alreadypressed[(i - 4) / 5] = false;
                    keys[(i - 4) / 5].transform.localPosition = new Vector3(0, 0, 0);
                    GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
                    keyID[(i - 4) / 5].material = keyColours[info[(i - 4) / 5][0]];
                    switch (info[(i - 4) / 5][1])
                    {
                        case 0:
                            keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(255, 0, 0, 255);
                            break;
                        case 1:
                            keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(0, 255, 0, 255);
                            break;
                        case 2:
                            keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(75, 75, 255, 255);
                            break;
                        case 3:
                            keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(0, 255, 255, 255);
                            break;
                        case 4:
                            keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(255, 0, 255, 255);
                            break;
                        case 5:
                            keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(255, 255, 0, 255);
                            break;
                    }
                    keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().text = labelList[(i - 4) / 5];
                }
                if (i == 29)
                {
                    i = -1;
		    pressable = true;
                    StopCoroutine(sequence);
                }
            }
            else
            {
                for (int j = 0; j < 6; j++)
                {
                    int[] rand = new int[3];
                    for (int k = 0; k < 3; k++)
                    {
                        rand[k] = UnityEngine.Random.Range(0, 6);
                    }
                    if (alreadypressed[j] == true)
                    {
                        keyID[j].material = keyColours[rand[0]];
                        switch (rand[1])
                        {
                            case 0:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(255, 0, 0, 255);
                                break;
                            case 1:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(0, 255, 0, 255);
                                break;
                            case 2:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(75, 75, 225, 255);
                                break;
                            case 3:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(0, 255, 255, 255);
                                break;
                            case 4:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(255, 0, 255, 255);
                                break;
                            case 5:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(255, 255, 0, 255);
                                break;
                        }
                        keys[j].GetComponentInChildren<TextMesh>().text = (rand[2] + 1).ToString();
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

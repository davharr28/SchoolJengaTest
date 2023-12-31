﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Interface for all the UI in the level
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private CameraController camControl;

    [SerializeField] private Transform[] stacks;
    [SerializeField] private Button[] gradeBtn;

    [Header("Game Mode buttons")]
    [SerializeField] private Button testMyStackModeBtn;
    [SerializeField] private Button resetStackBtn;
    
    [Header("Block Info window")]
    [SerializeField] private GameObject blockInfo_Window;
    [SerializeField] private TextMeshProUGUI blockInfo_Line1Txt;
    [SerializeField] private TextMeshProUGUI blockInfo_Line2Txt;
    [SerializeField] private TextMeshProUGUI blockInfo_Line3Txt;
    
    [Header("Sound")]
    [SerializeField] private AudioClip clickSound;

    private AudioSource uiSoundSystem;
    private Transform currentStack;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        uiSoundSystem = GetComponent<AudioSource>();
        currentStack = stacks[0];
    }
    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            Transform stack = stacks[i];
            gradeBtn[i].onClick.AddListener(() => GradeButtonClicked(stack));
        }

        testMyStackModeBtn.onClick.AddListener(() => TestStackModeButtonClicked());
        resetStackBtn.onClick.AddListener(() => ResetStackButtonClicked());
    }
    private void OnDisable()
    {
        for (int i = 0; i < 3; i++)
        {
            gradeBtn[i].onClick.RemoveAllListeners();
        }

        testMyStackModeBtn.onClick.RemoveAllListeners();
        resetStackBtn.onClick.RemoveAllListeners();
    }

    private void GradeButtonClicked(Transform stack)
    {
        ButtonSound();
        camControl.ChangeStack(stack);
        currentStack = stack;
    }

    private void TestStackModeButtonClicked()
    {
        ButtonSound();
        currentStack.GetComponent<StackBuilder>().TestMyBlockMode();
    }
    private void ResetStackButtonClicked()
    {
        ButtonSound();
        currentStack.GetComponent<StackBuilder>().Restack();
    }
    private void ButtonSound()
    {
        uiSoundSystem.PlayOneShot(clickSound);
    }

    public void DisplayBlockWindow(string grade, string domain, string cluster, string standardId, string standardDesc)
    {
        blockInfo_Line1Txt.text = grade + " : " + domain;
        blockInfo_Line2Txt.text = cluster;
        blockInfo_Line3Txt.text = standardId + " : " + standardDesc;
        blockInfo_Window.SetActive(true);
    }
    public void CloseDisplayBlockWindow()
    {
        blockInfo_Window.SetActive(false);
    }


}


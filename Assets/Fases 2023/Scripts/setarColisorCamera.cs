using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class setarColisorCamera : MonoBehaviour
{
    public CinemachineConfiner confiner;


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += onChangeActiveScene;

        onChangeActiveScene(default, default);
    }

    private void onChangeActiveScene(Scene arg0, Scene arg1)
    {
        

        var v =  GameObject.FindGameObjectWithTag("ColisorCamera");

        SceneManager.MoveGameObjectToScene(v, SceneManager.GetSceneByName("ComunsDeFase"));

        confiner.m_BoundingShape2D = v.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

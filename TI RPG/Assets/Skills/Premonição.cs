using System;
using System.Collections;
using System.Collections.Generic;
using Skills;
using UnityEngine;

public class Premonição : Skill
{
    [SerializeField] private List<Outline> outlines;
    [SerializeField] private GameObject[] inimigos;
    bool isActive = false;


    private void Awake()
    {
        inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
    }

    private void PrepareOutline(Outline _outline)
    {
        _outline.OutlineColor = Color.red;
        _outline.OutlineMode = Outline.Mode.OutlineAll;
        _outline.OutlineWidth = 2f;
        _outline.enabled = false;
        outlines.Add(_outline);
    }
    private void Start()
    {
        foreach (var inimigo in inimigos)
        {
            Outline _outline = inimigo.AddComponent<Outline>();
            PrepareOutline(_outline);
        }
    }
    
    public override void Update()
    {
        if (!Input.GetKeyDown(KeyCode.G)) return;
        isActive = !isActive;
        foreach (var outline in outlines)
        {
            outline.enabled = isActive;
        }
    }
    
}

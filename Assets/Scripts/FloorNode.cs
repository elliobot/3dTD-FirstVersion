using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorNode : MonoBehaviour {

    public Vector3 positionOffset;

    public Color hoverColor;
    public Color startColor;
    private Renderer rend;
    private GameObject turret;
    public bool selectionStatus;
    private GameObject selectedArea;



    public GameObject selectedAreaObjectFail;
    public GameObject selectedAreaObject;

    public float turretRange;
    public GameObject turretToBuild;
    private Turret turretSelected;
    BuildManager buildManager;
    public string selectTag = "Selection";

    private void Start()
    {
        rend = GetComponent<Renderer>();

        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        if (BuildManager.instance.GetTurretToBuild() != null)
        {
            turretToBuild = BuildManager.instance.GetTurretToBuild();
            turretSelected = turretToBuild.GetComponent<Turret>();
        }

        BuildManager.instance.selectedAreas = GameObject.FindGameObjectsWithTag("Selection");


    }

    private void OnMouseDown()
    {
        
        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }
        if (turret != null)
        {
            Debug.Log("Nope");
            return;
        }

        turretSelected = turretToBuild.GetComponent<Turret>();

        if (ResourceManager.instance.gold < turretSelected.goldCost || ResourceManager.instance.wood < turretSelected.woodCost)
        {

        }
        else
        {
            ResourceManager.instance.gold = ResourceManager.instance.gold - turretSelected.goldCost;
            ResourceManager.instance.wood = ResourceManager.instance.wood - turretSelected.woodCost;
            this.turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        }


    }



    void OnMouseOver()
    {
        
        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }


        turretToBuild = BuildManager.instance.GetTurretToBuild();


        rend.material.color = hoverColor;


        if (turretToBuild != null && turretSelected != null)
        {

            SelectArea();
        }





    }
    private void OnMouseExit()
    {

        DeleteAreas();
        
        rend.material.color = startColor;
        selectionStatus = false;
    }

    public void DeleteAreas()
    {
        foreach (GameObject selection in BuildManager.instance.selectedAreas)
        {
            Destroy(selection);
        }
    }

    public void SelectArea()
    {
        turretRange = turretSelected.range;
        if (ResourceManager.instance.gold >= turretSelected.goldCost && ResourceManager.instance.wood >= turretSelected.woodCost  )
        {
             if (selectedArea == null || selectionStatus == false)
             {
                DeleteAreas();
                selectionStatus = true;
                this.selectedArea = (GameObject)Instantiate(selectedAreaObject, transform.position + positionOffset, transform.rotation);
                this.selectedArea.transform.localScale = new Vector3(turretRange, turretRange, 0);
                return;
             }
            return;
        }
        else if (ResourceManager.instance.gold < turretSelected.goldCost || ResourceManager.instance.wood < turretSelected.woodCost || this.turret != null)
        {
             if (selectedArea == null || selectionStatus == true)
             {
                DeleteAreas();
                selectionStatus = false;
                this.selectedArea = (GameObject)Instantiate(selectedAreaObjectFail, transform.position + positionOffset, transform.rotation);
                this.selectedArea.transform.localScale = new Vector3(turretRange, turretRange, 0);
                return;
             }
            return;
        }
        DeleteAreas();
        return;
    }



    //public void SelectArea()
    //{

    //    if (BuildManager.instance.GetTurretToBuild() != null)
    //    {
    //        turretRange = turretSelected.range;

    //        if (ResourceManager.instance.gold >= turretSelected.goldCost && ResourceManager.instance.wood >= turretSelected.woodCost)
    //        {
    //            if (selectionStatus != selectedAreaObject)
    //            {

    //                this.selectionStatus = selectedAreaObject;

    //                this.selectedArea = (GameObject)Instantiate(selectionStatus, transform.position + positionOffset, transform.rotation);
    //                this.selectedArea.transform.localScale = new Vector3(turretRange, turretRange, 0);

    //            }
    //            return;
    //        }
    //        else
    //        {
    //            if (selectionStatus != selectedAreaObjectFail)
    //            {
    //                this.selectionStatus = selectedAreaObjectFail;
    //                this.selectedArea = (GameObject)Instantiate(selectionStatus, transform.position + positionOffset, transform.rotation);
    //                this.selectedArea.transform.localScale = new Vector3(turretRange, turretRange, 0);
    //            }
    //            return;
    //        }
    //    }
    //    DeleteAreas();
    //}

}

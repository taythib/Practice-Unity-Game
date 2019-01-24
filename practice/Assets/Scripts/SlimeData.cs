using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class SlimeData{
    public float age;
    public int trueAge;
    public float hunger;
    public int speed;
    public string slimeName;

    // Use this for initialization
    public SlimeData(float age, int trueAge, float hunger, int speed, string slimeName) {
        this.age = age;
        this.trueAge = trueAge;
        this.hunger = hunger;
        this.speed = speed;
        this.slimeName = slimeName;
	}
	
	// Update is called once per frame
	public void Update () {
        age += Time.deltaTime;
        trueAge = Mathf.RoundToInt(age) / 60;
        if(Mathf.RoundToInt(hunger) / 60 < 10)
        {
            Debug.Log(Mathf.RoundToInt(hunger) / 60);
        }
        hunger += Time.deltaTime;
    }

    public void giveName(string newName)
    {
        slimeName = newName;
    }

    public string getName()
    {
        return slimeName;
    }

    public SlimeData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SlimeData>(jsonString);
    }
}

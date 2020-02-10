using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    public GameObject botPrefab;
    public int populationSize = 100;
    public List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trialtime = 7;
    int generation = 1;

    GUIStyle guistyle = new GUIStyle();
    private void OnGUI()
    {
        guistyle.fontSize = 25;
        guistyle.normal.textColor = Color.white;
            ;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", guistyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Generation:" + generation, guistyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0.00}", elapsed), guistyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population:" + population.Count, guistyle);
        GUI.EndGroup();
    }
    // Start is called before the first frame update
    void Start()
    { 
        for(int i=0; i<populationSize; i++)
        {
            Vector3 startingpos = new Vector3(this.transform.position.x + Random.Range(2, -2),
                this.transform.position.y,
                this.transform.position.z + Random.Range(-2, 2));

            GameObject b = Instantiate(botPrefab, startingpos, this.transform.rotation);
            b.GetComponent<Brain>().Init();
            population.Add(b);
        }
        
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 startingpos = new Vector3(this.transform.position.x + Random.Range(2, -2),
            this.transform.position.y,
            this.transform.position.z + Random.Range(2 ,-2));
        GameObject offspring = Instantiate(botPrefab, startingpos, this.transform.rotation);
        Brain b = offspring.GetComponent<Brain>();
        if(Random.Range(0,1000)==1)//for mutating 1 in 100
        {
            b.Init();
            b.dna.Mutate();
        }
        else
        {
            b.Init();
            b.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
        }
        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => (o.GetComponent<Brain>().timealive)).ToList();
        population.Clear();

        //breed upper half of sorted list
        for (int i = (int) (sortedList.Count/ 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for(int i=0; i<sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }

  
// Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed>=trialtime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
        
    }
}

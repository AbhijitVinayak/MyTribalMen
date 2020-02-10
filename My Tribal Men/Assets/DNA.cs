using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    List<int> genes = new List<int>();
    int dnalength = 0;
    int maxvalues = 0;

    public DNA(int l, int v)
    {
        dnalength = l;
        maxvalues = v;
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for(int i=0; i< dnalength; i++)
        {
            genes.Add(Random.Range(0, maxvalues));
        }
    }

    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    public void Combine(DNA d1, DNA d2)
    {
        for(int i = 0; i<dnalength; i++)
        {
            if(i<dnalength/2.0)
             {
                    int c = d1.genes[i];
                    genes[i] = c;
             }
            else
            {
                int c = d2.genes[i];
                genes[i] = c;

            }
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, dnalength)] = Random.Range(0, maxvalues);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }
}


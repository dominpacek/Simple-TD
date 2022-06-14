using System.Collections.Generic;
using UnityEngine;

public class Traits
{
    public enum Trait { None, Speedy, Tough, Slimy };

    public static readonly Dictionary<Trait, string> traitDescriptions = new Dictionary<Trait, string>(){
        {Trait.Speedy, "Moves a bit faster"},
        {Trait.Tough, "Has more health"},
        {Trait.Slimy, "Slows down nearby turrets"}
    };


    private static float[] traitWeights = new float[]{
        0.7f, 0.2f, 0.2f, 0.1f
    };

    /* boosts as percent of original stat
    -- 2f would be 200% of original or 100% increase  */
    public static readonly float speedyBoost = 1.4f;
    public static readonly float toughBoost = 2f;

    public static readonly float slowingRange = 3f;
    public static readonly float slowPercentage = 0.3f;
    public static readonly float slowDuration = 0.1f;


    public static Trait chooseTrait()
    {
        float total = 0;

        foreach (float weight in traitWeights)
        {
            total += weight;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < traitWeights.Length; i++)
        {
            if (randomPoint < traitWeights[i])
            {
                return (Trait)i;
            }
            else
            {
                randomPoint -= traitWeights[i];
            }
        }
        return Trait.None;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    // eldritch abomination of level shapes
    private static List<Vector2Int>[] levels = new List<Vector2Int>[]{
    new List<Vector2Int>(){
        new Vector2Int(1, 7),new Vector2Int(2, 7),new Vector2Int(3, 7),new Vector2Int(4, 7),new Vector2Int(4, 8),
        new Vector2Int(4, 9),new Vector2Int(4, 10),new Vector2Int(4, 11),new Vector2Int(5, 11),new Vector2Int(6, 11),
        new Vector2Int(7, 11),new Vector2Int(8, 11),new Vector2Int(8, 10),new Vector2Int(8, 9),new Vector2Int(8, 8),
        new Vector2Int(8, 7),new Vector2Int(8, 6),new Vector2Int(8, 5),new Vector2Int(8, 4),new Vector2Int(8, 3),
        new Vector2Int(9, 3),new Vector2Int(10, 3),new Vector2Int(11, 3),new Vector2Int(11, 4),new Vector2Int(11, 5),
        new Vector2Int(11, 6),new Vector2Int(11, 7),new Vector2Int(11, 8),new Vector2Int(12, 8),new Vector2Int(13, 8),
        new Vector2Int(14, 8),new Vector2Int(14, 7),new Vector2Int(14, 6),new Vector2Int(15, 6),new Vector2Int(16, 6),
        new Vector2Int(17, 6),new Vector2Int(17, 7),new Vector2Int(17, 8),new Vector2Int(18, 8),new Vector2Int(19, 8),
        new Vector2Int(20, 8),new Vector2Int(21, 8),new Vector2Int(21, 7),new Vector2Int(22, 7),new Vector2Int(23, 7)
    },
    new List<Vector2Int>(){
        new Vector2Int(1, 7),new Vector2Int(2, 7),new Vector2Int(3, 7),new Vector2Int(4, 7),new Vector2Int(5, 7),
        new Vector2Int(6, 7),new Vector2Int(7, 7),new Vector2Int(8, 7),new Vector2Int(9, 7),new Vector2Int(10, 7),
        new Vector2Int(10, 8),new Vector2Int(10, 9),new Vector2Int(10, 10),new Vector2Int(10, 11),new Vector2Int(10, 12),
        new Vector2Int(9, 12),new Vector2Int(8, 12),new Vector2Int(7, 12),new Vector2Int(7, 11),new Vector2Int(7, 10),
        new Vector2Int(7, 9),new Vector2Int(8, 9),new Vector2Int(9, 9),new Vector2Int(11, 9),new Vector2Int(12, 9),
        new Vector2Int(13, 9),new Vector2Int(14, 9),new Vector2Int(14, 8),new Vector2Int(14, 7),new Vector2Int(14, 6),
        new Vector2Int(14, 5),new Vector2Int(14, 4),new Vector2Int(14, 3),new Vector2Int(14, 2),new Vector2Int(13, 2),
        new Vector2Int(12, 2),new Vector2Int(12, 3),new Vector2Int(12, 4),new Vector2Int(13, 4),new Vector2Int(15, 4),
        new Vector2Int(16, 4),new Vector2Int(17, 4),new Vector2Int(17, 5),new Vector2Int(17, 6),new Vector2Int(17, 7),
        new Vector2Int(17, 8),new Vector2Int(17, 9),new Vector2Int(17, 10),new Vector2Int(17, 11),new Vector2Int(18, 11),
        new Vector2Int(19, 11),new Vector2Int(20, 11),new Vector2Int(20, 10),new Vector2Int(20, 9),new Vector2Int(20, 8),
        new Vector2Int(20, 7),new Vector2Int(21, 7),new Vector2Int(22, 7),new Vector2Int(23, 7)
},
    new List<Vector2Int>(){
        new Vector2Int(1, 7),new Vector2Int(2, 7),new Vector2Int(3, 7),new Vector2Int(4, 7),new Vector2Int(4, 6),
        new Vector2Int(4, 5),new Vector2Int(4, 4),new Vector2Int(4, 3),new Vector2Int(4, 2),new Vector2Int(5, 2),
        new Vector2Int(6, 2),new Vector2Int(7, 2),new Vector2Int(8, 2),new Vector2Int(9, 2),new Vector2Int(10, 2),
        new Vector2Int(11, 2),new Vector2Int(12, 2),new Vector2Int(13, 2),new Vector2Int(14, 2),new Vector2Int(15, 2),
        new Vector2Int(16, 2),new Vector2Int(17, 2),new Vector2Int(17, 3),new Vector2Int(17, 4),new Vector2Int(17, 5),
        new Vector2Int(17, 6),new Vector2Int(17, 7),new Vector2Int(17, 8),new Vector2Int(17, 9),new Vector2Int(17, 10),
        new Vector2Int(17, 11),new Vector2Int(17, 12),new Vector2Int(17, 13),new Vector2Int(16, 13),new Vector2Int(15, 13),
        new Vector2Int(14, 13),new Vector2Int(13, 13),new Vector2Int(12, 13),new Vector2Int(11, 13),new Vector2Int(10, 13),
        new Vector2Int(9, 13),new Vector2Int(8, 13),new Vector2Int(7, 13),new Vector2Int(7, 12),new Vector2Int(7, 11),
        new Vector2Int(7, 10),new Vector2Int(7, 9),new Vector2Int(7, 8),new Vector2Int(7, 7),new Vector2Int(7, 6),
        new Vector2Int(7, 5),new Vector2Int(8, 5),new Vector2Int(9, 5),new Vector2Int(10, 5),new Vector2Int(11, 5),
        new Vector2Int(12, 5),new Vector2Int(13, 5),new Vector2Int(14, 5),new Vector2Int(14, 6),new Vector2Int(14, 7),
        new Vector2Int(14, 8),new Vector2Int(14, 9),new Vector2Int(14, 10),new Vector2Int(13, 10),new Vector2Int(12, 10),
        new Vector2Int(11, 10),new Vector2Int(10, 10),new Vector2Int(10, 9),new Vector2Int(10, 8),new Vector2Int(10, 7),
        new Vector2Int(11, 7),new Vector2Int(12, 7),new Vector2Int(13, 7),new Vector2Int(15, 7),new Vector2Int(16, 7),
        new Vector2Int(18, 7),new Vector2Int(19, 7),new Vector2Int(20, 7),new Vector2Int(21, 7),new Vector2Int(22, 7),
        new Vector2Int(23, 7)
},
    new List<Vector2Int>(){
        new Vector2Int(1, 7),new Vector2Int(2, 7),new Vector2Int(3, 7),new Vector2Int(4, 7),new Vector2Int(5, 7),new Vector2Int(6, 7),
        new Vector2Int(6, 8),new Vector2Int(6, 9),new Vector2Int(6, 10),new Vector2Int(7, 10),new Vector2Int(7, 11),
        new Vector2Int(7, 12),new Vector2Int(8, 12),new Vector2Int(9, 12),new Vector2Int(10, 12),new Vector2Int(10, 13),
        new Vector2Int(11, 13),new Vector2Int(12, 13),new Vector2Int(13, 13),new Vector2Int(14, 13),new Vector2Int(15, 13),
        new Vector2Int(15, 12),new Vector2Int(16, 12),new Vector2Int(17, 12),new Vector2Int(17, 11),new Vector2Int(17, 10),
        new Vector2Int(17, 9),new Vector2Int(17, 8),new Vector2Int(16, 8),new Vector2Int(15, 8),new Vector2Int(15, 9),
        new Vector2Int(15, 10),new Vector2Int(14, 10),new Vector2Int(13, 10),new Vector2Int(12, 10),new Vector2Int(11, 10),
        new Vector2Int(10, 10),new Vector2Int(10, 9),new Vector2Int(9, 9),new Vector2Int(9, 8),new Vector2Int(9, 7),
        new Vector2Int(9, 6),new Vector2Int(10, 6),new Vector2Int(10, 5),new Vector2Int(11, 5),new Vector2Int(11, 4),
        new Vector2Int(12, 4),new Vector2Int(13, 4),new Vector2Int(14, 4),new Vector2Int(15, 4),new Vector2Int(16, 4),
        new Vector2Int(17, 4),new Vector2Int(17, 5),new Vector2Int(18, 5),new Vector2Int(19, 5),new Vector2Int(19, 6),
        new Vector2Int(19, 7),new Vector2Int(20, 7),new Vector2Int(20, 8),new Vector2Int(21, 8),new Vector2Int(22, 8),
        new Vector2Int(22, 7),new Vector2Int(23, 7)
}
    };

    // chooses one of the pre-created levels randomly
    public static List<Vector2Int> pickLevel()
    {
        int i = Random.Range(0, levels.Length);

        return levels[i];
    }
}

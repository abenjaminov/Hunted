﻿using UnityEngine;

namespace Utils
{
    public static class VectorUtils
    {
        public static Vector2 Abs(this Vector2 me)
        {
            return new Vector2(Mathf.Abs(me.x), Mathf.Abs(me.y));
        }

        public static Vector2Int ToInt(this Vector2 me)
        {
            return new Vector2Int((int)me.x, (int)me.y);
        }
        
        public static Vector2 To2D(this Vector3 me)
        {
            return new Vector2(me.x, me.y);
        }
        
        public static Vector3Int To3D(this Vector2Int me)
        {
            return new Vector3Int(me.x, me.y,0);
        }
        
        public static Vector3 To3D(this Vector2 me)
        {
            return new Vector3(me.x, me.y,0);
        }
    }
}
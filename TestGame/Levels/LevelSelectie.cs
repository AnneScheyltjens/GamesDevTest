﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Levels
{
    public enum LevelSelectie
    {
        None,
        Level1,
        Level2,
        Level3
    }
    public class GameboardSelectie
    {
        public  Dictionary<LevelSelectie, int[,]> GameBoards = new Dictionary<LevelSelectie, int[,]>();

        private static GameboardSelectie uniqueInstance;

        private GameboardSelectie()
        {
            GameBoards.Add(LevelSelectie.None, new int[,] { });

            GameBoards.Add(LevelSelectie.Level1, new int[,]
                {
                    {20,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,21},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,0,2},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,1,1,19},
                    {3,0,0,0,0,0,0,0,0,0,0,4,5,0,0,0,8,0,0,8,0,8,0,0,8,0,0,0,0,2},
                    {3,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,111,0,2},
                    {3,0,0,0,0,0,8,0,0,0,0,0,0,0,0,12,0,0,0,0,0,10,0,12,0,0,0,0,0,2},
                    {3,0,0,4,5,0,0,0,8,0,8,0,0,0,0,4,1,1,1,1,1,1,1,5,0,0,4,1,1,19},
                    {7,16,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {15,17,1,5,0,4,5,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,0,0,0,4,5,0,0,0,0,4,5,0,0,2},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,19},
                    {3,11,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,2},
                    {3,0,0,0,0,0,0,12,0,0,0,0,0,10,0,0,0,0,0,0,12,0,0,0,0,22,0,0,0,2},
                    {17,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,23,1,1,1,18}
                });
            GameBoards.Add(LevelSelectie.Level2, new int[,]
                { 
                    {20,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,21},
                    { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    { 3,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    { 3,0,0,0,0,0,0,12,0,0,0,0,0,0,12,0,0,0,0,0,12,0,0,0,0,0,0,0,12,2},
                    { 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,0,4,1,19},
                    { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    { 3,0,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    { 3,0,0,0,0,0,12,0,10,0,0,0,0,10,0,0,0,0,0,0,0,10,0,12,0,0,0,0,0,2},
                    { 3,0,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
                    { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,111,0,0,2},
                    { 3,0,0,12,0,10,0,0,12,0,0,10,0,0,12,0,0,0,10,0,12,0,0,0,0,0,0,9,0,2},
                    { 17,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,18} 
                });
            GameBoards.Add(LevelSelectie.Level3, new int[,]
            {
                    {20,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,21},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,2},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,1,19},
                    {3,0,0,11,0,0,0,0,0,12,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,9,0,0,0,0,0,0,0,8,0,0,8,0,0,0,0,0,12,0,0,0,10,0,12,0,0,0,0,2},
                    {7,1,1,1,5,0,0,0,0,0,0,0,0,0,0,0,0,0,4,1,1,1,1,1,1,5,0,0,0,2},
                    {3,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,0,0,0,0,0,0,8,0,8,0,0,0,8,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,0,12,0,0,10,0,0,0,10,0,12,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,2},
                    {3,0,4,1,1,1,1,1,1,1,1,5,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,2},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,8,0,0,4,5,0,0,0,2},
                    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,111,0,0,0,0,0,4,19},
                    {3,0,0,0,0,12,0,0,0,0,10,0,0,0,10,0,0,0,10,12,0,0,0,0,0,0,22,0,0,2},
                    {17,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,23,1,1,18}
                });
        }

        public static GameboardSelectie getInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new GameboardSelectie();
            }
            return uniqueInstance;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestGame.Blocks;
using TestGame.Characters;
using TestGame.Enemies;

namespace TestGame.Collision
{
    static class Collidable
    {
        public static List<IGameObject> IntersectsWith(IMovingObject mainObject, List<IGameObject> toCollideWith)
    {
        bool intersects = false;
        bool IsDead = false;
        List<IGameObject> collides = new List<IGameObject>();

        foreach (IGameObject col in toCollideWith)
        {
            if (mainObject.GetType == col.GetType)
            {
                //skip
                //geen collision checken met jezelf
            } else if (col is Block)
            {
                Block blok = col as Block;

                if (mainObject.NextPositie.HitboxRectangle.Intersects(blok.Hitbox))
                {
                    //intersects = true;
                    collides.Add(blok);
                    //intersects
                    /*if (mainObject.NextPositie.HitboxRectangle.Bottom == blok.Hitbox.Top ||
                        mainObject.NextPositie.HitboxRectangle.Bottom == blok.Hitbox.Top + 1)
                    {
                        //wandel op de vloer
                        //walkOnGround = true;
                        //groundLevel = blok.Hitbox.Top;
                    }
                    else
                    {
                        intersects = true;
                    }*/
                }
            } else if (col is Prikkeldraad)
            {
                Prikkeldraad prik = col as Prikkeldraad;

                if (prik.Positie.HitboxRectangle.Intersects(mainObject.NextPositie.HitboxRectangle))
                {
                    if (prik.Positie.HitboxRectangle.Left < mainObject.NextPositie.HitboxRectangle.Right)
                    {
                        //IsDead = true;
                        collides.Add(prik);
                    } else if (prik.Positie.HitboxRectangle.Right > mainObject.NextPositie.HitboxRectangle.Left)
                    {
                        //IsDead = true;
                        collides.Add(prik);
                    } else
                    {
                        //langs boven
                    }
                }

            }


        }
        return collides;
    }

    public static int WalksOnGround(IMovingObject mainObject, List<Block> toCollideWith)
    {
        bool walkOnGround = false;
        int groundLevel = 0;

        foreach (Block col in toCollideWith)
        {
           
                if (mainObject.NextPositie.HitboxRectangle.Intersects(col.Hitbox))
                {
                    if (mainObject is Hero)
                    {
                        //intersects
                        if (mainObject.NextPositie.HitboxRectangle.Bottom == col.Hitbox.Top ||
                            mainObject.NextPositie.HitboxRectangle.Bottom == col.Hitbox.Top + 1)
                        {
                            //wandel op de vloer
                            walkOnGround = true;
                            groundLevel = col.Hitbox.Top;
                        }
                    } else if (mainObject is Wolf)
                    {
                        if (mainObject.NextPositie.HitboxRectangle.Bottom > col.Hitbox.Top)
                        {
                            //wandel op de vloer
                            walkOnGround = true;
                            groundLevel = col.Hitbox.Top;
                        }
                    }
                    
                }
            

        }
        return groundLevel;
    }


    }
}

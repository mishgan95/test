﻿#region using

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Galaxy.Core.Actors;

#endregion

namespace Galaxy.Core.Collision
{
    public static class CollisionChecher
    {
        #region Static Public methods

        private static List<ActorType[]> Teams
        {
            get
            {
                return new List<ActorType[]>
                {
                    new ActorType[] {ActorType.Player, ActorType.PlayerWeapon},
                    new ActorType[] {ActorType.Enemy, ActorType.EnemyWeapon},
                    new ActorType[] {ActorType.Light, ActorType.LightWeapon}
                };
            }
        }

        public static IEnumerable<BaseActor> GetAllCollisions(List<BaseActor> allActors)
        {
            List<BaseActor> collided = new List<BaseActor>();
            foreach (BaseActor actor in allActors)
            {
                var actorTmp = actor;
                collided.AddRange(allActors.Where(baseActor => h_collideCondition(baseActor, actorTmp)));
            }

            return collided;
        }

        #endregion

        #region Private methods

        private static bool h_collideCondition(BaseActor baseActor, BaseActor actorTmp)
        {
            var teams = Teams;
            bool finded = false;
            int index;
            for (index = 0; index < teams.Count; index++)
            {
                for (int i = 0; i < teams[index].Length; i++)
                {
                    if (baseActor.ActorType == teams[index][i])
                    {
                        finded = true;
                        break;
                    }
                }
                if (finded)
                {
                    break;
                }
            }

            bool issameteam = false;
    
            for (int i = 0; i < teams[index].Length; i++) 
            {
                if (actorTmp.ActorType == teams[index][i])
                {
                    issameteam = true;
                    break;
                }
            }
            return baseActor != actorTmp
                   && issameteam == false
                   && h_collidedWith(actorTmp, baseActor);
        }

        private static bool h_collidedWith(BaseActor actor1, BaseActor actor2)
        {
            Rectangle rectangle1;
            {
                int actorX = (int)actor1.Position.X;
                int actorY = (int)actor1.Position.Y;
                rectangle1 = new Rectangle(actorX, actorY, actor1.Width, actor1.Height);
            }

            Rectangle rectangle2;
            {
                int actorX = (int)actor2.Position.X;
                int actorY = (int)actor2.Position.Y;
                rectangle2 = new Rectangle(actorX, actorY, actor2.Width, actor2.Height);
            }

            return rectangle1.IntersectsWith(rectangle2);
        }

        #endregion
    }
}
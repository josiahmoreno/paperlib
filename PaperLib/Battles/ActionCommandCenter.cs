using System;
using System.Collections;
using System.Collections.Generic;

namespace Battle
{
    public class ActionCommandCenter : IActionCommandCenter
    {

        Stack<bool> stack = new Stack<bool>();
        public void AddSuccessfulPress()
        {
            stack.Push(true);
        }

        public IBattleAnimationSequence FetchSequence()
        {
            bool last = false;
            try
            {
                last = stack.Pop();
            } catch(Exception e)
            {

            }
            return new DefaultBattleAnimationSequence(last);
        }
    }
}
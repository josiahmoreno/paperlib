using System;
using System.Collections;
using System.Collections.Generic;

namespace Battle
{
    public class TestActionCommandCenter : IActionCommandCenter
    {

        Stack<bool> stack = new Stack<bool>();

        public void AddFailedPress()
        {
           stack.Push(false);
        }

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
            Console.WriteLine($"fetching the sequence, and checking stack... {string.Join(",", stack.ToArray())}");
            return new DefaultBattleAnimationSequence(last);
        }
    }
}
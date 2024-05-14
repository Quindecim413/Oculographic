using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oculographic
{
    class CommandsRandomizer
    {
        public readonly int TotalCommands, MaxCommand, MinCommand;
        public CommandsRandomizer(int totalCommands, int maxCommand, int minCommand=0)
        {
            TotalCommands = totalCommands;
            MaxCommand = maxCommand;
            MinCommand = minCommand;
        }

        public List<int> Generate()
        {
            int setWidth = MaxCommand + 1 - MinCommand;
            int numFullSets = TotalCommands / setWidth;
            int numCommandsLeft = TotalCommands % setWidth;

            List<int> commands = new List<int>();
            List<int> commandsLeft = new List<int>();
            for (int command = MinCommand; command <= MaxCommand; command++)
            {
                commandsLeft.Add(command);
                for (int i = 0; i < numFullSets; i++)
                {
                    commands.Add(command);
                }
            }
            Shuffle(commandsLeft);
            commandsLeft = commandsLeft.Take(numCommandsLeft).ToList();
            commands.AddRange(commandsLeft);
            Shuffle(commands);
            return commands;
        }

        Random rand = new Random();
        void Shuffle(List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

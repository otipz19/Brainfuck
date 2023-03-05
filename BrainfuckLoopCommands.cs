using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		private static Dictionary<int, int> startToEnd = new Dictionary<int, int>();
		private static Dictionary<int, int> endToStart = new Dictionary<int, int>();

		public static void RegisterTo(IVirtualMachine vm)
		{
			AnalyzeLoopInstructions(vm);
			vm.RegisterCommand('[', (b) => 
				{
                    if (b.Memory[b.MemoryPointer] == 0)
						b.InstructionPointer = startToEnd[b.InstructionPointer];
				}
			);
			vm.RegisterCommand(']', (b) => 
				{
					if (b.Memory[b.MemoryPointer] != 0)
						b.InstructionPointer = endToStart[b.InstructionPointer];
                }
			);
		}

		private static void AnalyzeLoopInstructions(IVirtualMachine vm)
		{
			var stack = new Stack<int>();
			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[')
					stack.Push(i);
				else if (vm.Instructions[i] == ']')
				{
					int startIndex = stack.Pop();
					startToEnd[startIndex] = i;
					endToStart[i] = startIndex;
                }
            }
		}
	}
}
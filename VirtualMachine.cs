using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		private Dictionary<char, Action<IVirtualMachine>> commands = new Dictionary<char, Action<IVirtualMachine>>();

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			if (memorySize <= 0)
				throw new ArgumentOutOfRangeException();
			Memory = new byte[memorySize];
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			if (execute == null)
				throw new ArgumentNullException();
			commands[symbol] = execute;
		}

		public void Run()
		{
			for( ; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				if (!commands.ContainsKey(Instructions[InstructionPointer]))
					continue;
				commands[Instructions[InstructionPointer]](this);
			}
		}
	}
}
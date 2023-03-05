using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', (b) => write((char)b.Memory[b.MemoryPointer]));
			vm.RegisterCommand('+', (b) => { unchecked { b.Memory[b.MemoryPointer]++; } });
			vm.RegisterCommand('-', (b) => { unchecked { b.Memory[b.MemoryPointer]--; } });
			vm.RegisterCommand(',', (b) => b.Memory[b.MemoryPointer] = (byte)read());
			vm.RegisterCommand('<', (b) => {  b.MemoryPointer = b.MemoryPointer == 0 ? b.Memory.Length - 1 : b.MemoryPointer - 1;  });
			vm.RegisterCommand('>', (b) => {  b.MemoryPointer = b.MemoryPointer == b.Memory.Length - 1 ? 0 : b.MemoryPointer + 1;  });
			for (char c = 'A'; c <= 'Z'; c++)
			{
				var tmp = c;
                vm.RegisterCommand(c, (b) => b.Memory[b.MemoryPointer] = (byte)tmp);
				vm.RegisterCommand((char)(c + 32), (b) => b.Memory[b.MemoryPointer] = (byte)(tmp + 32));
            }
			for(char i = '0'; i <= '9'; i++)
			{
				var tmp = i;
				vm.RegisterCommand((char)i, (b) => b.Memory[b.MemoryPointer] = (byte)tmp);
			}
        }
	}
}
/*
                         )/_
               _.--..---"-,--c_
          \L..'           ._O__)_  < oh no! memory leak! />
  ,-.     _.+  _  \..--( /           
    `\.-''__.-' \ (     \_      
      `'''       `\__   /\
                  ')
*/

List<byte[]> leakingList = [];
while (true)
{
    Console.Clear();
    Console.WriteLine("Simulando vazamento de memória...");
    Console.WriteLine("Pressione Ctrl+C para encerrar. \n");

    AllocateMemory(ref leakingList);
    Thread.Sleep(100);
}

static void AllocateMemory(ref List<byte[]> leakingList)
{
    byte[] leakyArray = new byte[1024 * 1024]; // 1MB
    leakingList.Add(leakyArray);

    Console.WriteLine($"Memória alocada: {leakingList.Count} MB");

    // // só pra não da ruim kk
    // if (leakingList.Count > 1000)
    // {
    //     leakingList.Clear();
    //     GC.Collect();
    // }
}

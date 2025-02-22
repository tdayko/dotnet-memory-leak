# Analisando vazamento de memória com .NET Diagnostic Tools

## 1. Visão Geral

Este projeto demonstra um caso simples de vazamento de memória (memory leak) em uma aplicação .NET 9. O objetivo é fornecer um cenário para estudar e praticar o uso das ferramentas de diagnóstico do .NET para identificação e análise de problemas relacionados ao consumo excessivo de memória.

## 2. Estrutura do Projeto

O projeto consiste em uma única classe `Program`, que implementa um vazamento de memória proposital. O vazamento ocorre através do acúmulo contínuo de arrays de bytes em uma lista. Isso demonstra como o crescimento descontrolado de objetos na memória pode afetar o desempenho da aplicação.

## 3. Como Reproduzir o Vazamento de Memória

### 3.1. Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas:

- [.NET SDK 9 ou superior](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0)
- [dotnet-counters](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-counters)
- [dotnet-dump](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-dump)
- [dotnet-trace](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-trace)

### 3.2. Compilação e Execução

1. Clone este repositório ou copie o código para um arquivo `Program.cs`.
2. Navegue até o diretório do arquivo e execute os seguintes comandos:

    ```sh
    # Compilar
    dotnet build -c Release

    # Executar
    dotnet run -c Release
    ```

3. O programa começará a alocar 1MB de memória a cada 100ms.
4. Para evitar um vazamento incontrolável, a lista `leakingList` será limpa e o coletor de lixo (`GC.Collect`) será invocado ao atingir 1000MB alocados.
5. Para interromper a execução, pressione `Ctrl + C`.

## 4. Monitoramento e Diagnóstico

### 4.1. Identificação do ID do Processo

Antes de iniciar o monitoramento, identifique o ID do processo da aplicação com o comando:

```sh
dotnet-trace ps
```
![image](https://github.com/user-attachments/assets/cbfa518e-f760-4379-a894-61e0cf6f0672)


### 4.2. Monitoramento com dotnet-counters

Para acompanhar o consumo de memória da aplicação, utilize o comando:

```sh
dotnet-counters monitor -p <PID>
```

Substitua <PID> pelo ID do processo obtido no passo anterior.
### 4.3. Captura de Dump com dotnet-dump

Gerar um arquivo dump para análisar posteriormente:

```sh
dotnet-dump collect -p <PID>
```

Para analisar o dump, utilize:

```
dotnet-dump analyze <arquivo.dmp>
```

Liste os objetos acumulados na memória com:

```
dumpheap -stat
```
![image](https://github.com/user-attachments/assets/b7d9a06b-0ecb-49f9-918b-9de957f0cab7)


Para analisar um objeto específico na memória, use:

```
dumpheap -mt <endereço>
```
![image](https://github.com/user-attachments/assets/5499f8f5-a5f3-4f35-9b89-1f0e0a515ad7)


### 4.4. Encontrar a Raiz do Acúmulo de Memória

Para identificar a raiz do vazamento de memória, use o comando:

```
gcroot <endereço>
```
![image](https://github.com/user-attachments/assets/ec14e17c-b1a9-426b-9142-5652424861cb)


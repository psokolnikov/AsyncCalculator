using AsyncCalculator;
using Calculator = AsyncCalculator.AsyncCalculator;

ApplicationController.Init();

var calculator = new Calculator();

calculator.Start();

var calculatorArgs = Environment.GetCommandLineArgs().Skip(1).ToArray();

calculator.Execute(calculatorArgs);

Console.ReadKey();



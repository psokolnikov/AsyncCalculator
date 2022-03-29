﻿using Calculator = AsyncCalculator.AsyncCalculator;

var calculator = new Calculator();

calculator.Start();

var calculatorArgs = Environment.GetCommandLineArgs().Skip(1).ToArray();

calculator.Execute(calculatorArgs);

calculator.WriteMetrics();
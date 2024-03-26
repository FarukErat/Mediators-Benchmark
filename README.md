# Benchmarking Mediator Performance: MassTransit vs. MediatR

This repository contains benchmarking code and results comparing the performance of the mediator patterns implemented with MassTransit and MediatR in C#.

## Introduction

Mediator patterns are widely used in software development to decouple components and facilitate communication between them. MassTransit and MediatR are two popular libraries in the .NET ecosystem that provide implementations of mediator patterns, each with its own strengths and trade-offs.

While MediatR is specifically designed as a mediator library, it's important to note that MassTransit is primarily focused on message queue systems, such as RabbitMQ. Despite this, MassTransit can still be used to implement mediator patterns, although it may not be as optimized for this purpose as MediatR.

This benchmark aims to compare the performance of using MassTransit and MediatR as mediators in a simple scenario.

## Benchmark Results

The benchmark results below showcase the performance comparison between MassTransit and MediatR in terms of execution time:

| Method      | Mean        | Error     | StdDev      | Median      |
|------------ |------------:|----------:|------------:|------------:|
| MassTransit | 36,623.9 ns | 727.96 ns | 2,111.94 ns | 35,680.6 ns |
| MediatR     |    161.1 ns |   3.24 ns |     3.47 ns |    160.3 ns |

**Legends:**
- Mean   : Arithmetic mean of all measurements
- Error  : Half of 99.9% confidence interval
- StdDev : Standard deviation of all measurements
- Median : Value separating the higher half of all measurements (50th percentile)
- 1 ns   : 1 Nanosecond (0.000000001 sec)

## Methodology

The benchmark was conducted using [BenchmarkDotNet](https://benchmarkdotnet.org/), a powerful .NET library for benchmarking. The benchmarking code simulates a simple mediator pattern scenario, where a message is passed to the mediator, which in turn dispatches it to the appropriate handler.

## Getting Started

To reproduce the benchmark or explore the code, follow these steps:

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/FarukErat/Mediators-Benchmark.git
   ```

2. Open the solution in your preferred IDE or text editor.

3. Build the solution.

4. Run the benchmark project to execute the performance tests.

## Code Overview

The benchmarking code consists of two implementations of mediators using MassTransit and MediatR libraries. Key components include:

- **MassTransitMediator**: Implementation of mediator using MassTransit.
- **MediatRMediator**: Implementation of mediator using MediatR.
- **BenchmarkRunner**: Entry point for running the benchmarks.

## Conclusion

Based on the benchmark results, MediatR demonstrates significantly better performance compared to MassTransit in this particular scenario. However, it's important to consider that MassTransit is primarily designed for message queue systems like RabbitMQ and may not be as optimized for mediator patterns as MediatR.

When choosing between MassTransit and MediatR for mediator pattern implementations, consider factors such as performance requirements, feature sets, ease of use, and compatibility with your existing infrastructure.

Feel free to explore the code and modify it according to your needs. Contributions and feedback are welcome!

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

*Note: The benchmark results may vary depending on factors such as hardware, environment, and workload. Interpret the results with consideration to your specific use case.*

> "Code never lies, comments sometimes do."

# TVlPQi5Db2Rl

## CI/CD Pipeline
[![Build Status](https://travis-ci.org/user3301/TVlPQi5Db2Rl.svg?branch=travis)](https://travis-ci.org/user3301/TVlPQi5Db2Rl)

## Environment
|Language | Framework | Test |
|---------|---|---|
|[![Language](https://img.shields.io/badge/language-csharp-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/) | [![Platform](https://img.shields.io/badge/.Net%20Core-3.0-brightgreen)](https://dotnet.microsoft.com/download/dotnet-core/3.0) | [![Test](https://img.shields.io/badge/xUnit-2.4-green)](https://xunit.net/) |


This project is built with `.Net Core 3.0` and the `C#` version should be 7.2 or greater. It can run on both Linux and Windows OS.

## QA & Code Review
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/c46dd2ceae9c4f8d86c9eb94f9965e31)](https://www.codacy.com/manual/user3301/TVlPQi5Db2Rl?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=user3301/TVlPQi5Db2Rl&amp;utm_campaign=Badge_Grade)

## How To Run
1. Clone this repo to your local pc;
2. `cd` to `~\TVlPQi5Db2Rl` directory
3. Run `dotnet run`
4. In the command prompt, please specify the path of the csv file and the tax calculation result will print on console

PS: The project is Docker friendly, you can build docker image and run as docker container
```
> docker build - < Dockerfile .
> docker run
```

## Assumptions
1. All numbers are non-negative rational numbers;
2. `AnnualSalary` of an individual will be within `+-1.0 x 10^28 to +-7.9 x 10^28`;
3. The size of csv file cannot exceed the RAM capacity of your system;
4. The output tax result uses the same currency with the input;
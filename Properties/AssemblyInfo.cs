// =======================
// AssemblyInfo.cs
// =======================
// Location: AutomationExcercise/Properties/AssemblyInfo.cs

using NUnit.Framework;

// Enable parallel execution for all test classes (fixtures)
[assembly: Parallelizable(ParallelScope.Fixtures)]

// Set how many test threads NUnit can run in parallel
// Adjust based on CPU cores or CI resource availability
[assembly: LevelOfParallelism(6)]
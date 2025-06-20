# 🧪 Automation Framework - Selenium C# with Reqnroll, NUnit, DI

This is a production-grade test automation framework designed to test the UI flow of [Automation Exercise](https://automationexercise.com) using:

* ✅ **Selenium WebDriver** (C#)
* ✅ **Reqnroll** (SpecFlow-compatible BDD framework)
* ✅ **NUnit** (test runner)
* ✅ **Microsoft.Extensions.DependencyInjection** for DI
* ✅ **Modular browser driver management**
* ✅ **Parallel & Sequential execution via CLI toggle**
* ✅ **Custom HTML reporting & logging**

---

## 🚀 Features

| Feature                          | Description                                               |
| -------------------------------- | --------------------------------------------------------- |
| 🔧 Modular Driver Managers       | Chrome, Firefox, Edge via DI + factory pattern            |
| 💉 Dependency Injection          | Isolated driver/context/log per scenario                  |
| 📋 BDD with Reqnroll             | Gherkin `.feature` files for human-readable automation    |
| 🧪 NUnit Parallel Execution      | Fully thread-safe via scoped driver lifetime              |
| 🧾 Logging & Reporting           | UnifiedLogger + ReportManager HTML outputs                |
| 🧰 Config-driven Behavior        | `appsettings.json` for browser, timeout, screenshot rules |
| 🎯 Category-based Test Execution | Run `@sanity`, `@regression`, etc. via CLI filters        |

---

## 📁 Project Structure

```
Automation-Exercise/
├── AutomationExercise/                # Main test project
│   ├── Features/                      # Gherkin feature files
│   ├── StepDefinitions/              # Step binding classes
│   ├── Pages/                        # POM structure
│   ├── DriverManagers/               # Driver managers + factory
│   ├── Hooks/                        # TestInitialize.cs with Before/AfterScenario
│   ├── Utilities/                    # ConfigReader, Logger, DependencyInjection
│   ├── Reports/                      # Auto-generated HTML reports
│   ├── Properties/                   # AssemblyInfo.cs for parallelism
│   ├── appsettings.json              # Config file
│   └── reqnroll.json                 # Reqnroll parallel toggle
└── README.md
```

---

## ⚙️ Configuration (`appsettings.json`)

```json
"AppSettings": {
  "BaseUrl": "https://automationexercise.com",
  "Browser": "chrome",
  "ExecutionMode": "Parallel",
  "DefaultTimeout": 30,
  "LogLevel": "DEBUG",
  "ScreenshotMode": "OnFailure"
}
```

---

## 🧪 Run Tests via CLI

### ✅ Run All Tests in Parallel (default)

Executes tests with the default thread count from `AssemblyInfo.cs`.

```bash
dotnet test AutomationExercise.csproj
```

### 🧍 Run Tests Sequentially (override threading)

Overrides parallelism and forces tests to run one-by-one.

```bash
dotnet test AutomationExercise.csproj -- NUnit.LevelOfParallelism=1
```

### 🏷️ Run Only Regression Tests

Executes only scenarios tagged with `@regression`.

```bash
dotnet test --filter TestCategory=regression
```

---

## 🏷️ Tag-Based Test Filtering

Use Gherkin tags in your `.feature` files:

```gherkin
@sanity
Scenario: Basic login validation
  Given user is on login page
  ...

@regression
Scenario: Update user profile
  Given user is logged in
  ...
```

Then run them via CLI:

```bash
dotnet test --filter TestCategory=sanity
```

---

## 💡 Best Practices

* ✅ Register WebDriver, DriverManager, and Page classes as **Scoped** or **Transient**
* ✅ Use **Singleton** only for shared, read-only components like `ConfigReader`
* ❌ Avoid static/shared `WebDriver` instances — break thread safety
* 🧪 Leverage Dependency Injection to isolate test scenarios safely
* 🧱 Use Gherkin tags to group and manage test suites effectively

---

## 📩 Future Enhancements

* 🌐 Add remote WebDriver support (Selenium Grid, BrowserStack)
* 📊 Integrate reporting tools like Allure or ExtentReports
* 📹 Add optional video recording support
* 🤖 Build GitHub Actions/CI pipeline integration
* 📥 Implement retry strategy for flaky tests

---

## 📎 Repository

GitHub: [Automation-Exercise](https://github.com/raviprasad-y/Automation-Exercise)

---

## 🙌 Contributing

Suggestions are welcome to extend the capability of the base framework.

---

## 🧠 Need Help?

This framework is designed with real-world scale, CI/CD readiness, and best practices in mind. Whether you're integrating into pipelines, customizing drivers, or extending reporting, feel free to fork, clone, and adapt it as needed.

For any issues or support:

* Use the [GitHub Issues](https://github.com/raviprasad-y/Automation-Exercise/issues) page
* Or drop a feature request

---

Happy Testing! 🎯

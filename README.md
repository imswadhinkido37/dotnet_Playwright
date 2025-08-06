# Dotnet Playwright Automation Framework

This repository contains a **Playwright automation framework** built with **.NET 7** and **C#**.  
It automates UI tests for the [SauceDemo](https://www.saucedemo.com/) website with NUnit and integrates Allure reporting, screenshots, and video recording.

---

## Features

- Cross-browser automation using Microsoft Playwright  
- Tests for multiple user scenarios with SauceDemo  
- Screenshot and video recording support during test execution  
- Allure reporting integration (optional)  
- NUnit test framework support  
- Clean, modular, and maintainable C# code  

---

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)  
- [Node.js](https://nodejs.org/) (for Playwright browsers installation)  
- PowerShell / Command Prompt (Windows) or Terminal (macOS/Linux)  
- Java JDK 11+ (for Allure reporting) — optional  

### Setup

1. Clone this repository:

   ```bash
   git clone https://github.com/imswadhinkido37/dotnet_Playwright.git
   cd dotnet_Playwright/SauceDemo_Tests
````

2. Install NuGet packages:

   ```bash
   dotnet restore
   ```

3. Install Playwright browsers:

   ```bash
   npx playwright install
   ```

4. (Optional) Install Allure commandline for reports:
   [Installation Guide](https://docs.qameta.io/allure/#_installing_a_commandline)

---

## Running Tests

Run tests using the .NET CLI:

```bash
dotnet test
```

Tests will:

* Launch Chromium browser (headless=false by default)
* Perform UI actions like login, add/remove items, checkout
* Capture screenshots & video recordings in `Screenshots/` and `Test_recording/` folders

---

## Screenshots and Video Recording

* Screenshots are saved as `.jpeg` in the `Screenshots/` folder during test failures or as specified.
* Video recordings for each test are saved in `Test_recording/{TestName}/` folders.

---

## Reporting (Allure)

* After test execution, generate Allure reports with:

  ```bash
  allure generate --clean
  allure open
  ```

* Make sure you have Java and Allure installed and configured properly.

---

## Notes on Repository & Usage

* This repository is **private / proprietary** and intended for **personal learning and demonstration** only.
* **Do NOT use or distribute this code without permission.**
* Please contact the author for collaboration or permission requests.

---

## Author

**Swadhin Patnaik**
[GitHub Profile](https://github.com/imswadhinkido37)
[LinkedIn](https://www.linkedin.com/in/swadhin-patnaik-20910a21b/)
📧 [imswadhin28@gmail.com](mailto:imswadhin28@gmail.com) 

---

## Additional Resources

* [Playwright for .NET Docs](https://playwright.dev/dotnet/docs/intro)
* [NUnit Docs](https://nunit.org/)
* [Allure NUnit Adapter](https://github.com/allure-framework/allure-nunit)

---

### License

This repository does **not** grant permission to reuse or redistribute any part of the code without explicit consent from the author.

---

If you want, I can also help you prepare a **LICENSE** file or add badges, gifs, or other extras! Let me know.
```

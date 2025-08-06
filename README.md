# 🎯 Dotnet Playwright Automation Framework

This repository contains a **Playwright-based end-to-end UI automation framework** built with **.NET 7** and **C#**.  
It automates test scenarios for [SauceDemo](https://www.saucedemo.com/) website using NUnit, supports screenshots, video recording, and Allure reporting integration.

---

## 📌 Tech Stack

- **Programming Language:** C# (.NET 7)  
- **Test Framework:** NUnit  
- **Browser Automation:** Microsoft Playwright for .NET  
- **Reporting:** Allure NUnit Adapter (optional)  
- **CI/CD:** Ready for GitHub Actions, Azure DevOps, Jenkins  
- **Media:** Screenshots & video recording enabled per test  
- **Browsers Supported:** Chromium (default), Firefox, WebKit (can be added)  

---

## 📁 Folder Structure

```

SauceDemo\_Tests/
│
├── Screenshots/               # Screenshots saved on test failures or as needed
├── Test\_recording/            # Video recordings of tests
├── bin/                      # Build outputs
├── obj/                      # Intermediate files
├── SauceDemo\_Tests.csproj     # Project file
├── SauceDemo\_Tests.sln        # Solution file
├── testcode.cs                # Main test code file(s)
├── README.md                  # This file

````

---

## 🧪 How to Run Tests

### 🔧 1. Prerequisites

- Install [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)  
- Install [Node.js (v16+)](https://nodejs.org/) (required for Playwright browser installation)  
- (Optional) Install [Java JDK 11+](https://adoptium.net/) for Allure reports  

### 🔧 2. Setup

1. Clone the repo and navigate to the test folder:

   ```bash
   git clone https://github.com/imswadhinkido37/dotnet_Playwright.git
   cd dotnet_Playwright/SauceDemo_Tests
````

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Install Playwright browsers (required only once):

   ```bash
   npx playwright install
   ```

---

### ▶️ 4. Run Tests

```bash
dotnet test
```

* Tests launch Chromium browser (headed mode by default)
* Captures screenshots and video recordings for debugging
* Test results visible in the console

---

## 📷 Media & Reporting Features

* **Screenshots:** Saved in `Screenshots/` on failure or as configured
* **Video Recording:** Stored per test in `Test_recording/{TestName}/`
* **Allure Reports:** Generate advanced test reports (requires Java & Allure CLI)

Generate Allure report after test run:

```bash
allure generate --clean
allure open
```

---

## 📌 Test Scenarios Covered

* **TC001:** Add 3 cheapest products, remove the 2nd cheapest, verify cart and checkout
* **TC002:** Verify 3rd highest priced product details for `standard_user`
* **TC003:** Verify 3rd highest priced product for `problem_user` (handles expected UI bugs)

---

## 🔒 Credentials Used

* Username: `standard_user`
* Username: `problem_user`
* Password: `secret_sauce`

Official SauceDemo test accounts.

---

## 🚀 CI/CD Integration

* This framework can be integrated easily with GitHub Actions, Azure DevOps, Jenkins.
* Supports artifact uploads (screenshots, videos) for better debugging in pipelines.

---

## 🙋 Author

**Swadhin Patnaik**
[GitHub Profile](https://github.com/imswadhinkido37) | [LinkedIn](https://www.linkedin.com/in/imswadhinkido37)
Email: [imswadhinkido37@gmail.com](mailto:imswadhinkido37@gmail.com)

---

## 📄 License

This project is for educational and demo purposes only.
Please contact the author for permission before reusing or redistributing any part of this code.

---

## 💬 Feedback & Contributions

Feel free to raise issues or submit pull requests for improvements.

---
```

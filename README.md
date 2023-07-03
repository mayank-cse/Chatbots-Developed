<a name="readme-top"></a>
# Chatbot Repository
Welcome to my chatbot repository! This repository contains a collection of chatbot projects created using C#, Python, and Microsoft Bot Composer. Each project focuses on different use cases and showcases the capabilities of chatbot development.

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#introduction">Introduction</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#prerequisites">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#overview">Overview</a></li>
        <li><a href="#install-net-core-cli">Installation</a></li>
        <li><a href="#create-a-luis-application-to-enable-language-understanding">Enable LUIS</a></li>
      </ul>
    </li>
    <li><a href="#to-try-this-sample">Try This Sample</a></li>
    <li><a href="#testing-the-bot-using-bot-framework-emulator">Emulator Testing</a></li>
    <li><a href="#deploy-the-bot-to-azure">Deploying</a></li>
    <li><a href="#projects">Projects</a></li>
    <li><a href="#resources-used-to-learn">Resources Used to Learn</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>


## Introduction
In this repository, you will find a variety of chatbot projects that I have created using C#, Python, and Microsoft Bot Composer. These projects demonstrate my skills and experience in chatbot development, showcasing different functionalities and integration capabilities.

Each project is located in its respective folder, containing the necessary source code, configuration files, and any additional resources required to run and test the chatbot. The projects are organized based on their specific use cases and functionalities.

## Projects
- **Ecommerce Merchandiser Bot**: This chatbot assists with ecommerce merchandising tasks, providing product information, handling customer queries, and offering personalized recommendations. [Project Repo](https://github.com/mayank-cse/EcommerceMerchandizerBot)

- **Admin Bot**: The admin bot simplifies administrative tasks by automating processes, such as user management, data retrieval, and report generation. [Project Repo](https://github.com/mayank-cse/Admin-Bot-with-Authentication)

- **Console Bot with Language Studio QnA**: This chatbot, built using the Microsoft Bot Framework and Language Understanding Intelligent Service (LUIS), enables users to interact with a console-based application by asking questions and receiving instant responses.

- **Flight Booking Bot using Intent Recognizer**: The flight booking bot leverages an intent recognizer to understand user requests and assists with flight reservations, fare information, and itinerary management. [Project Repo](https://github.com/mayank-cse/Weather-Bot)

- **To-Do Bot**: This bot helps users manage their tasks and to-do lists, allowing them to add, update, and complete tasks through conversational interactions. [Project Repo](https://github.com/mayank-cse/ToDoBot-with-Email-Authentication)

- **Hunger Free Society**: This chatbot project focuses on raising awareness about hunger-related issues, providing information on food banks, donations, and volunteering opportunities. [Project Repo](https://github.com/mayank-cse/Hunger-Free-Society)

- **Proactive Messaging Bot (Route ApiNotify)**: The proactive messaging bot utilizes the Route ApiNotify service to send proactive messages to users, delivering timely notifications and updates.

- **Echo Bot with Language Studio**: The echo bot uses the Microsoft Bot Framework and Language Understanding Models to understand and respond to user input, providing a simple conversational experience.

Each project folder contains a README file with detailed information about the project, including instructions on how to run and test the chatbot.

## Built With
Bot Framework v4 core bot sample.

This bot has been created using [Bot Framework](https://dev.botframework.com), it shows how to:

- Use [LUIS](https://www.luis.ai) to implement core AI capabilities
- Implement a multi-turn conversation using Dialogs
- Handle user interruptions for such things as `Help` or `Cancel`
- Prompt for and validate requests for information from the user

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Prerequisites

This sample **requires** prerequisites in order to run.

### Overview

This bot uses [LUIS](https://www.luis.ai), an AI based cognitive service, to implement language understanding.

### Install .NET Core CLI

- [.NET Core SDK](https://dotnet.microsoft.com/download) version 3.1

  ```bash
  # determine dotnet version
  dotnet --version
  ```

- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/).
- Install the latest version of the [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest) tool. Version 2.0.54 or higher.

### Create a LUIS Application to enable language understanding

The LUIS model for this example can be found under `CognitiveModels/BankLuisModel.json` and the LUIS language model setup, training, and application configuration steps can be found [here](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-v4-luis?view=azure-bot-service-4.0&tabs=cs).

Once you created the LUIS model, update `appsettings.json` with your `LuisAppId`, `LuisAPIKey` and `LuisAPIHostName`.

```json
  "LuisAppId": "Your LUIS App Id",
  "LuisAPIKey": "Your LUIS Subscription key here",
  "LuisAPIHostName": "Your LUIS App region here (i.e: westus.api.cognitive.microsoft.com)"
```
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## To try this sample

- In a terminal, navigate to `Dev-BankBot`

    ```bash
    # change into project folder
    cd DevVirtualBankingAssistant
    ```

- Run the bot from a terminal or from Visual Studio, choose option A or B.

  A) From a terminal

  ```bash
  # run the bot
  dotnet run
  ```

  B) Or from Visual Studio

  - Launch Visual Studio
  - File -> Open -> Project/Solution
  - Navigate to `DevVirtualBankingAssistant` folder
  - Select `DevVirtualBankingAssistant.csproj` file
  - Press `F5` to run the project

## Testing the bot using Bot Framework Emulator

[Bot Framework Emulator](https://github.com/microsoft/botframework-emulator) is a desktop application that allows bot developers to test and debug their bots on localhost or running remotely through a tunnel.

- Install the Bot Framework Emulator version 4.5.0 or greater from [here](https://github.com/Microsoft/BotFramework-Emulator/releases)

### Connect to the bot using Bot Framework Emulator

- Launch Bot Framework Emulator
- File -> Open Bot
- Enter a Bot URL of `http://localhost:3978/api/messages

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## The LUIS Bank Transaction Concept
The bot is built around a very typical banking scenario which has two main capabilities:
* Check balance
* Make a transfer

### This sample demonstrates:
* __Luis Intent detection__ 
	* Using `LuisRecognizer` not `LuisRecognizerMiddleware` 
	* Using Luis in middleware means every single message will go via Luis which is not necessary and costly in this scenario because once we have the intent and initial entities we no longer require Luis
* __Luis entity extraction__; getting the entities we have from the initial Luis utterance
* __Entity completion__; using bot dialogs to complete entities that were missing from initial Luis utterance
* __Basic bot dialogs with waterfall steps__

## Deploy the bot to Azure

To learn more about deploying a bot to Azure, see [Deploy your bot to Azure](https://aka.ms/azuredeployment) for a complete list of deployment instructions.

<p align="right">(<a href="readme-top">back to top</a>)</p>

## Resources Used to Learn
In my journey of chatbot development, I have utilized various resources to enhance my skills and understanding. This section contains a list of the resources I have found helpful in learning and improving my chatbot development capabilities. These resources include tutorials, documentation, online courses, and community forums.

- [Bot Framework Documentation](https://docs.botframework.com)
- [Bot Basics](https://docs.microsoft.com/azure/bot-service/bot-builder-basics?view=azure-bot-service-4.0)
- [Dialogs](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-concept-dialog?view=azure-bot-service-4.0)
- [Gathering Input Using Prompts](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-prompts?view=azure-bot-service-4.0&tabs=csharp)
- [Activity processing](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-concept-activity-processing?view=azure-bot-service-4.0)
- [Azure Bot Service Introduction](https://docs.microsoft.com/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0)
- [Azure Bot Service Documentation](https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0)
- [.NET Core CLI tools](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x)
- [Azure CLI](https://docs.microsoft.com/cli/azure/?view=azure-cli-latest)
- [Azure Portal](https://portal.azure.com)
- [Language Understanding using LUIS](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/)
- [Channels and Bot Connector Service](https://docs.microsoft.com/en-us/azure/bot-service/bot-concepts?view=azure-bot-service-4.0)
- 
Feel free to explore these resources to further expand your knowledge in chatbot development and related technologies.

## Contributing
Contributions to this repository are welcome! If you have any improvements, bug fixes, or additional chatbot projects that you would like to contribute, please follow the guidelines outlined in the contributing guidelines.

Your contributions can help enhance the diversity and value of chatbot projects available to other developers and enthusiasts.

<!-- CONTACT -->
## Contact

Mayank Gupta - [@MayankGuptaCse1](https://twitter.com/MayankGuptacse1) - mayank.guptacse1@gmail.com

Project Link: [https://github.com/mayank-cse/DEV-A-Virtual-Banking-Assistant](https://github.com/mayank-cse/DEV-A-Virtual-Banking-Assistant)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## License
This repository is licensed under the MIT License. You are free to use, modify, and distribute the code and resources in this repository, as long as you include the appropriate attribution and adhere to the terms and conditions of the license.

If you have any questions or suggestions, feel free to reach out. Happy chatbot development!

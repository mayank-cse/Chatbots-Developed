// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.16.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.AI.QnA;
using Azure.AI.Language.QuestionAnswering;
using Azure;
using System;

namespace qnaIntegration.Bots
{
    public class EchoBot : ActivityHandler
    {
        public object EchoBotQnA { get; private set; }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
            await GetAnswerFromQnAMaker(turnContext, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
        private async Task GetAnswerFromQnAMaker(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            Uri endpoint = new Uri("https://jdbots-d-language-service.cognitiveservices.azure.com/");
            AzureKeyCredential credential = new AzureKeyCredential("a0f2e692cee347208905478f89096400");
            string projectName = "Connect-EchoBot";
            string deploymentName = "test";
            string question = turnContext.Activity.Text;


            QuestionAnsweringClient client = new QuestionAnsweringClient(endpoint, credential);

            QuestionAnsweringProject project = new QuestionAnsweringProject(projectName, deploymentName);

            Response<AnswersResult> response = client.GetAnswers(question, project);

            foreach (KnowledgeBaseAnswer answer in response.Value.Answers)
      
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Bot says: " + answer.Answer), cancellationToken);
            }

            /*else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Sorry, could not find an answer in QnA system. Please rephrase your question."), cancellationToken);

            }*/



        }


        //public QnAMaker EchoBotQnA { get; private set; }


    }
    
}

﻿using EcommerceAdminBot.Models;
using EcommerceAdminBot.Services;
using EcommerceAdminBot.Utilities;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceAdminBot.Dialogs.Operations
{
    public class UpdateProductDialog : CancelAndHelpDialog
    {
        CosmosDBClient _cosmosDBClient;
        StateService _stateService;
        public UpdateProductDialog(CosmosDBClient cosmosDBClient, StateService stateService) : base(nameof(UpdateProductDialog))
        {
            _cosmosDBClient = cosmosDBClient;
            _stateService = stateService;

            var waterfallSteps = new WaterfallStep[]
            {
                IntroStepAsync,
                ViewProductsStepAsync,
                SelectProductStepAsync,
                UpdateProductStepAsync,
                UpdateMoreProductsStepAsync,
                SummaryStepAsync,
            };

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new IndividualProductUpdateDialog(_cosmosDBClient, _stateService));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(ChoicePrompt), new PromptOptions()
            {
                Prompt = MessageFactory.Text("How would you like to see your products?"),
                Choices = ChoiceFactory.ToChoices(new List<string> { "View All Products", "View By Category" }),
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> ViewProductsStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["ViewBy"] = ((FoundChoice)stepContext.Result).Value;
            string viewBy = (string)stepContext.Values["ViewBy"];

            if ("View By Category".Equals(viewBy))
            {
                return await stepContext.PromptAsync(nameof(ChoicePrompt), new PromptOptions()
                {
                    Prompt = MessageFactory.Text("Please select the category to view the products?"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "Television", "Laptop", "Air Conditioner", "Monitor", "Speaker", "Earphones" }),
                }, cancellationToken);
            }
            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> SelectProductStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string viewBy = (string)stepContext.Values["ViewBy"];
            if ("View All Products".Equals(viewBy))
            {
                List<ProductDBDetails> productDBDetails = await _cosmosDBClient.QueryAllItemsAsync("All", "");

                if (productDBDetails.Count > 0)
                {
                    var attachments = new List<Attachment>();
                    var reply = MessageFactory.Attachment(attachments);
                    reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                    for (int i = 0; i < productDBDetails.Count; i++)
                    {
                        reply.Attachments.Add(Cards.Cards.GetThumbnailCardForUpdateView(productDBDetails[i].Id, productDBDetails[i].ProductName, productDBDetails[i].Price, productDBDetails[i].Image).ToAttachment());
                    }
                    // Send the card(s) to the user as an attachment to the activity
                    await stepContext.Context.SendActivityAsync(reply, cancellationToken);
                    return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { }, cancellationToken);
                }
                else
                {
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("You don't have any products to update."), cancellationToken);
                    return await stepContext.EndDialogAsync(null, cancellationToken);
                }
                
            }
            else
            {
                stepContext.Values["Category"] = ((FoundChoice)stepContext.Result).Value;

                List<ProductDBDetails> productDBDetails = await _cosmosDBClient.QueryAllItemsAsync("Category", (string)stepContext.Values["Category"]);

                if (productDBDetails.Count > 0)
                {
                    var attachments = new List<Attachment>();
                    var reply = MessageFactory.Attachment(attachments);
                    reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                    for (int i = 0; i < productDBDetails.Count; i++)
                    {
                        reply.Attachments.Add(Cards.Cards.GetThumbnailCardForUpdateView(productDBDetails[i].Id, productDBDetails[i].ProductName, productDBDetails[i].Price, productDBDetails[i].Image).ToAttachment());
                    }
                    // Send the card(s) to the user as an attachment to the activity
                    await stepContext.Context.SendActivityAsync(reply, cancellationToken);
                    return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { }, cancellationToken);
                }
                else
                {
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text($"You don't have any products for {(string)stepContext.Values["Category"]} category."), cancellationToken);

                    // Restart the main dialog with a different message the second time around
                    var promptMessage = "What else can I do for you?";
                    return await stepContext.ReplaceDialogAsync(InitialDialogId, promptMessage, cancellationToken);
                }
            }
        }

        private async Task<DialogTurnResult> UpdateProductStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            
            stepContext.Values["ProductId"] = (string)stepContext.Result;

            List<ProductDBDetails> productList = await _cosmosDBClient.QueryItemWithIdAsync((string)stepContext.Values["ProductId"]);

            return await stepContext.BeginDialogAsync(nameof(IndividualProductUpdateDialog), productList[0], cancellationToken);
        }

        private async Task<DialogTurnResult> UpdateMoreProductsStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions
            {
                Prompt = MessageFactory.Text("Would you like to update more products?")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                return await stepContext.ReplaceDialogAsync(InitialDialogId, null, cancellationToken);
            }
            else
            {
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }
    }
}

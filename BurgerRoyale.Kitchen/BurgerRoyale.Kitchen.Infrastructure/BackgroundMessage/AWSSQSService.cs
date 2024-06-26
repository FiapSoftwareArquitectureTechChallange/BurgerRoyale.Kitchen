﻿using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using BurgerRoyale.Kitchen.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Kitchen.Domain.CredentialsConfiguration;
using System.Text.Json;

namespace BurgerRoyale.Kitchen.Infrastructure.BackgroundMessage;

public class AWSSQSService(IAmazonSQS _amazonSQSClient) : IMessageService
{
    private readonly ICredentialConfiguration _credentialConfiguration;

    public async Task<string> SendMessageAsync(string queueName, string message)
    {
        try
        {
            string queueUrl = await GetQueueUrl(queueName);

            var response = await _amazonSQSClient.SendMessageAsync(queueUrl, message);

            return response.MessageId;
        }
        catch (Exception exception)
        {
            throw new Exception(
                $"Error sending messages to AWS SQS Queue ({queueName})",
                exception
            );
        }
    }

    public async Task<string> SendMessageAsync(string queueName, dynamic messageBody)
    {
        string message = JsonSerializer.Serialize(messageBody);

        return await SendMessageAsync(queueName, message);
    }

    public async Task<IEnumerable<TResponse>> ReadMessagesAsync<TResponse>(string queueName, int? maxNumberOfMessages)
    {
        try
        {
            string queueUrl = await GetQueueUrl(queueName);

            var request = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = maxNumberOfMessages ?? 10
            };

            var response = await _amazonSQSClient.ReceiveMessageAsync(request);

            List<TResponse> messages = new();

            foreach (var message in response.Messages)
            {
                messages.Add(JsonSerializer.Deserialize<TResponse>(message.Body)!);

                await _amazonSQSClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
            }

            return messages;
        }
        catch (Exception exception)
        {
            throw new Exception(
                $"Error reading messages from AWS SQS Queue ({queueName})",
                exception
            );
        }
    }

    private IAmazonSQS CreateClient()
    {
        var credentials = new SessionAWSCredentials(
            _credentialConfiguration.AccessKey(),
            _credentialConfiguration.SecretKey(),
            _credentialConfiguration.SessionToken()
        );

        var region = RegionEndpoint.GetBySystemName(_credentialConfiguration.Region());

        return new AmazonSQSClient(
            credentials,
            region
        );
    }

    private async Task<string> GetQueueUrl(string queueName)
    {
        try
        {
            var response = await _amazonSQSClient.GetQueueUrlAsync(
                new GetQueueUrlRequest(queueName)
            );

            return response.QueueUrl;
        }
        catch (QueueDoesNotExistException)
        {
            return await CreateQueue(queueName);
        }
    }

    private async Task<string> CreateQueue(string queueName)
    {
        var response = await _amazonSQSClient.CreateQueueAsync(
            new CreateQueueRequest(queueName)
        );

        return response.QueueUrl;
    }
}

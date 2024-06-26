﻿using Amazon.SQS;
using Amazon.SQS.Model;
using BurgerRoyale.Kitchen.Infrastructure.BackgroundMessage;
using Moq;
using System.Text.Json;

namespace BurgerRoyale.Kitchen.Infrastructure.Tests.MessageServices;

internal class AWSSQSServiceShould
{
    private Mock<IAmazonSQS> awsClientMock;

    private AWSSQSService service;

    [SetUp]
    public void SetUp()
    {
        awsClientMock = new Mock<IAmazonSQS>();
        service = new AWSSQSService(awsClientMock.Object);
    }

    [Test]
    public async Task Create_Queue_When_Does_Not_Exist()
    {
        #region Arrange(Given)

        string queueName = "myqueue";
        string queueUrl = $"http://localhost/{queueName}";

        string message = "my Message";
        string messageId = Guid.NewGuid().ToString();

        awsClientMock
            .Setup(x => x.GetQueueUrlAsync(
                It.IsAny<GetQueueUrlRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new QueueDoesNotExistException("Exception message"));

        awsClientMock
            .Setup(x => x.CreateQueueAsync(
                It.IsAny<CreateQueueRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new CreateQueueResponse()
            {
                QueueUrl = queueUrl
            });

        awsClientMock
            .Setup(x => x.SendMessageAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new SendMessageResponse()
            {
                MessageId = messageId
            });

        #endregion

        #region Act(When)

        string response = await service.SendMessageAsync(queueName, message);

        #endregion

        #region Assert(Then)

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.EqualTo(messageId));

        #endregion
    }

    [Test]
    public async Task Throw_Integration_Exception_When_Error()
    {
        #region Arrange(Given)

        string queueName = "myqueue";
        string message = "my Message";

        awsClientMock
            .Setup(x => x.GetQueueUrlAsync(
                It.IsAny<GetQueueUrlRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new QueueDoesNotExistException("Exception message"));

        #endregion

        #region Act(When)

        Exception? threwException = null;

        try
        {
            await service.SendMessageAsync(queueName, message);
        }
        catch (Exception ex)
        {
            threwException = ex;
        }

        #endregion

        #region Assert(Then)

        Assert.That(threwException, Is.Not.Null);
        Assert.That(threwException, Is.InstanceOf<Exception>());

        string expectedExceptionMessage = $"Error sending messages to AWS SQS Queue ({queueName})";
        Assert.That(threwException.Message, Is.EqualTo(expectedExceptionMessage));

        #endregion
    }

    [Test]
    public async Task Send_Serialized_Message()
    {
        #region Arrange(Given)

        string queueName = "myqueue";
        string queueUrl = $"http://localhost/{queueName}";

        var messageBody = new { MessageProperty = "Value" };
        string messageId = Guid.NewGuid().ToString();

        awsClientMock
            .Setup(x => x.GetQueueUrlAsync(
                It.IsAny<GetQueueUrlRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new GetQueueUrlResponse
            {
                QueueUrl = queueUrl,
            });

        awsClientMock
            .Setup(x => x.SendMessageAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new SendMessageResponse()
            {
                MessageId = messageId
            });

        #endregion

        #region Act(When)

        await service.SendMessageAsync(queueName, messageBody);

        #endregion

        #region Assert(Then)

        awsClientMock
            .Verify(
                x => x.SendMessageAsync(
                    queueUrl,
                    It.Is<string>(m => m.Contains("MessageProperty")),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

        #endregion
    }

    [Test]
    public async Task Read_Message()
    {
        #region Arrange(Given)

        string queueName = "myqueue";
        string queueUrl = $"http://localhost/{queueName}";

        awsClientMock
            .Setup(x => x.GetQueueUrlAsync(
                It.IsAny<GetQueueUrlRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new GetQueueUrlResponse
            {
                QueueUrl = queueUrl,
            });

        awsClientMock
            .Setup(
                x => x.ReceiveMessageAsync(
                    It.IsAny<ReceiveMessageRequest>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ReceiveMessageResponse()
            {
                Messages = [
                    new() { Body = JsonSerializer.Serialize(new { MessageProperty = "Message 1" }) },
                    new() { Body = JsonSerializer.Serialize(new { MessageProperty = "Message 2" }) },
                    new() { Body = JsonSerializer.Serialize(new { MessageProperty = "Message 3" }) }
                ]
            });

        #endregion

        #region Act(When)

        IEnumerable<dynamic> messages = await service.ReadMessagesAsync<dynamic>(queueName, null);

        #endregion

        #region Assert(Then)

        Assert.That(messages, Is.Not.Null);
        Assert.That(messages, Is.Not.Empty);

        Assert.That(messages.Count(), Is.EqualTo(3));

        awsClientMock
            .Verify(
                x => x.ReceiveMessageAsync(
                    It.Is<ReceiveMessageRequest>(r =>
                        r.QueueUrl == queueUrl
                        && r.MaxNumberOfMessages == 10
                    ),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

        awsClientMock
            .Verify(
                x => x.DeleteMessageAsync(
                    queueUrl,
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(3)
            );

        #endregion
    }

    [Test]
    public async Task Throw_Integration_Exception_When_Error_Reading_Message()
    {
        #region Arrange(Given)

        string queueName = "myqueue";

        awsClientMock
            .Setup(x => x.GetQueueUrlAsync(
                It.IsAny<GetQueueUrlRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new Exception("Exception message"));

        #endregion

        #region Act(When)

        Exception? threwException = null;

        try
        {
            await service.ReadMessagesAsync<string>(queueName, 10);
        }
        catch (Exception ex)
        {
            threwException = ex;
        }

        #endregion

        #region Assert(Then)

        Assert.That(threwException, Is.Not.Null);
        Assert.That(threwException, Is.InstanceOf<Exception>());

        string expectedExceptionMessage = $"Error reading messages from AWS SQS Queue ({queueName})";
        Assert.That(threwException.Message, Is.EqualTo(expectedExceptionMessage));

        #endregion
    }
}

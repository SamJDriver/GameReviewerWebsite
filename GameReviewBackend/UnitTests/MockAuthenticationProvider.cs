using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Moq;

namespace UnitTests
{
    public class MockAuthenticationProvider : Mock<IAuthenticationProvider>
    {
        public MockAuthenticationProvider(string accessToken = null) : base(MockBehavior.Strict)
        {
            this.Setup(
                provider => provider.AuthenticateRequestAsync(It.IsAny<RequestInformation>(), It.IsAny<Dictionary<string,object>>(), It.IsAny<CancellationToken>()))
                .Callback<RequestInformation, Dictionary<string, object>, CancellationToken>((r,d,c) => r.Headers.Add("Authorization", $"Bearer {accessToken ?? "Default-Token" }"))
                .Returns(Task.FromResult(0));
        }
    }
}
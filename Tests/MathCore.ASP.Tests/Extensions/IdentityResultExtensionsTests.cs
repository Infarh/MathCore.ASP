using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathCore.ASP.Tests.Extensions
{
    [TestClass]
    public class IdentityResultExtensionsTests
    {
        [TestMethod]
        public void ThrowIfNotSuccessTest()
        {
            const int expected_errors_count = 10;
            var identity_errors = Enumerable.Range(1, expected_errors_count)
               .ToArray(i => new IdentityError
               {
                   Code = $"Code {i}",
                   Description = $"Description {i}"
               });

            var identity_result = IdentityResult.Failed(identity_errors);

            const string expected_message = "Message";
            Assert.That.Method(() => identity_result.ThrowIfNotSuccess(expected_message))
               .Throw<AggregateException>()
               .Where(exception => exception.Message).Check(message => message
                   .IsEqual(string.Join(" ", identity_errors.Select(e => $"({e.Code}:{e.Description})").InsertBefore(expected_message))))
               .Where(exception => exception.InnerExceptions).Check(Errors => Errors
                   .Where(errors => errors.Count).Check(count => count.IsEqual(expected_errors_count))
                   .WhereItems(errors => errors).Check(errors => errors
                       .All((error, i) => error
                           .Where(e => e.Message).IsEqual($"Code {i + 1}:Description {i + 1}"))));
        }

        [TestMethod, Ignore]
        public void ToErrorStringTest()
        {
            Assert.Fail();
        }

        [TestMethod, Ignore]
        public void ToErrorStringExtendedTest()
        {
            Assert.Fail();
        }

        [TestMethod, Ignore]
        public void ToExceptionsTest()
        {
            Assert.Fail();
        }

        [TestMethod, Ignore]
        public void AddModelErrorsTest()
        {
            Assert.Fail();
        }
    }
}